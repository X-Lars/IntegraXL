using IntegraXL.Core;
using System.Reflection;
using System.Threading.Tasks;

namespace IntegraXL.Models
{
    [Integra(0x00030000, 0x00100000)]
    public class SuperNATURALDrumKit : IntegraModel
    {
        //private IntegraSuperNATURALDrumKitNoteCollection _Notes;

        public SuperNATURALDrumKit(TemporaryTone tone) : base(tone.Device)
        {
            Address += tone.Address;
            //Requests.Add(GetType().GetCustomAttribute<ModelAddress>().Request);

            Common = new SuperNATURALDrumKitCommon(this);
            CompEQ = new DrumKitCommonCompEQ(this);
            //Notes = new IntegraSuperNATURALDrumKitNoteCollection(this);
        }

        //public override bool IsInitialized
        //{
        //    get { return Notes != null && Notes.IsInitialized; }
        //}
        public override bool IsInitialized 
        { 
            // TODO: Notes is initialized
            get => CompEQ.IsInitialized; 

            protected internal set => base.IsInitialized = value; 
        }
        public SuperNATURALDrumKitCommon Common { get; }
        public DrumKitCommonCompEQ CompEQ { get; }

        //public IntegraSuperNATURALDrumKitNoteCollection Notes
        //{
        //    get { return _Notes; }
        //    set
        //    {
        //        if(_Notes != value)
        //        {
        //            _Notes = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
    }
}
