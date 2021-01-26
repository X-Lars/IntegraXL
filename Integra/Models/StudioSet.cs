using Integra.Core;
using Integra.Core.Interfaces;
using Integra.Database;
using MidiXL;
using System;
using System.Collections.Generic;

namespace Integra.Models
{

    // TODO: Make only the studio set report progress
    public class StudioSet : IntegraBase<StudioSet>
    {
        private IntegraParts _SelectedPart = IntegraParts.Part01;

        IntegraBasePartial<StudioSetPart> _StudioSetParts = new IntegraBasePartial<StudioSetPart>(0x18002000, 0x00004D);
        IntegraBasePartial<StudioSetMidi> _Midi = new IntegraBasePartial<StudioSetMidi>(0x18001000, 0x00000001);


        StudioSetCommon _Common = new StudioSetCommon();


        public delegate void PartChangeEventHandler(object sender, IntegraPartChangeEventArts e);

        public event PartChangeEventHandler PartChanged;

        public StudioSet() : base(0x18002000, 0x0000004D)
        {
        }

        public IntegraBasePartial<StudioSetMidi> MIDI
        {
            get { return _Midi; }
            set
            {
                _Midi = value;
                NotifyPropertyChanged();
            }
        }

        public StudioSetCommon Common
        {
            get { return _Common; }
            set
            {
                if(_Common != value)
                {
                    _Common = value;
                    NotifyPropertyChanged();
                }
            }
        }

        internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
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
                //Save();
            }

            return IsInitialized;
        }

        private Tone _SelectedTone;

        public Tone Tone
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
        private IntegraToneTypes _ToneType = IntegraToneTypes.SuperNATURALAcousticTone;

        public IntegraToneTypes ToneType
        {
            get { return _ToneType; }
            set
            {
                _ToneType = value;
                NotifyPropertyChanged();
            }
        }

        public IntegraMFXTypes MFXType
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

        public IToneMFX MFXDataContext
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

        public IntegraParts SelectedPart
        {
            get { return _SelectedPart; }
            set
            {
                if(_SelectedPart != value)
                {
                    _SelectedPart = value;

                    Tone = new Tone(Parts[(int)value].ToneBankSelectMSB, Parts[(int)value].ToneBankSelectLSB, Parts[(int)value].ToneProgramNumber);

                    NotifyPropertyChanged(nameof(MFXDataContext));
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraBasePartial<StudioSetPart> Parts
        {
            get { return _StudioSetParts; }
        }

        public override void Load(int id)
        {
            // Temp for testing
            //Device.ID = 1;

            // Temporary fixed ID
            base.Load(id);
            
        }

        public void SaveFavorite()
        {

            Console.WriteLine($"StudioSet Save Tone!");
            Tone.Save();
        }
        //public override void Save()
        //{
        //    Console.WriteLine("SAVE STUDIO SET CALLED");
            
        //    List<SQLParameter> parameters = new List<SQLParameter>();

        //    // TODO: Remo
        //    parameters.Add(new SQLParameter(0, typeof(int), nameof(StudioSetCommon), Device.Session.SessionID));
        //    //parameters.Add(new SQLParameter(0, typeof(int), "StudioSetCommonChorus",  Device.ID));
        //    //parameters.Add(new SQLParameter(0, typeof(int), "StudioSetCommonReverb",  Device.ID));
        //    //parameters.Add(new SQLParameter(0, typeof(int), "StudioSetCommonMotionalSurround",  Device.ID));
        //    //parameters.Add(new SQLParameter(0, typeof(int), "StudioSetMasterEQ",  Device.ID));
        //    parameters.Add(new SQLParameter(0, typeof(int), nameof(StudioSetMidi), Device.Session.SessionID));
        //    parameters.Add(new SQLParameter(0, typeof(int), nameof(StudioSetPart), Device.Session.SessionID));
        //    //parameters.Add(new SQLParameter(0, typeof(int), "StudioSetPartEQ",  Device.ID));

        //    DataAccess.Save(this, parameters, false, false);


        //    base.Save();
            
        //}
    }
}
