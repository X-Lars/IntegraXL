using IntegraXL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Core
{
    

    public class IntegraBankSelect : IBankSelect
    {
        public IntegraBankSelect(byte msb, byte lsb, byte pc)
        {
            MSB = msb;
            LSB = lsb;
            PC = pc;
        }

        public byte MSB { get; set; }
        public byte LSB { get; set; }
        public byte PC { get; set; }

        public bool Equals(IBankSelect other)
        {
            return MSB == other.MSB && LSB == other.LSB && PC == other.PC;
        }

        public bool Equals(byte[] other)
        {
            return MSB == other[0] && LSB == other[1] && PC == other[3];
        }
    }
}
