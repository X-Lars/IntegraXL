using Integra.Core;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public class Tone : IntegraBase<Tone>
    {
        public Tone(byte msb, byte lsb, byte pc) : base(0x0F000402)
        {
            ID = (uint)((lsb % 64) * 128) + pc + 1;
            MSB = msb;
            LSB = lsb;
            PC = pc;

            ToneBank = this.ToneBank();
            IsUserTone = this.IsUserTone();
            IsExpansion = this.IsExpansion();

            Requests.Add(new IntegraRequest(MSB, LSB, PC, 0x01));

            Initialize();
        }

        public Tone(IntegraTone tone) : base(0x0F000402)
        {
            ID = tone.ID;
            MSB = tone.MSB;
            LSB = tone.LSB;
            PC = tone.PC;
            Category = tone.Category;
            Name = tone.Name;
            ToneBank = this.ToneBank();
            IsUserTone = this.IsUserTone();
            IsExpansion = this.IsExpansion();
            Requests.Add(new IntegraRequest(MSB, LSB, PC, 0x01));

            //Initialize();
        }

       
        public uint ID { get; set; }
        public new string Name { get; set; }
        public byte MSB { get; set; }
        public byte LSB { get; set; }
        public byte PC { get; set; }
        public IntegraToneCategories Category { get; set; }
        public IntegraToneBanks ToneBank { get; set; }
        public bool IsUserTone { get; set; }
        public bool IsExpansion { get; set; }

        internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            if(!IsInitialized)
            {
                IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

                if(syx.Address == Address)
                {
                    if(Initialize(syx.Data))
                        Device.Instance.ReportProgress(new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                }
            }
            else
            {
                Console.WriteLine("Change Tone!!!");
            }
        }

        protected override bool Initialize(byte[] data)
        {
            if(!IsInitialized)
            {
                Name = Encoding.ASCII.GetString(data, 5, 16);
                Category = (IntegraToneCategories)data[3];

                Device.Instance.MidiInputDevice.SystemExclusiveReceived -= SystemExclusiveReceived;
                IsInitialized = true;
            }

            return IsInitialized;
        }
    }
}
