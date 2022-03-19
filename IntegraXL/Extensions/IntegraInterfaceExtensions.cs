using IntegraXL.Interfaces;

namespace IntegraXL.Extensions
{
    public static class IntegraInterfaceExtensions
    {
        public static byte[] Bytes(this IBankSelect bankselect)
        {
            return new byte[3] { bankselect.MSB, bankselect.LSB, bankselect.PC };
        }
    }
}
