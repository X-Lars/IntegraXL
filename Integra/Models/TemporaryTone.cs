using Integra.Core;
using Integra.Core.Interfaces;
using Integra.Database;
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
        private TemporaryTone() { }
        public TemporaryTone(IntegraParts part, IntegraToneTypes type) : base(0x19000000)
        {
            // Offset the base address with the selected part
            // 0x19, 0x19, 0x19, 0x19, 0x20, ...
            Address += new byte[] { (byte)((int)part / 4), 0x00, 0x00, 0x00 };

            // 0x00, 0x20, 0x40, 0x60, 0x00, ...
            Address += new byte[] { 0x00, (byte)((int)part % 4 * 0x20), 0x00, 0x00 };


            Address += (uint)type;

            _Part = part;
            Type = type;

            MFX = new ToneMFX(Address, part);

            Console.WriteLine($"[{nameof(TemporaryTone)}] {Address} - {Type}");

            switch (Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    SuperNATURALAcousticTone = new SuperNATURALAcousticTone(Address, part);
                    break;

                case IntegraToneTypes.SuperNATURALSynthTone:
                    SuperNATURALSynthTone = new SuperNATURALSynthTone(Address, part);
                    break;

                case IntegraToneTypes.SuperNATURALDrumkit:
                    SuperNATURALDrumKit = new SuperNATURALDrumKit(Address, part);
                    break;

                case IntegraToneTypes.PCMSynthTone:
                    PCMSynthTone = new PCMSynthTone(Address, part);
                    break;

                case IntegraToneTypes.PCMDrumkit:
                    PCMDrumKit = new PCMDrumKit(Address, part);
                    break;
            }
        }

        #endregion

    

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

        public SuperNATURALAcousticTone SuperNATURALAcousticTone
        {
            get { return _SuperNaturalAcousticTone; }
            private set
            {
                _SuperNaturalAcousticTone = value;
                NotifyPropertyChanged();
            }
        }

        public SuperNATURALSynthTone SuperNATURALSynthTone
        {
            get { return _SuperNaturalSynthTone; }
            private set
            {
                _SuperNaturalSynthTone = value;
                NotifyPropertyChanged();
            }
        }

        public SuperNATURALDrumKit SuperNATURALDrumKit
        {
            get { return _SuperNaturalDrumKit; }
            private set
            {
                _SuperNaturalDrumKit = value;
                NotifyPropertyChanged();
            }
        }

        public PCMSynthTone PCMSynthTone
        {
            get { return _PCMSynthTone; }
            private set
            {
                _PCMSynthTone = value;
                NotifyPropertyChanged();
            }
        }

        public PCMDrumKit PCMDrumKit
        {
            get { return _PCMDrumKit; }
            private set
            {
                _PCMDrumKit = value;
                NotifyPropertyChanged();
            }
        }

        #region IIntegraDataClass

        public override void Insert()
        {
            DataAccess.Insert(this);

            switch (Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    SuperNATURALAcousticTone.Insert();
                    break;
                case IntegraToneTypes.SuperNATURALSynthTone:
                    SuperNATURALSynthTone.Insert();
                    break;
                case IntegraToneTypes.SuperNATURALDrumkit:
                    SuperNATURALDrumKit.Insert();
                    break;
                case IntegraToneTypes.PCMSynthTone:
                    PCMSynthTone.Insert();
                    break;
                case IntegraToneTypes.PCMDrumkit:
                    PCMDrumKit.Insert();
                    break;
            }
        }

        public override void Select(int id)
        {
            DataAccess.Select(this, id);

            // TODO: Select change tone type
            switch(Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    SuperNATURALAcousticTone.Select(id);
                    break;
                case IntegraToneTypes.SuperNATURALSynthTone:
                    SuperNATURALSynthTone.Select(id);
                    break;
                case IntegraToneTypes.SuperNATURALDrumkit:
                    SuperNATURALDrumKit.Select(id);
                    break;
                case IntegraToneTypes.PCMSynthTone:
                    PCMSynthTone.Select(id);
                    break;
                case IntegraToneTypes.PCMDrumkit:
                    PCMDrumKit.Select(id);
                    break;
            }
        }

        public override void Update()
        {
            DataAccess.Update(this);

            // TODO: Update when tone type is changed, delete old tone type insert new tone type
            switch(Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    SuperNATURALAcousticTone.Update();
                    break;
                case IntegraToneTypes.SuperNATURALSynthTone:
                    SuperNATURALSynthTone.Update();
                    break;
                case IntegraToneTypes.SuperNATURALDrumkit:
                    SuperNATURALDrumKit.Update();
                    break;
                case IntegraToneTypes.PCMSynthTone:
                    PCMSynthTone.Update();
                    break;
                case IntegraToneTypes.PCMDrumkit:
                    PCMDrumKit.Update();
                    break;
            }
        }

        public override void Delete()
        {
            DataAccess.Delete(this);

            switch(Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    SuperNATURALAcousticTone.Delete();
                    break;
                case IntegraToneTypes.SuperNATURALSynthTone:
                    SuperNATURALSynthTone.Delete();
                    break;
                case IntegraToneTypes.SuperNATURALDrumkit:
                    SuperNATURALDrumKit.Delete();
                    break;
                case IntegraToneTypes.PCMSynthTone:
                    PCMSynthTone.Delete();
                    break;
                case IntegraToneTypes.PCMDrumkit:
                    PCMDrumKit.Delete();
                    break;
            }
        }

        public override void Truncate()
        {
            DataAccess.Truncate(this);

            //DataAccess.Truncate<SuperNATURALAcousticTone>();

            switch(Type)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    SuperNATURALAcousticTone.Truncate();
                    break;
                case IntegraToneTypes.SuperNATURALSynthTone:
                    SuperNATURALSynthTone.Truncate();
                    break;
                case IntegraToneTypes.SuperNATURALDrumkit:
                    SuperNATURALDrumKit.Truncate();
                    break;
                case IntegraToneTypes.PCMSynthTone:
                    PCMSynthTone.Truncate();
                    break;
                case IntegraToneTypes.PCMDrumkit:
                    PCMDrumKit.Truncate();
                    break;
            }
        }

        #endregion
    }
}
