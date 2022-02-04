namespace IntegraXL.Interfaces
{
    public interface IBankSelect : IEquatable<IBankSelect>, IEquatable<byte[]>
    {
        public byte MSB { get; }
        public byte LSB { get; }
        public byte PC  { get; }
    }
}
