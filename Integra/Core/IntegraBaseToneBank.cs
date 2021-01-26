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
    public abstract class IntegraBaseToneBank : IntegraBaseCollection<IntegraBaseToneBank, IntegraTone>
    {
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

        public override void Initialize()
        {
            Debug.Print($"[{nameof(IntegraBaseToneBank)}.{nameof(Initialize)}] {GetType().Name}");
            // TODO: Check if exists in database

            _IsPersisted = DataAccess.GetCount(this) == Size;

            if(!_IsPersisted)
            {
                // Ensure table is empty
                DataAccess.Truncate(this);

                base.Initialize();

                // insert into database after initializaiont
            }
            else
            {
                // Load from database
                DataAccess.Select(this, new IntegraTone()).ForEach(Collection.Add);
            }
        }

        // Exists in database?
        protected bool _IsPersisted = false;

        public byte MSB { get; internal protected set; }
        public byte LSB { get; internal protected set; }
        public uint Size { get; internal protected set; }

        protected override uint DataSize
        {
            get { return Size; }
        }

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
                        if (!_IsPersisted)
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

    }
}
