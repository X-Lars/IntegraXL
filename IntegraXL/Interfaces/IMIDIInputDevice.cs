using IntegraXL.Core;

namespace IntegraXL.Interfaces
{
    public interface IMIDIInputDevice : IMIDIDevice
    {
        public event EventHandler<LongMessageEventArgs> LongMessageReceived;

        public void Open();
        public void Close();
        public void Start();

        public void LongMessageEventHandler(object sender, LongMessageEventArgs e);

    }
}
