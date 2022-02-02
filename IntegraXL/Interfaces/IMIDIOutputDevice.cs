namespace IntegraXL.Interfaces
{
    public interface IMIDIOutputDevice : IMIDIDevice
    {
        public void Open();
        public void Close();
        public void SendLongMessage(byte[] data);
    }
}
