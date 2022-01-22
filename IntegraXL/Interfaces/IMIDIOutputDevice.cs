namespace IntegraXL.Interfaces
{
    public interface IMIDIOutputDevice : IMIDIDevice
    {
        public void SendLongMessage(byte[] data);
    }
}
