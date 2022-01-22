namespace IntegraXL.Interfaces
{
    public interface IMIDIDevice
    {
        public int ID { get; }
        public string Name { get; }

        public void Open();
        public void Close();

    }
}
