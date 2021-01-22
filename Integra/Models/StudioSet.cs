using Integra.Core;
using Integra.Core.Interfaces;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{

    // TODO: Make only the studio set report progress
    public class StudioSet : IntegraBase<StudioSet>
    {
        StudioSetMidi[] _StudioSetMidi = new StudioSetMidi[16];
        StudioSetPart[] _StudioSetParts = new StudioSetPart[16];

        //StudioSetPart _Part = new StudioSetPart(IntegraParts.Part01);

        public delegate void PartChangeEventHandler(object sender, IntegraPartChangeEventArts e);

        public event PartChangeEventHandler PartChanged;

        public StudioSet() : base(0x18002000, 0x0000004D)
        {
            //_Part = new StudioSetPart(IntegraParts.Part01);
            for (int i = 0; i < 16; i++)
            {
                _StudioSetParts[i] = new StudioSetPart((IntegraParts)i);
            }

            //for (int i = 0; i < 16; i++)
            //{
            //    _StudioSetMidi[i] = new StudioSetMidi((IntegraParts)i);
            //}
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

                    Part.ToneBankSelectMSB = value.MSB;
                    Part.ToneBankSelectLSB = value.LSB;
                    Part.ToneProgramNumber = value.PC;

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
                if(MFXDataContext != null)
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
                        return Part.SuperNATURALAcousticTone;

                    case IntegraToneTypes.SuperNATURALSynthTone:
                        return Part.SuperNATURALSynthTone;

                    case IntegraToneTypes.SuperNATURALDrumkit:
                        return Part.SuperNATURALDrumKit;

                    case IntegraToneTypes.PCMSynthTone:
                        return Part.PCMSynthTone;

                    case IntegraToneTypes.PCMDrumkit:
                        return Part.PCMDrumKit;

                    default:
                        return null;
                }
            }
        }

        private IntegraParts _SelectedPart = IntegraParts.Part01;

        public IntegraParts SelectedPart
        {
            get { return _SelectedPart; }
            set
            {
                if(_SelectedPart != value)
                {


                    _SelectedPart = value;


                    Tone = new Tone(Part.ToneBankSelectMSB, Part.ToneBankSelectLSB, Part.ToneProgramNumber);
                    //Reinitialize();
                    //Part = new StudioSetPart(value);
                    NotifyPropertyChanged(nameof(Part));
                    NotifyPropertyChanged(nameof(MFXDataContext));
                    NotifyPropertyChanged();
                    //PartChanged?.Invoke(this, new IntegraPartChangeEventArts(_SelectedPart, value));
                }
            }
        }

        public StudioSetPart[] Parts
        {
            get { return _StudioSetParts; }
        }

        public StudioSetPart Part
        {
            get { return _StudioSetParts[(int)SelectedPart]; }
           
        }

    }
}
