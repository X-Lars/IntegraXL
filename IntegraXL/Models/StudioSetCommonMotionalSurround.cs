using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models
{
    /// <summary>
    /// Defines the INTEGRA-7 studio set common motional surround model.
    /// </summary>
    [Integra(0x18000800, 0x00000010)]
    public class StudioSetCommonMotionalSurround : IntegraModel<StudioSetCommonMotionalSurround>
    {
        #region Fields: INTEGRA-7

        [Offset(0x0000)] private IntegraSwitch _Switch;
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
        [Offset(0x000D)] private byte[] _RESERVED = new byte[3];

        #endregion

        #region Constructor

        private StudioSetCommonMotionalSurround(Integra device) : base(device) { }

        #endregion

        #region Properties: INTEGRA-7

        [Offset(0x0000)]
        public IntegraSwitch Switch
        {
            get => _Switch;
            set
            {
                if (_Switch != value)
                {
                    _Switch = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0001)]
        public IntegraRoomTypes RoomType
        {
            get => _RoomType;
            set
            {
                if (_RoomType != value)
                {
                    _RoomType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0002)]
        public byte AmbienceLevel
        {
            get => _AmbienceLevel;
            set
            {
                if (_AmbienceLevel != value)
                {
                    _AmbienceLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0003)]
        public IntegraRoomSizes RoomSize
        {
            get => _RoomSize;
            set
            {
                if (_RoomSize != value)
                {
                    _RoomSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0004)]
        public byte AmbienceTime
        {
            get => _AmbienceTime;
            set
            {
                if (_AmbienceTime != value)
                {
                    _AmbienceTime = value.Clamp(0, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        public byte AmbienceDensity
        {
            get => _AmbienceDensity;
            set
            {
                if (_AmbienceDensity != value)
                {
                    _AmbienceDensity = value.Clamp(0, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public byte AmbienceHFDamp
        {
            get => _AmbienceHFDamp;
            set
            {
                if (_AmbienceHFDamp != value)
                {
                    _AmbienceHFDamp = value.Clamp(0, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0007)]
        public int ExtPartLR
        {
            get => _ExtPartLR.Deserialize(64);
            set
            {
                if (ExtPartLR != value)
                {
                    _ExtPartLR = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0008)]
        public int ExtPartFB
        {
            get => _ExtPartFB.Deserialize(64);
            set
            {
                if (ExtPartFB != value)
                {
                    _ExtPartFB = value.Clamp(-64, 63).Serialize(64);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0009)]
        public byte ExtPartWidth
        {
            get => _ExtPartWidth;
            set
            {
                if (_ExtPartWidth != value)
                {
                    _ExtPartWidth = value.Clamp(0, 32);
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000A)]
        public byte ExtPartAmbienceSendLevel
        {
            get => _ExtPartAmbienceSendLevel;
            set
            {
                if (_ExtPartAmbienceSendLevel != value)
                {
                    _ExtPartAmbienceSendLevel = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000B)]
        public IntegraControlChannels ExtPartControlChannel
        {
            get => _ExtPartControlChannel;
            set
            {
                if (_ExtPartControlChannel != value)
                {
                    _ExtPartControlChannel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x000C)]
        public byte MotionalSurroundDepth
        {
            get => _MotionalSurroundDepth;
            set
            {
                if (_MotionalSurroundDepth != value)
                {
                    _MotionalSurroundDepth = value.Clamp(0, 100);
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
    }
}
