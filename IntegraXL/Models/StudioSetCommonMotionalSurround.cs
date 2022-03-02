using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x18000800, 0x00000010)]
    public class StudioSetCommonMotionalSurround : IntegraModel<StudioSetCommonMotionalSurround>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private bool _MotionalSurroundSwitch;
        [Offset(0x0001)] private IntegraRoomTypes _RoomType;
        [Offset(0x0002)] private byte _AmbienceLevel;
        [Offset(0x0003)] private IntegraRoomSizes _RoomSize;
        [Offset(0x0004)] private byte _AmbienceTime;
        [Offset(0x0005)] private byte _AmbienceDensity;
        [Offset(0x0006)] private byte _AmbienceHFDamp;
        [Offset(0x0007)] private byte _ExtPartLR;
        [Offset(0x0008)] private byte _ExtPartFB;
        [Offset(0x0009)] private byte _ExtPartWidth;
        [Offset(0x000A)] private byte _ExtPartAmbienceSendLevel;
        [Offset(0x000B)] private IntegraControlChannels _ExtPartControlChannel;
        [Offset(0x000C)] private byte _MotionalSurroundDepth;
        [Offset(0x000D)] private byte[] _Reserved = new byte[3];

        #endregion

        #region Constructor

#pragma warning disable IDE0051 // Remove unused private members
        private StudioSetCommonMotionalSurround(Integra device) : base(device) { }
#pragma warning restore IDE0051 // Remove unused private members

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public bool MotionalSurroundSwitch
        {
            get { return _MotionalSurroundSwitch; }
            set
            {
                _MotionalSurroundSwitch = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0001)]
        public IntegraRoomTypes RoomType
        {
            get { return _RoomType; }
            set
            {
                _RoomType = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0002)]
        public byte AmbienceLevel
        {
            get { return _AmbienceLevel; }
            set
            {
                _AmbienceLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0003)]
        public IntegraRoomSizes RoomSize
        {
            get { return _RoomSize; }
            set
            {
                _RoomSize = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0004)]
        public byte AmbienceTime
        {
            get { return _AmbienceTime; }
            set
            {
                _AmbienceTime = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public byte AmbienceDensity
        {
            get { return _AmbienceDensity; }
            set
            {
                _AmbienceDensity = (byte)value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte AmbienceHFDamp
        {
            get { return _AmbienceHFDamp; }
            set
            {
                _AmbienceHFDamp = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public byte ExtPartLR
        {
            get { return (byte)(_ExtPartLR - 64); }
            set
            {
                _ExtPartLR = (byte)(value + 64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0008)]
        public byte ExtPartFB
        {
            get { return (byte)(_ExtPartFB - 64); }
            set
            {
                _ExtPartFB = (byte)(value + 64);
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0009)]
        public byte ExtPartWidth
        {
            get { return _ExtPartWidth; }
            set
            {
                _ExtPartWidth = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000A)]
        public byte ExtPartAmbienceSendLevel
        {
            get { return _ExtPartAmbienceSendLevel; }
            set
            {
                _ExtPartAmbienceSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000B)]
        public IntegraControlChannels ExtPartControlChannel
        {
            get { return _ExtPartControlChannel; }
            set
            {
                _ExtPartControlChannel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000C)]
        public byte MotionalSurroundDepth
        {
            get { return _MotionalSurroundDepth; }
            set
            {
                _MotionalSurroundDepth = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Enumerations

        public IEnumerable<IntegraRoomTypes> RoomTypes
        {
            get { return Enum.GetValues(typeof(IntegraRoomTypes)).Cast<IntegraRoomTypes>(); }
        }

        public IEnumerable<IntegraRoomSizes> RoomSizes
        {
            get { return Enum.GetValues(typeof(IntegraRoomSizes)).Cast<IntegraRoomSizes>(); }
        }

        public IEnumerable<IntegraControlChannels> ControlChannels
        {
            get { return Enum.GetValues(typeof(IntegraControlChannels)).Cast<IntegraControlChannels>(); }
        }

        #endregion
    }
}
