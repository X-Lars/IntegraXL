using IntegraXL.Core;
using System.Reflection;
using System.Threading.Tasks;

namespace IntegraXL.Models
{
    [Integra(0x00000000, 0x00100000)]
    public class PCMSynthTone : IntegraModel<PCMSynthTone>
    {
        //private PCMSynthTonePMT _PMT;
        //private IntegraPCMSynthTonePartialCollection _Partials;
        //private PCMSynthToneCommon02 _Common02;

        //private IntegraBasePCMSynthTonePartial<PCMSynthTonePartial> _Partials;

        //public PCMSynthTone(IntegraAddress address, IntegraParts part) : base(address + 0x00003000, 0x0000003C)
        public PCMSynthTone(TemporaryTone tone) : base(tone.Device)
        {
            Address += tone.Address;

            //Requests.Add(GetType().GetCustomAttribute<ModelAddress>().Request);

            Common = new PCMSynthToneCommon(this);
            //PMT = new PCMSynthTonePMT(this);
            //Partials = new IntegraPCMSynthTonePartialCollection(this);
            //Common02 = new PCMSynthToneCommon02(this);
        }

        public override bool IsInitialized 
        { 
            get => Common.IsInitialized; 
            protected internal set => base.IsInitialized = value; 
        }

        public PCMSynthToneCommon Common { get; }

        //public PCMSynthTonePMT PMT
        //{
        //    get { return _PMT; }
        //    set
        //    {
        //        if (_PMT != value)
        //        {
        //            _PMT = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //public IntegraPCMSynthTonePartialCollection Partials
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
        ////public IntegraBasePCMSynthTonePartial<PCMSynthTonePartial> Partials
        ////{
        ////    get { return _Partials; }
        ////}

        //public PCMSynthToneCommon02 Common02
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
