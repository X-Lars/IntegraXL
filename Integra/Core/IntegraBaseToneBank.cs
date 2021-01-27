using Integra.Database;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    /// <summary>
    /// Base collection for all INTEGRA-7 tone banks
    /// </summary>
    public abstract class IntegraBaseToneBank : IntegraBaseCollection<IntegraBaseToneBank, IntegraTone>
    {
        #region Fields

        /// <summary>
        /// Tracks whether initialization can be done from the database.
        /// </summary>
        protected bool _IsPersistent = false;

        #endregion

        #region Constructor

        internal protected IntegraBaseToneBank() : base(0x0F000402) { }

        public IntegraBaseToneBank(byte msb, byte lsb, uint size) : base(0x0F000402)
        {
            MSB = msb;
            LSB = lsb;
            Size = size;

            // Generate the requests to retreive the tones
            for (uint i = 0; i < size; i += 0x40)
            {
                // IntegraBaseToneBank(0x57, 0x00, 256)

                // request[0] = 0x57, 0x00, 0x00, 0x40;   0..63
                // request[1] = 0x57, 0x00, 0x40, 0x40;  64..127
                // request[2] = 0x57, 0x01, 0x00, 0x40; 128..191
                // request[3] = 0x57, 0x01, 0x40, 0x40; 192..255

                if (i + 0x40 > size)
                {
                    // The last request size is smaller than the BASE_TONE_BANK_REQUEST_SIZE and must be truncated to the correct size
                    Requests.Add(new IntegraRequest(msb, (byte)(lsb + (i / (0x40 * 2))), (byte)((i / 0x40) % 2 == 0 ? 0x00 : 0x40), (byte)(0x40 - (i + 0x40 - size))));
                }
                else
                {
                    Requests.Add(new IntegraRequest(msb, (byte)(lsb + (i / (0x40 * 2))), (byte)((i / 0x40) % 2 == 0 ? 0x00 : 0x40), 0x40));
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the most significant byte of the tone bank request.
        /// </summary>
        public byte MSB { get; internal protected set; }

        /// <summary>
        /// Gets the least significant byte of the tone bank request.
        /// </summary>
        public byte LSB { get; internal protected set; }

        /// <summary>
        /// Gets the size of the tone bank.
        /// </summary>
        public uint Size { get; internal protected set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Overrides the default behaviour to initialize the data structure from the data base when possible.
        /// </summary>
        public override void Initialize()
        {
            // TODO: User tone banks should always be loaded from the INTEGRA-7 because they can be changed

            _IsPersistent = DataAccess.GetCount(this) == Size;

            if (!_IsPersistent)
            {
                Debug.Print($"[{nameof(IntegraBaseToneBank)}.{nameof(Initialize)}] {GetType().Name} ");

                // Ensures table is empty
                DataAccess.Truncate(this);

                // Initialize the tone bank from the INTEGRA-7
                base.Initialize();
            }
            else
            {
                Debug.Print($"[{nameof(IntegraBaseToneBank)}.{nameof(Initialize)}] {GetType().Name}");

                // Initialize the tone bank from the database.
                DataAccess.Select(this, new IntegraTone()).ForEach(Collection.Add);
            }
        }

        
        protected override uint DataSize
        {
            get { return Size; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            if (IsInitialized)
                return;

            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if(syx.Address == Address)
            {
                if(syx.MSB == MSB)
                {
                    IDCounter++;

                    if(Initialize(syx.Data))
                    {
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));

                        // Store the tone bank in the database
                        if (!_IsPersistent)
                        {
                            DataAccess.BatchInsert(this, new IntegraTone());
                        }
                    }
                    else
                    {
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Please wait...", Progress, $"Tone {IDCounter} of {DataSize}"));
                    }
                }
            }
        }

        #endregion
    }
}
