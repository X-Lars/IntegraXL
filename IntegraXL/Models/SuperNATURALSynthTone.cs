using IntegraXL.Core;
using System.Reflection;
using System.Threading.Tasks;

namespace IntegraXL.Models
{
    [Integra(0x00010000, 0x00100000)]
    public class SuperNATURALSynthTone : IntegraModel<SuperNATURALSynthTone>
    {
        //private IntegraSuperNATURALSynthTonePartialCollection _Partials;

        public SuperNATURALSynthTone(TemporaryTone tone) : base(tone.Device)
        {
            Address += tone.Address;
            
            Common = new SuperNATURALSynthToneCommon(this);
            //Partials = new IntegraSuperNATURALSynthTonePartialCollection(this);
        }

        public override bool IsInitialized 
        { 
            // TODO: Partials is initialzed
            get => Common.IsInitialized; 
            protected internal set => base.IsInitialized = value; 
        }

        public SuperNATURALSynthToneCommon Common { get; }

        //public IntegraSuperNATURALSynthTonePartialCollection Partials
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
    }
}
