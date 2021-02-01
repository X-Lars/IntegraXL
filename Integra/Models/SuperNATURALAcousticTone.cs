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
    public class SuperNATURALAcousticTone : IntegraBase<SuperNATURALAcousticTone>//, IToneMFX
    {
        private SuperNATURALAcousticToneCommon _Common;
        //private ToneMFX _MFX;

        public SuperNATURALAcousticTone(IntegraAddress address) : base(address)
        {
            Name = "SuperNATURAL Acoustic Tone";

            _Common = new SuperNATURALAcousticToneCommon(address);
            //_MFX = new ToneMFX(address);
        }

        public SuperNATURALAcousticToneCommon Common
        {
            get { return _Common; }
        }

        //public ToneMFX MFX
        //{
        //    get { return _MFX; }
        //    //set
        //    //{
        //    //    if(_MFX != value)
        //    //    {
        //    //        _MFX = value;
        //    //        NotifyPropertyChanged();
        //    //    }
        //    //}
        //}

        //internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        //{
        //    IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

        //    if(!IsInitialized)
        //    {
        //        if(syx.Address == Address)
        //        {
        //            if (Initialize(syx.Data))
        //                Device.Instance.ReportProgress(new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
        //        }
        //        else if((syx.Address & 0xFFFF0000) == (Address & 0xFFFF0000))
        //        {
        //            InitializeField(syx);
        //        }

        //    }
        //}
    }
}
