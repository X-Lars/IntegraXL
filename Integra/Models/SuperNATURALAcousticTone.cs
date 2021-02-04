using Integra.Core;
using Integra.Core.Interfaces;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// TODO: Parameter context based on tone category (See Roland parameter reference)
namespace Integra.Models
{
    public class SuperNATURALAcousticTone : IntegraBase<SuperNATURALAcousticTone>, IIntegraPartial
    {
        private IntegraParts _Part;

        private SuperNATURALAcousticToneCommon _Common;

        public SuperNATURALAcousticTone(IntegraAddress address, IntegraParts part) : base(address)
        {
            Name = "SuperNATURAL Acoustic Tone";

            Part = part;
            Common = new SuperNATURALAcousticToneCommon(address, part);
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

        public SuperNATURALAcousticToneCommon Common
        {
            get { return _Common; }
            set
            {
                if(Common != value)
                {
                    _Common = value;
                    NotifyPropertyChanged();
                }
            }
        }

        
    }
}
