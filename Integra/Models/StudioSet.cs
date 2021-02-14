using Integra.Core;
using Integra.Core.Interfaces;
using MidiXL;
using System;
using System.Diagnostics;

namespace Integra.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 studio set data structure.
    /// </summary>
    /// <remarks><i>Address: 18 00 20 00</i></remarks>
    public class StudioSet : IntegraBase<StudioSet>
    {
        #region Fields

        private IntegraParts _SelectedPart = IntegraParts.Part01;
        private IntegraToneTypes _ToneType = IntegraToneTypes.SuperNATURALAcousticTone;

        private Tone _SelectedTone;

        StudioSetCommon _Common = new StudioSetCommon();
        StudioSetMasterEQ _MasterEQ = new StudioSetMasterEQ();
        StudioSetCommonChorus _CommonChorus = new StudioSetCommonChorus();
        StudioSetCommonReverb _CommonReverb = new StudioSetCommonReverb();
        StudioSetCommonMotionalSurround _CommonMotionalSurround = new StudioSetCommonMotionalSurround();
        StudioSetPartial<StudioSetMidi> _Midi = new StudioSetPartial<StudioSetMidi>(0x18001000);
        StudioSetPartial<StudioSetPart> _StudioSetParts = new StudioSetPartial<StudioSetPart>(0x18002000);
        StudioSetPartial<StudioSetPartEQ> _PartsEQ = new StudioSetPartial<StudioSetPartEQ>(0x18005000);

        public delegate void PartChangedEventHandler(object sender, IntegraPartChangeEventArgs e);
        public delegate void ToneChangedEventHandler(object sender, IntegraToneChangedEventArgs e);
        
        public event PartChangedEventHandler PartChanged;
        public event ToneChangedEventHandler ToneChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new connected <see cref="StudioSet"/> instance.
        /// </summary>
        /// <remarks><i>Studio set listens for studio set part 1 to initialize the selected tone property.</i></remarks>
        public StudioSet() : base(0x18002000, 0x0000004D) { }

        #endregion

        #region Properties

        public virtual Tone Tone
        {
            get { return _SelectedTone; }
            set
            {
                if (_SelectedTone != value)
                {
                    ToneChanged?.Invoke(this, new IntegraToneChangedEventArgs(_SelectedTone, value));

                    _SelectedTone = value;
                    
                    Parts[(int)SelectedPart].ToneBankSelectMSB = value.MSB;
                    Parts[(int)SelectedPart].ToneBankSelectLSB = value.LSB;
                    Parts[(int)SelectedPart].ToneProgramNumber = value.PC;
                    Parts[(int)SelectedPart].Reinitialize();
                    
                    ToneType = IntegraToneExtensions.Type(value.MSB);

                    NotifyPropertyChanged();
                }
            }
        }

        public virtual IntegraToneTypes ToneType
        {
            get { return _ToneType; }
            set
            {
                _ToneType = value;
                NotifyPropertyChanged();
            }
        }

        public virtual IntegraMFXTypes MFXType
        {
            get
            {
                if (MFXDataContext != null)
                    return MFXDataContext.MFX.Type;

                return IntegraMFXTypes.Thru;
            }

            set
            {
                MFXDataContext.MFX.Type = value;
                NotifyPropertyChanged();

            }
        }

        public virtual ToneMFX MFXContext
        {
            get { return Parts[(int)SelectedPart].TemporaryTone.MFX; }

        }

        public virtual IToneMFX MFXDataContext
        {
            get
            {
                return Parts[(int)SelectedPart].TemporaryTone;
            }
        }

        /// <summary>
        /// Gets or sets the selected partial.
        /// </summary>
        public virtual IntegraParts SelectedPart
        {
            get { return _SelectedPart; }
            set
            {
                // Change selected tone
                // Change tone
                // Change MFX
                if (_SelectedPart != value)
                {
                    PartChanged?.Invoke(this, new IntegraPartChangeEventArgs(_SelectedPart, value));

                    _SelectedPart = value;

                    Tone = new Tone(Parts[(int)value].ToneBankSelectMSB, Parts[(int)value].ToneBankSelectLSB, Parts[(int)value].ToneProgramNumber);

                    //NotifyPropertyChanged(nameof(MFXDataContext));
                    NotifyPropertyChanged();
                }
            }
        }

        #region Properties

        public StudioSetCommon Common
        {
            get { return _Common; }
        }

        public StudioSetCommonChorus CommonChorus
        {
            get { return _CommonChorus; }
        }

        // TODO: Temporary virtual
        public StudioSetCommonReverb CommonReverb
        {
            get { return _CommonReverb; }
        }

        public StudioSetCommonMotionalSurround CommonMotionalSurround
        {
            get { return _CommonMotionalSurround; }
        }

        public StudioSetMasterEQ MasterEQ
        {
            get { return _MasterEQ; }
        }

        public StudioSetPartial<StudioSetMidi> MIDI
        {
            get { return _Midi; }
        }

        public StudioSetPartial<StudioSetPart> Parts
        {
            get { return _StudioSetParts; }
        }

        public StudioSetPartial<StudioSetPartEQ> PartsEQ
        {
            get { return _PartsEQ; }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Stores the currently selected tone into the database.
        /// </summary>
        public void SaveFavorite()
        {
            Debug.Print($"[{nameof(StudioSet)}.{nameof(SaveFavorite)}]");

            Tone.Insert();
            NotifyPropertyChanged(nameof(Tone));
        }

        #endregion

        #region Overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            if (!IsInitialized)
            {
                IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

                // Studio Set Part
                if ((syx.Address & 0xFFFFF000) == (Address & 0xFFFFF000))
                {
                    // Partial
                    if (((syx.Address & 0x00000F00) >> 8) == (uint)SelectedPart)
                    {
                        Initialize(syx.Data);
                        Console.WriteLine(syx);
                    }
                }
            }
        }

        /// <summary>
        /// Override to only initialize the <see cref="Tone"/> property.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                Tone = new Tone(data[6], data[7], data[8]);

                IsInitialized = true;

                Device.Instance.MidiInputDevice.SystemExclusiveReceived -= SystemExclusiveReceived;
            }

            return IsInitialized;
        }

        #endregion
    }
}
