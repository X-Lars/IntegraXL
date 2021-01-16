using Integra.Core;
using MidiXL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Models
{
    public interface IToneMFX
    {
        ToneMFX MFX { get; }
    }

    public class ToneMFX : IntegraBase<ToneMFX>
    {
        [Offset(0x0000)] private IntegraMFXTypes _Type;
        [Offset(0x0002)] private byte _ChorusSendLevel;
        [Offset(0x0003)] private byte _ReverbSendLevel;
        [Offset(0x0005)] private IntegraMFXControlSources _ControlSource01;
        [Offset(0x0006)] private byte _ControlSense01;
        [Offset(0x0007)] private IntegraMFXControlSources _ControlSource02;
        [Offset(0x0008)] private byte _ControlSense02;
        [Offset(0x0009)] private IntegraMFXControlSources _ControlSource03;
        [Offset(0x000A)] private byte _ControlSense03;
        [Offset(0x000B)] private IntegraMFXControlSources _ControlSource04;
        [Offset(0x000C)] private byte _ControlSense04;

        [Offset(0x000D)] private IntegraMFXControlAssigns _ControlAssign01;
        [Offset(0x000E)] private IntegraMFXControlAssigns _ControlAssign02;
        [Offset(0x000F)] private IntegraMFXControlAssigns _ControlAssign03;
        [Offset(0x0010)] private IntegraMFXControlAssigns _ControlAssign04;

        [Offset(0x0011)] private int _Parameter01;
        [Offset(0x0015)] private int _Parameter02;
        [Offset(0x0019)] private int _Parameter03;
        [Offset(0x001D)] private int _Parameter04;
        [Offset(0x0021)] private int _Parameter05;
        [Offset(0x0025)] private int _Parameter06;
        [Offset(0x0029)] private int _Parameter07;
        [Offset(0x002D)] private int _Parameter08;
        [Offset(0x0031)] private int _Parameter09;
        [Offset(0x0035)] private int _Parameter10;
        [Offset(0x0039)] private int _Parameter11;
        [Offset(0x003D)] private int _Parameter12;
        [Offset(0x0041)] private int _Parameter13;
        [Offset(0x0045)] private int _Parameter14;
        [Offset(0x0049)] private int _Parameter15;
        [Offset(0x004D)] private int _Parameter16;
        [Offset(0x0051)] private int _Parameter17;
        [Offset(0x0055)] private int _Parameter18;
        [Offset(0x0059)] private int _Parameter19;
        [Offset(0x005D)] private int _Parameter20;
        [Offset(0x0061)] private int _Parameter21;
        [Offset(0x0065)] private int _Parameter22;
        [Offset(0x0069)] private int _Parameter23;
        [Offset(0x006D)] private int _Parameter24;
        [Offset(0x0071)] private int _Parameter25;
        [Offset(0x0075)] private int _Parameter26;
        [Offset(0x0079)] private int _Parameter27;
        [Offset(0x007D)] private int _Parameter28;
        [Offset(0x0101)] private int _Parameter29;
        [Offset(0x0105)] private int _Parameter30;
        [Offset(0x0109)] private int _Parameter31;
        [Offset(0x010D)] private int _Parameter32;

        public ToneMFX(IntegraAddress address) : base(address + 0x00000200, 0x00000111)
        {
            Name = "Tone MFX";
            Console.WriteLine($"[{nameof(ToneMFX)}] {Address}");
        }

        #region Properties

        [Offset(0x0000)]
        public IntegraMFXTypes Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0002)]
        public byte ChorusSendLevel
        {
            get { return _ChorusSendLevel; }
            set
            {
                _ChorusSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0003)]
        public byte ReverbSendLevel
        {
            get { return _ReverbSendLevel; }
            set
            {
                _ReverbSendLevel = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0005)]
        public IntegraMFXControlSources ControlSource01
        {
            get { return _ControlSource01; }
            set
            {
                _ControlSource01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0006)]
        public byte ControlSense01
        {
            get { return _ControlSense01; }
            set
            {
                _ControlSense01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0007)]
        public IntegraMFXControlSources ControlSource02
        {
            get { return _ControlSource02; }
            set
            {
                _ControlSource02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0008)]
        public byte ControlSense02
        {
            get { return _ControlSense02; }
            set
            {
                _ControlSense02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0009)]
        public IntegraMFXControlSources ControlSource03
        {
            get { return _ControlSource03; }
            set
            {
                _ControlSource03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000A)]
        public byte ControlSense03
        {
            get { return _ControlSense03; }
            set
            {
                _ControlSense03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000B)]
        public IntegraMFXControlSources ControlSource04
        {
            get { return _ControlSource04; }
            set
            {
                _ControlSource04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000C)]
        public byte ControlSense04
        {
            get { return _ControlSense04; }
            set
            {
                _ControlSense04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000D)]
        public IntegraMFXControlAssigns ControlAssign01
        {
            get { return _ControlAssign01; }
            set
            {
                _ControlAssign01 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000E)]
        public IntegraMFXControlAssigns ControlAssign02
        {
            get { return _ControlAssign02; }
            set
            {
                _ControlAssign02 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x000F)]
        public IntegraMFXControlAssigns ControlAssign03
        {
            get { return _ControlAssign03; }
            set
            {
                _ControlAssign03 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0010)]
        public IntegraMFXControlAssigns ControlAssign04
        {
            get { return _ControlAssign04; }
            set
            {
                _ControlAssign04 = value;
                NotifyPropertyChanged();
            }
        }

        [Offset(0x0011)] public int Parameter01 { get { return GetData(_Parameter01); } set { _Parameter01 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0015)] public int Parameter02 { get { return GetData(_Parameter02); } set { _Parameter02 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0019)] public int Parameter03 { get { return GetData(_Parameter03); } set { _Parameter03 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x001D)] public int Parameter04 { get { return GetData(_Parameter04); } set { _Parameter04 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0021)] public int Parameter05 { get { return GetData(_Parameter05); } set { _Parameter05 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0025)] public int Parameter06 { get { return GetData(_Parameter06); } set { _Parameter06 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0029)] public int Parameter07 { get { return GetData(_Parameter07); } set { _Parameter07 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x002D)] public int Parameter08 { get { return GetData(_Parameter08); } set { _Parameter08 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0031)] public int Parameter09 { get { return GetData(_Parameter09); } set { _Parameter09 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0035)] public int Parameter10 { get { return GetData(_Parameter10); } set { _Parameter10 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0039)] public int Parameter11 { get { return GetData(_Parameter11); } set { _Parameter11 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x003D)] public int Parameter12 { get { return GetData(_Parameter12); } set { _Parameter12 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0041)] public int Parameter13 { get { return GetData(_Parameter13); } set { _Parameter13 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0045)] public int Parameter14 { get { return GetData(_Parameter14); } set { _Parameter14 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0049)] public int Parameter15 { get { return GetData(_Parameter15); } set { _Parameter15 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x004D)] public int Parameter16 { get { return GetData(_Parameter16); } set { _Parameter16 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0051)] public int Parameter17 { get { return GetData(_Parameter17); } set { _Parameter17 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0055)] public int Parameter18 { get { return GetData(_Parameter18); } set { _Parameter18 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0059)] public int Parameter19 { get { return GetData(_Parameter19); } set { _Parameter19 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x005D)] public int Parameter20 { get { return GetData(_Parameter20); } set { _Parameter20 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0061)] public int Parameter21 { get { return GetData(_Parameter21); } set { _Parameter21 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0065)] public int Parameter22 { get { return GetData(_Parameter22); } set { _Parameter22 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0069)] public int Parameter23 { get { return GetData(_Parameter23); } set { _Parameter23 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x006D)] public int Parameter24 { get { return GetData(_Parameter24); } set { _Parameter24 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0071)] public int Parameter25 { get { return GetData(_Parameter25); } set { _Parameter25 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0075)] public int Parameter26 { get { return GetData(_Parameter26); } set { _Parameter26 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0079)] public int Parameter27 { get { return GetData(_Parameter27); } set { _Parameter27 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x007D)] public int Parameter28 { get { return GetData(_Parameter28); } set { _Parameter28 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0101)] public int Parameter29 { get { return GetData(_Parameter29); } set { _Parameter29 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0105)] public int Parameter30 { get { return GetData(_Parameter30); } set { _Parameter30 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x0109)] public int Parameter31 { get { return GetData(_Parameter31); } set { _Parameter31 = SetData(value); NotifyPropertyChanged(); } }
        [Offset(0x010D)] public int Parameter32 { get { return GetData(_Parameter32); } set { _Parameter32 = SetData(value); NotifyPropertyChanged(); } }


        #endregion

        protected int GetData(int value)
        {
            byte[] values = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(values);

            return ((values[1] & 0x0F) << 8 | (values[2] & 0x0F) << 4 | (values[3] & 0x0F));
        }

        protected int SetData(int value)
        {
            byte[] result = new byte[4];

            result[0] = 0x08;
            result[1] = (byte)((value >> 8) & 0x0F);
            result[2] = (byte)((value >> 4) & 0x0F);
            result[3] = (byte)((value & 0x0F));

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return BitConverter.ToInt32(result, 0); ;
        }

        internal override void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if (!IsInitialized)
            {
                if (syx.Address == Address)
                {
                    if (Initialize(syx.Data))
                        Device.Instance.ReportProgress(new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                }
                

            }
            else
            {
                if ((syx.Address & 0xFFFFF000) == (Address & 0xFFFFF000))
                {
                    if((syx.Address & 0xFFFFF0FF).IsInRange(0x00000200, 0x00000300))
                    {
                        if (syx.Data.Length == 4)
                            InitializeField(syx);
                    }
                }
            }
        }
    }
}
