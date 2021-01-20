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
            _MSB = msb;
            _LSB = lsb;
            _PC = pc;

            ToneBank = this.ToneBank();
            IsUserTone = this.IsUserTone();
            IsExpansion = this.IsExpansion();

            Requests.Add(new IntegraRequest(MSB, LSB, PC, 0x01));

            Initialize();
        }

        public Tone(IntegraTone tone) : base(0x0F000402)
        {
            ID = tone.ID;
            _MSB = tone.MSB;
            _LSB = tone.LSB;
            _PC = tone.PC;
            Category = tone.Category;
            Name = tone.Name;
            ToneBank = this.ToneBank();
            IsUserTone = this.IsUserTone();
            IsExpansion = this.IsExpansion();
            Requests.Add(new IntegraRequest(MSB, LSB, PC, 0x01));

            //Initialize();
        }

        private string _Name;
        public uint ID { get; set; }
        public new string Name 
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged(nameof(Name), false);
            }
        }
        private byte _MSB;
        private byte _LSB;
        private byte _PC;

        public byte MSB
        {
            get { return _MSB; }
            set
            {
                if(_MSB != value)
                {
                    _MSB = value;
                    //UpdateRequest(value, _LSB, _PC);
                    NotifyPropertyChanged();
                }
            }
        }
        public byte LSB
        {
            get { return _LSB; }
            set
            {
                if(_LSB != value)
                {
                    _LSB = value;
                    //UpdateRequest(_MSB, value, _PC);
                    NotifyPropertyChanged();
                }
            }
        }
        public byte PC
        {
            get { return _PC; }
            set
            {
                if (_PC != value)
                {
                    _PC = value;
                    //UpdateRequest(_MSB, _LSB, value);
                    NotifyPropertyChanged();
                }
            }
        }
        private IntegraToneCategories _Category;

        public IntegraToneCategories Category
        {
            get { return _Category; }
            set
            {
                if(_Category != value)
                {
                    _Category = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public IntegraToneBanks ToneBank { get; set; }
        public bool IsUserTone { get; set; }
        public bool IsExpansion { get; set; }

        private IntegraToneTypes _ToneType;
        public IntegraToneTypes ToneType
        {
            get { return _ToneType; }
            set
            {
                _ToneType = value;
                NotifyPropertyChanged();
            }
        }
        //private void UpdateRequest(byte msb, byte lsb, byte pc)
        //{
        //    Requests.Clear();
        //    Requests.Add(new IntegraRequest(msb, lsb, pc, 0x01));
        //}

        //internal void Reinitialize()
        //{
           

        //    Requests.Add(new IntegraRequest(MSB, LSB, PC, 0x01));
        //    IsInitialized = false;

        //    Initialize();
        //}

        internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            if (!IsInitialized)
            {
                IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

                if(syx.Address == Address)
                {
                    Console.WriteLine($"[{GetType().Name}.{nameof(SystemExclusiveReceived)}] {syx}");
                    //Initialize(syx.Data);
                    if (Initialize(syx.Data))
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                }
            }
        }

        protected override bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                MSB = data[0];
                LSB = data[1];
                PC = data[2];
                Name = Encoding.ASCII.GetString(data, 5, 16);
                Category = (IntegraToneCategories)data[3];

                Device.Instance.MidiInputDevice.SystemExclusiveReceived -= SystemExclusiveReceived;
                IsInitialized = true;
            }

            return IsInitialized;
        }
    }
}
