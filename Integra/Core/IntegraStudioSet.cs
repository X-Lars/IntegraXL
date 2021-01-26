using System.Text;

namespace Integra.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class IntegraStudioSet : IntegraDataTemplate<IntegraStudioSet>
    {
        private int _ID;
        private string _Name;
        private byte _MSB;
        private byte _LSB;
        private byte _PC;

        /// <summary>
        /// Creates and initalizes a new <see cref="IntegraStudioSet"/> instance.
        /// </summary>
        /// <param name="id">The ID of the studio set.</param>
        /// <param name="data">The data to initalize the instance.</param>
        /// <remarks><i>Constructor used for dynamic instance creation.</i></remarks>
        private IntegraStudioSet(int id, byte[] data) : base(id, data)
        {
            ID = id;
            Name = Encoding.ASCII.GetString(data, 5, 16);
            MSB = data[0];
            LSB = data[1];
            PC = data[2];
        }
        
        public int ID
        {
            get { return _ID; }
            set { _ID = value; NotifyPropertyChanged(); }
        }


        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(); }
        }


        public byte MSB
        {
            get { return _MSB; }
            set { _MSB = value; NotifyPropertyChanged(); }
        }


        public byte LSB
        {
            get { return _LSB; }
            set { _LSB = value; NotifyPropertyChanged(); }
        }

        public byte PC
        {
            get { return _PC; }
            set { _PC = value; NotifyPropertyChanged(); }
        }
    }
}
