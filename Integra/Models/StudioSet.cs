using Integra.Core;
using Integra.Core.Interfaces;
using Integra.Database;
using MidiXL;
using System;
using System.Collections.Generic;
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
        IntegraBasePartial<StudioSetMidi> _Midi = new IntegraBasePartial<StudioSetMidi>(0x18001000, 0x00000001);
        IntegraBasePartial<StudioSetPart> _StudioSetParts = new IntegraBasePartial<StudioSetPart>(0x18002000, 0x00004D);

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new connected <see cref="StudioSet"/> instance.
        /// </summary>
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
                    _SelectedTone = value;

                    Parts[(int)SelectedPart].ToneBankSelectMSB = value.MSB;
                    Parts[(int)SelectedPart].ToneBankSelectLSB = value.LSB;
                    Parts[(int)SelectedPart].ToneProgramNumber = value.PC;

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

        public virtual IToneMFX MFXDataContext
        {
            get
            {
                switch (ToneType)
                {
                    case IntegraToneTypes.SuperNATURALAcousticTone:
                        return Parts[(int)SelectedPart].SuperNATURALAcousticTone;

                    case IntegraToneTypes.SuperNATURALSynthTone:
                        return Parts[(int)SelectedPart].SuperNATURALSynthTone;

                    case IntegraToneTypes.SuperNATURALDrumkit:
                        return Parts[(int)SelectedPart].SuperNATURALDrumKit;

                    case IntegraToneTypes.PCMSynthTone:
                        return Parts[(int)SelectedPart].PCMSynthTone;

                    case IntegraToneTypes.PCMDrumkit:
                        return Parts[(int)SelectedPart].PCMDrumKit;

                    default:
                        return null;
                }
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
                    _SelectedPart = value;

                    Tone = new Tone(Parts[(int)value].ToneBankSelectMSB, Parts[(int)value].ToneBankSelectLSB, Parts[(int)value].ToneProgramNumber);

                    NotifyPropertyChanged(nameof(MFXDataContext));
                    NotifyPropertyChanged();
                }
            }
        }

        #region Properties: Data Access

        public StudioSetCommon Common
        {
            get { return _Common; }
        }

        public IntegraBasePartial<StudioSetMidi> MIDI
        {
            get { return _Midi; }
            set
            {
                if(_Midi != value)
                {
                    _Midi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraBasePartial<StudioSetPart> Parts
        {
            get { return _StudioSetParts; }
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
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            // Studio Set Part
            if ((syx.Address & 0xFFFFF000) == (Address & 0xFFFFF000))
            {
                //
                if(((syx.Address & 0x00000F00) >> 8) == (uint)SelectedPart)
                {
                    Initialize(syx.Data);
                    Console.WriteLine(syx);
                }
            }
        }

        /// <summary>
        /// Override to only initialize the <see cref="Tone"/> property.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                Tone = new Tone(data[6], data[7], data[8]);

                IsInitialized = true;
            }

            return IsInitialized;
        }

        #endregion
    }
}
