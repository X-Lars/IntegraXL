using Integra.Core;
using Integra.Core.Interfaces;
using Integra.Database;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integra.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Favorites")]
    public class Tone : IntegraBase<Tone>, IIntegraDataClass
    {
        #region Fields

        private int _ID;
        private string _Name;
        private byte _MSB;
        private byte _LSB;
        private byte _PC;
        private IntegraToneCategories _Category;

        #endregion

        #region Constructor

        public Tone(byte msb, byte lsb, byte pc) : base(0x0F000402)
        {
            ID = ((lsb % 64) * 128) + pc + 1;
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

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ID of the tone.
        /// </summary>
        public int ID 
        {
            get { return _ID; } 
            private set
            {
                _ID = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the name of the tone.
        /// </summary>
        public new string Name 
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged(nameof(Name), false);
            }
        }

        /// <summary>
        /// Gets the most significant byte of the tone request.
        /// </summary>
        public byte MSB
        {
            get { return _MSB; }
            set
            {
                if(_MSB != value)
                {
                    _MSB = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the least significant byte of the tone request.
        /// </summary>
        public byte LSB
        {
            get { return _LSB; }
            set
            {
                if(_LSB != value)
                {
                    _LSB = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the propgram change of the tone request.
        /// </summary>
        public byte PC
        {
            get { return _PC; }
            set
            {
                if (_PC != value)
                {
                    _PC = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the category associated with the tone.
        /// </summary>
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

        #endregion

        #region Overrides

        internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            if (!IsInitialized)
            {
                IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

                if(syx.Address == Address)
                {
                    Console.WriteLine($"[{GetType().Name}.{nameof(SystemExclusiveReceived)}] {syx}");

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
                Name = Encoding.ASCII.GetString(data, 5, 12);
                Category = (IntegraToneCategories)data[3];

                Device.Instance.MidiInputDevice.SystemExclusiveReceived -= SystemExclusiveReceived;
                IsInitialized = true;
            }

            return IsInitialized;
        }




        public override void Save()
        {
            List<SQLParameter> parameters = new List<SQLParameter>();

            parameters.Add(new SQLParameter(typeof(byte), nameof(MSB), MSB));
            parameters.Add(new SQLParameter(typeof(byte), nameof(LSB), LSB));
            parameters.Add(new SQLParameter(typeof(byte), nameof(PC), PC));

            if (DataAccess.Exists(this, parameters))
            {
                Console.WriteLine("Tone already favorited");
                return;
            }

            parameters.Clear();
            parameters.Add(new SQLParameter(typeof(int),    nameof(ID), ID));
            parameters.Add(new SQLParameter(typeof(string), nameof(Name), Name));
            parameters.Add(new SQLParameter(typeof(byte),   nameof(MSB), MSB));
            parameters.Add(new SQLParameter(typeof(byte),   nameof(LSB), LSB));
            parameters.Add(new SQLParameter(typeof(byte),   nameof(PC), PC));
            parameters.Add(new SQLParameter(typeof(byte),   nameof(Category), (byte)Category));

            DataAccess.Save(this, parameters, false);
            
        }

        public override void Load(int id)
        {
            //base.Load(id);
        }

        public override void Delete(int id)
        {
            //base.Delete(id);
        }

        public override void Truncate()
        {
            //base.Truncate();
        }

        #endregion
    }
}
