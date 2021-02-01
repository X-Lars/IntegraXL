using Integra.Core;
using Integra.Core.Interfaces;
using MidiXL;
using System;

namespace Integra.Models
{

    public class TemporaryTone : IntegraBase<TemporaryTone>, IIntegraPartial, IToneMFX
    {
        #region Fields

        private IntegraParts _Part;
        private IntegraToneTypes _Type;
        private ToneMFX _MFX;

        private SuperNATURALAcousticTone _SuperNaturalAcousticTone;
        private SuperNATURALSynthTone _SuperNaturalSynthTone;
        private SuperNATURALDrumKit _SuperNaturalDrumKit;
        private PCMSynthTone _PCMSynthTone;
        private PCMDrumKit _PCMDrumKit;

        #endregion

        #region Constructor

        public TemporaryTone(IntegraParts part, IntegraToneTypes type) : base(0x19000000)
        {
            // Offset the base address with the selected part
            // 0x19, 0x19, 0x19, 0x19, 0x20, ...
            Address += new byte[] { (byte)((int)part / 4), 0x00, 0x00, 0x00 };

            // 0x00, 0x20, 0x40, 0x60, 0x00, ...
            Address += new byte[] { 0x00, (byte)((int)part % 4 * 0x20), 0x00, 0x00 };


            Address += (uint)type;

            Part = part;
            Type = type;

            MFX = new ToneMFX(Address, part);

            Console.WriteLine($"[{nameof(TemporaryTone)}] {Address} - {Type}");
        }

        #endregion

        protected override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if (!IsInitialized)
            {
                if (syx.Address == Address)
                {
                    Console.WriteLine(syx.Address);
                    // Exact match
                    if (Initialize(syx.Data))
                    {
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                    }
                    else
                    {
                        InitializeField(syx);
                    }
                }
                else if ((syx.Address & 0xFFFFFF00) == (Address & 0xFFFFFF00))
                {
                    InitializeField(syx);
                }
            }
        }

        protected override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                //base.Initialize(data);

                switch (Type)
                {
                    case IntegraToneTypes.SuperNATURALAcousticTone:
                        SuperNATURALAcousticTone = new SuperNATURALAcousticTone(Address);
                        //SuperNATURALAcousticTone.Initialize();
                        break;
                    case IntegraToneTypes.SuperNATURALSynthTone:
                        SuperNATURALSynthTone = new SuperNATURALSynthTone(Address);
                        //SuperNATURALSynthTone.Initialize();
                        break;
                    case IntegraToneTypes.SuperNATURALDrumkit:
                        SuperNATURALDrumKit = new SuperNATURALDrumKit(Address);
                        //SuperNATURALDrumKit.Initialize();
                        break;
                    case IntegraToneTypes.PCMSynthTone:
                        PCMSynthTone = new PCMSynthTone(Address);
                        //PCMSynthTone.Initialize();
                        break;
                    case IntegraToneTypes.PCMDrumkit:
                        PCMDrumKit = new PCMDrumKit(Address);
                        //PCMDrumKit.Initialize();
                        break;
                }

                NotifyPropertyChanged(string.Empty, false);
                IsInitialized = true;

                Console.WriteLine($"[{nameof(TemporaryTone)}] {Address} - {Type} [Initialized]");
            }

            return IsInitialized;
        }

        public IntegraParts Part
        {
            get { return _Part; }
            set
            {
                if(_Part != value)
                {
                    _Part = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraToneTypes Type
        {
            get { return _Type; }
            private set
            {
                if (_Type != value)
                {
                    _Type = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ToneMFX MFX
        {
            get { return _MFX; }
            set
            {
                _MFX = value;
                NotifyPropertyChanged();
            }
        }

        public virtual SuperNATURALAcousticTone SuperNATURALAcousticTone
        {
            get { return _SuperNaturalAcousticTone; }
            private set
            {
                _SuperNaturalAcousticTone = value;
                NotifyPropertyChanged();
            }
        }

        public virtual SuperNATURALSynthTone SuperNATURALSynthTone
        {
            get { return _SuperNaturalSynthTone; }
            private set
            {
                _SuperNaturalSynthTone = value;
                NotifyPropertyChanged();
            }
        }

        public virtual SuperNATURALDrumKit SuperNATURALDrumKit
        {
            get { return _SuperNaturalDrumKit; }
            private set
            {
                _SuperNaturalDrumKit = value;
                NotifyPropertyChanged();
            }
        }

        public virtual PCMSynthTone PCMSynthTone
        {
            get { return _PCMSynthTone; }
            private set
            {
                _PCMSynthTone = value;
                NotifyPropertyChanged();
            }
        }

        public virtual PCMDrumKit PCMDrumKit
        {
            get { return _PCMDrumKit; }
            private set
            {
                _PCMDrumKit = value;
                NotifyPropertyChanged();
            }
        }
    }
}
