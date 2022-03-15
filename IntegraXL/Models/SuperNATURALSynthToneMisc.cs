using IntegraXL.Core;
using IntegraXL.Extensions;

namespace IntegraXL.Models
{
    [Integra(0x00005000, 0x00000025)]
    public class SuperNATURALSynthToneMisc : IntegraModel<SuperNATURALSynthToneMisc>
    {
        [Offset(0x0000)] byte RESERVED01;
        [Offset(0x0001)] byte _AttackIntervalSens;
        [Offset(0x0002)] byte _ReleaseIntervalSens;
        [Offset(0x0003)] byte _PortamentoIntervalSens;
        [Offset(0x0004)] IntegraEnvLoopMode _EnvLoopMode;
        [Offset(0x0005)] IntegraTempoSyncNote _EnvLoopSyncNote;
        [Offset(0x0006)] IntegraSwitch _ChromaticPortamento;
        [Offset(0x0007)] byte[] RESERVED02 = new byte[30];

        internal SuperNATURALSynthToneMisc(SuperNATURALSynthTone tone) : base(tone.Device)
        {
            Address += tone.Address;
        }

        [Offset(0x0001)]
        public byte AttackTimeIntervalSens
        {
            get => _AttackIntervalSens;
            set
            {
                if(_AttackIntervalSens != value)
                {
                    _AttackIntervalSens = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0002)]
        public byte ReleaseTimeIntervalSens
        {
            get => _ReleaseIntervalSens;
            set
            {
                if(_ReleaseIntervalSens != value)
                {
                    _ReleaseIntervalSens = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0003)]
        public byte PortamentoTimeIntervalSens
        {
            get => _PortamentoIntervalSens;
            set
            {
                if(_PortamentoIntervalSens != value)
                {
                    _PortamentoIntervalSens = value.Clamp();
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0004)]
        public IntegraEnvLoopMode EnvelopeLoopMode
        {
            get => _EnvLoopMode;
            set
            {
                if(_EnvLoopMode != value)
                {
                    _EnvLoopMode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0005)]
        public IntegraTempoSyncNote EnvelopeLoopSyncNote
        {
            get => _EnvLoopSyncNote;
            set
            {
                if(_EnvLoopSyncNote != value)
                {
                    _EnvLoopSyncNote = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Offset(0x0006)]
        public IntegraSwitch ChromaticPortamento
        {
            get => _ChromaticPortamento;
            set
            {
                if(_ChromaticPortamento != value)
                {
                    _ChromaticPortamento = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
