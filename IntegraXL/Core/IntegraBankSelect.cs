using IntegraXL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    internal class IntegraBankSelect : IBankSelect
    {
        public IntegraBankSelect(byte msb, byte lsb, byte pc)
        {
            MSB = msb;
            LSB = lsb;
            PC = pc;

        }
        #region Interface: IBankSelect

        /// <summary>
        /// Gets the (M)ost (S)ignificant (B)yte to select the tone.
        /// </summary>
        public byte MSB { get; private set; }

        /// <summary>
        /// Gets the (L)east (S)ignificant (B)yte to select the tone.
        /// </summary>
        public byte LSB { get; private set; }

        /// <summary>
        /// Gets the (P)rogram (C)hange byte to select the tone.
        /// </summary>
        public byte PC { get; private set; }

        /// <summary>
        /// Gets whether the current <see cref="IBankSelect"/> interface data equals the provided <see cref="IBankSelect"/> interface data.
        /// </summary>
        /// <param name="bankSelect">The interface to compare.</param>
        /// <returns>True if both <see cref="IBankSelect"/> interfaces have equal data.</returns>
        public bool Equals(IBankSelect? bankSelect)
        {
            if (bankSelect is null)
                return false;

            return MSB == bankSelect.MSB && LSB == bankSelect.LSB && PC == bankSelect.PC;
        }

        #endregion
    }
}
