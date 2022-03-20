using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using IntegraXL.Templates;
using System.Text;

namespace IntegraXL.Models
{
    [Integra(0x0F000402, 0x00000015)]
    public sealed class Tone : IntegraModel<Tone>, IBankSelect
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="Tone"/> instance.
        /// </summary>
        /// <param name="device"></param>
        internal Tone(Integra device) : base(device) { }

        /// <summary>
        /// Creates and initializes a new <see cref="Tone"/> instance providing tone information for the given <see cref="StudioSetPart"/>.
        /// </summary>
        /// <param name="studiosetpart">The <see cref="StudioSetPart"/> providing the tone's bank select.</param>
        public Tone(StudioSetPart studiosetpart) : this(studiosetpart.Device, studiosetpart) { }

        /// <summary>
        /// Creates and initializes a new <see cref="Tone"/> instance providing tone information for the given <see cref="TemporaryTone"/>.
        /// </summary>
        /// <param name="temporarytone">The <see cref="TemporaryTone"/> providing the tone's bank select.</param>
        public Tone(TemporaryTone temporarytone) : this(temporarytone.Device, temporarytone) { }

        /// <summary>
        /// Creates and initializes a new <see cref="Tone"/> instance from the given <see cref="IBankSelect"/> interface.
        /// </summary>
        /// <param name="device">The <see cref="Integra"/> to connect the model.</param>
        /// <param name="bankselect">The <see cref="IBankSelect"/> interface to initialize the model.</param>
        private Tone(Integra device, IBankSelect bankselect) : base(device)
        {
            Initialize(bankselect);
        }

        #endregion;

        #region Properties: IBankSelect

        /// <summary>
        /// Gets the (M)ost (S)ignificant (B)yte of the <see cref="Tone"/>, represents the MIDI control change bank select MSB.
        /// </summary>
        /// <remarks><i>MIDI Controller 0.</i></remarks>
        public byte MSB { get; private set; }

        /// <summary>
        /// Gets the (L)east (S)ignificant (B)yte of the <see cref="Tone"/>, represents the MIDI control change bank select LSB.
        /// </summary>
        /// <remarks><i>MIDI Controller 32.</i></remarks>
        public byte LSB { get; private set; }

        /// <summary>
        /// Gets the (P)rogram (C)hange of the <see cref="Tone"/>, represents the MIDI program change program number.
        /// </summary>
        public byte PC { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ID of the tone.
        /// </summary>
        /// <remarks><i>For display purpose only, do not use for calculations.</i></remarks>
        public int ID { get; private set; } = 0;

        /// <summary>
        /// Gets the tone name or drum kit name.
        /// </summary>
        public new string Name { get; private set; } = "Uninitialized";

        /// <summary>
        /// Gets the tone category.
        /// </summary>
        public IntegraToneCategories Category { get; private set; } = IntegraToneCategories.Unassigned;

        /// <summary>
        /// Gets the <see cref="Tone"/>'s associated tone bank.
        /// </summary>
        public IntegraToneBanks ToneBank { get; private set; } = IntegraToneBanks.Unavailable;

        /// <summary>
        /// Gets the <see cref="Tone"/>'s type.
        /// </summary>
        public IntegraToneTypes Type { get; private set; } = IntegraToneTypes.Unavailable;

        /// <summary>
        /// Gets wheter the <see cref="Tone"/> is editable.
        /// </summary>
        public bool IsEditable { get; private set; } = false;

        /// <summary>
        /// Gets the tone variation.
        /// </summary>
        /// <remarks><i>Only for GM2 tone bank and drum kit.</i></remarks>
        public int? Variation { get; private set; } = null;

        #endregion

        #region Methods

        /// <summary>
        /// Updates the <see cref="Tone"/> with the specified <see cref="IBankSelect"/> interface.
        /// </summary>
        /// <param name="bankselect">The <see cref="IBankSelect"/> interface to update the tone.</param>
        public void Update(IBankSelect bankselect)
        {
            Initialize(bankselect);
        }

        /// <summary>
        /// Initializes the <see cref="Tone"/> with the specified <see cref="IBankSelect"/> interface and enqueues a <see cref="ToneTemplate"/> request.
        /// </summary>
        /// <param name="bankselect">The <see cref="IBankSelect"/> interface to initialize the tone.</param>
        private void Initialize(IBankSelect bankselect)
        {
            MSB = bankselect.MSB;
            LSB = bankselect.LSB;
            PC  = bankselect.PC;

            IsEditable = this.IsEditable();
            ToneBank   = this.ToneBank();
            Type       = this.ToneType();

            IsInitialized = false;

            // IMPORTANT! Quick tone changes can corrupt the model initialization queue, dequeue the temporary tone as prevention
            Device.Dequeue(this);

            Device.Enqueue(this);
        }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Sends the system exclusive request to initialize the <see cref="Tone"/>.
        /// </summary>
        internal override void RequestInitialization()
        {
            Device.TransmitSystemExclusive(new IntegraSystemExclusive(Address, new IntegraRequest(MSB, LSB, PC, 0x01)));
        }

        /// <summary>
        /// Handles the <see cref="Integra.SystemExclusiveReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="Integra"/> that raised the event.</param>
        /// <param name="e">The event's associated data.</param>
        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if (e.SystemExclusive.Address == Address)
            {
                if (e.SystemExclusive.Data.Length == Size)
                {
                    if (e.SystemExclusive.Data[0] == MSB && e.SystemExclusive.Data[1] == LSB && e.SystemExclusive.Data[2] == PC)
                    {
                        Initialize(e.SystemExclusive.Data);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the <see cref="Tone"/> with data.
        /// </summary>
        /// <param name="data">The data used to initialize the <see cref="Tone"/>.</param>
        /// <returns>Always true.</returns>
        internal override bool Initialize(byte[] data)
        {
            int id = PC + 1;

            switch (ToneBank)
            {
                case IntegraToneBanks.GM2Tone:
                    Variation = LSB;
                    break;

                case IntegraToneBanks.GM2Drum:
                    id = 1;
                    Variation = PC;
                    break;

                default:
                    id += ((LSB - (int)ToneBank & 0x00FF) % 64 * 128);
                    Variation = null;
                    break;
            }

            ID = id;
            Category = (IntegraToneCategories)data[3];
            Name = Encoding.ASCII.GetString(data, 5, 12);

            return IsInitialized = true;
        }

        #endregion

        #region Overrides: Object

        /// <summary>
        /// Provides string that represents the current <see cref="Tone"/>.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{ID:0000} {Name} [{Category}] (0x{MSB:X2} 0x{LSB:X2} 0x{PC:X2})";
        }

        #endregion
    }
}
