using IntegraXL.Core;
using IntegraXL.Extensions;
using IntegraXL.Interfaces;
using System.ComponentModel;
using System.Text;

namespace IntegraXL.Models
{
    public sealed class Tone : IBankSelect, INotifyPropertyChanged
    {
        #region Fields

        private readonly Integra _Device;

        #endregion

        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Constructor

        internal Tone(Integra device)
        {
            _Device = device;
            _Device.SystemExclusiveReceived += SystemExclusiveReceived;
        }

        public Tone(StudioSetPart studiosetpart) : this(studiosetpart.Device, studiosetpart) { }
        public Tone(TemporaryTone temporarytone) : this(temporarytone.Device, temporarytone) { }

        private Tone(Integra device, IBankSelect bankselect)
        {
            _Device = device;
            _Device.SystemExclusiveReceived += SystemExclusiveReceived;

            Initialize(bankselect);
        }

        #endregion;
        
        #region Properties: IBankSelect

        public byte MSB { get; private set; }
        public byte LSB { get; private set; }
        public byte PC  { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ID of the tone.
        /// </summary>
        /// <remarks><i>For display purpose only, do not use for calculations.</i></remarks>
        public int ID { get; private set; } = 0;

        /// <summary>
        /// Gets the tone name.
        /// </summary>
        public string Name { get; private set; } = "Uninitialized";

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
        public int? Variation { get; private set; } = 0;

        #endregion

        #region Methods

        public void Update(IBankSelect bankselect)
        {
            Initialize(bankselect);
        }

        private void Initialize(IBankSelect bankselect)
        {
            MSB = bankselect.MSB;
            LSB = bankselect.LSB;
            PC  = bankselect.PC;

            IsEditable = this.IsEditable();
            ToneBank   = this.ToneBank();
            Type       = this.ToneType();

            _Device.TransmitSystemExclusive(new IntegraSystemExclusive(0x0F000402, new IntegraRequest(MSB, LSB, PC, 0x01)));
        }

        #endregion

        #region Event Handlers

        private void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if (e.SystemExclusive.Address == 0x0F000402)
            {
                // Tone template request
                if (e.SystemExclusive.Data.Length == 0x00000015)
                {
                    if (e.SystemExclusive.Data[0] == MSB && e.SystemExclusive.Data[1] == LSB && e.SystemExclusive.Data[2] == PC)
                    {
                        int id = PC + 1;

                        switch (ToneBank)
                        {
                            case IntegraToneBanks.GM2Tone:
                                Variation = LSB;
                                break;

                            case IntegraToneBanks.GM2Drum:
                                id = 1;
                                //Variation = PC;
                                break;

                            default:
                                id += ((LSB - (int)ToneBank & 0x00FF) % 64 * 128);
                                Variation = null;
                                break;
                        }

                        ID = id;
                        Category = (IntegraToneCategories)e.SystemExclusive.Data[3];
                        Name = Encoding.ASCII.GetString(e.SystemExclusive.Data, 5, 12);

                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                    }
                }
            }
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
