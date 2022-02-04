using IntegraXL.Core;
using System.Reflection;
using System.Threading.Tasks;

namespace IntegraXL.Models
{
    [Integra(0x00100000, 0x00100000)]
    public class PCMDrumKit : IntegraModel<PCMDrumKit>
    {
        //private DrumKitCommonCompEQ _CompEQ;
        //private IntegraPCMDrumKitPartialCollection _Partials;

        ////private IntegraBasePCMDrumKitPartial<PCMDrumKitPartial> _Partials;
        //private PCMDrumKitCommon02 _Common02;
        public PCMDrumKit(TemporaryTone tone) : base(tone.Device)
        {
            Address += tone.Address;

            IsEditable = tone.IsEditable;

            if (IsEditable)
            {
                Common = new PCMDrumKitCommon(this);

                //CompEQ = new DrumKitCommonCompEQ(this);
                //Partials = new IntegraPCMDrumKitPartialCollection(this);
                //Common02 = new PCMDrumKitCommon02(this);
            }
            else
            {
                IsInitialized = true;
            }
        }

        public bool IsEditable { get; private set; }

        public override bool IsInitialized 
        { 
            get
            {
                if (!IsEditable)
                    return true;

                return Common.IsInitialized;
            }

            protected internal set => base.IsInitialized = value; 
        }

        public PCMDrumKitCommon Common { get; }

        //public DrumKitCommonCompEQ CompEQ
        //{
        //    get { return _CompEQ; }
        //    set
        //    {
        //        if (_CompEQ != value)
        //        {
        //            _CompEQ = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //public IntegraPCMDrumKitPartialCollection Partials
        //{
        //    get { return _Partials; }
        //    set
        //    {
        //        if(_Partials != value)
        //        {
        //            _Partials = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //public PCMDrumKitCommon02 Common02
        //{
        //    get { return _Common02; }
        //    set
        //    {
        //        if (_Common02 != value)
        //        {
        //            _Common02 = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
    }
}
