using IntegraXL.Core;

namespace IntegraXL.Models
{
    [Integra(0x0F000402, 0x58400000,   26)] public sealed class SNDPresetToneBank : IntegraToneBank { private SNDPresetToneBank(Integra device) : base(device) { Name = "SuperNATURAL Preset Drum Kits"; } }
    [Integra(0x0F000402, 0x59400000,  256)] public sealed class SNAPresetToneBank : IntegraToneBank { private SNAPresetToneBank(Integra device) : base(device) { Name = "SuperNATURAL Acoustic Preset Tones"; } }
    [Integra(0x0F000402, 0x5F400000, 1109)] public sealed class SNSPresetToneBank : IntegraToneBank { private SNSPresetToneBank(Integra device) : base(device) { Name = "SuperNATURAL Synth Preset Tones"; } }
    [Integra(0x0F000402, 0x57400000,  896)] public sealed class PCMPresetToneBank : IntegraToneBank { private PCMPresetToneBank(Integra device) : base(device) { Name = "PCM Preset Tones"; } }
    [Integra(0x0F000402, 0x56400000,   14)] public sealed class PCMPresetDrumKits : IntegraToneBank { private PCMPresetDrumKits(Integra device) : base(device) { Name = "PCM Preset Drum Kits"; } }

    // User tone banks
    [Integra(0x0F000402, 0x59000000, 256)] public sealed class SNAUserToneBank : IntegraToneBank { private SNAUserToneBank(Integra device) : base(device) { Name = "SuperNATURAL Acoustic User Tones"; } }
    [Integra(0x0F000402, 0x58000000,  64)] public sealed class SNDUserToneBank : IntegraToneBank { private SNDUserToneBank(Integra device) : base(device) { Name = "SuperNATURAL User Drum Kits"; } }
    [Integra(0x0F000402, 0x5F000000, 512)] public sealed class SNSUserToneBank : IntegraToneBank { private SNSUserToneBank(Integra device) : base(device) { Name = "SuperNATURAL Synth User Tones"; } }
    [Integra(0x0F000402, 0x57000000, 256)] public sealed class PCMUserToneBank : IntegraToneBank { private PCMUserToneBank(Integra device) : base(device) { Name = "PCM User Tones"; } }
    [Integra(0x0F000402, 0x56000000,  32)] public sealed class PCMUserDrumKits : IntegraToneBank { private PCMUserDrumKits(Integra device) : base(device) { Name = "PCM User Drum Kits"; } }

    // Expansions
    [Integra(0x0F000402, 0x59600000,  17)] public sealed class ExSN01ToneBank : IntegraToneBank { private ExSN01ToneBank(Integra device) : base(device) { Name = "ExSN-01 Etnic Tones"; } }
    [Integra(0x0F000402, 0x59610000,  17)] public sealed class ExSN02ToneBank : IntegraToneBank { private ExSN02ToneBank(Integra device) : base(device) { Name = "ExSN-02 Woodwinds Tones"; } }
    [Integra(0x0F000402, 0x59620000,  50)] public sealed class ExSN03ToneBank : IntegraToneBank { private ExSN03ToneBank(Integra device) : base(device) { Name = "ExSN-03 Session Tones"; } }
    [Integra(0x0F000402, 0x59630000,  12)] public sealed class ExSN04ToneBank : IntegraToneBank { private ExSN04ToneBank(Integra device) : base(device) { Name = "ExSN-04 Acoustic Guitar Tones"; } }
    [Integra(0x0F000402, 0x59640000,  12)] public sealed class ExSN05ToneBank : IntegraToneBank { private ExSN05ToneBank(Integra device) : base(device) { Name = "ExSN-05 Brass Tones"; } }
    [Integra(0x0F000402, 0x58650000,   7)] public sealed class ExSN06ToneBank : IntegraToneBank { private ExSN06ToneBank(Integra device) : base(device) { Name = "ExSN-06 SFX Tones"; } }

    [Integra(0x0F000402, 0x5D000000,  41)] public sealed class SRX01ToneBank : IntegraToneBank { private SRX01ToneBank(Integra device) : base(device) { Name = "SRX-01 Dynamic Drums Tones"; } }
    [Integra(0x0F000402, 0x5D010000,  50)] public sealed class SRX02ToneBank : IntegraToneBank { private SRX02ToneBank(Integra device) : base(device) { Name = "SRX-02 Concert Piano Tones"; } }
    [Integra(0x0F000402, 0x5D020000, 128)] public sealed class SRX03ToneBank : IntegraToneBank { private SRX03ToneBank(Integra device) : base(device) { Name = "SRX-03 Studio SRX Tones"; } }
    [Integra(0x0F000402, 0x5D030000, 128)] public sealed class SRX04ToneBank : IntegraToneBank { private SRX04ToneBank(Integra device) : base(device) { Name = "SRX-04 Symphonique Strings Tones"; } }
    [Integra(0x0F000402, 0x5D040000, 312)] public sealed class SRX05ToneBank : IntegraToneBank { private SRX05ToneBank(Integra device) : base(device) { Name = "SRX-05 Supreme Dance Tones"; } }
    [Integra(0x0F000402, 0x5D070000, 449)] public sealed class SRX06ToneBank : IntegraToneBank { private SRX06ToneBank(Integra device) : base(device) { Name = "SRX-06 Complete Orchestra Tones"; } }
    [Integra(0x0F000402, 0x5D0B0000, 475)] public sealed class SRX07ToneBank : IntegraToneBank { private SRX07ToneBank(Integra device) : base(device) { Name = "SRX-07 Ultimate Keys Tones"; } }
    [Integra(0x0F000402, 0x5D0F0000, 448)] public sealed class SRX08ToneBank : IntegraToneBank { private SRX08ToneBank(Integra device) : base(device) { Name = "SRX-08 Platinum Trax Tones"; } }
    [Integra(0x0F000402, 0x5D130000, 414)] public sealed class SRX09ToneBank : IntegraToneBank { private SRX09ToneBank(Integra device) : base(device) { Name = "SRX-09 World Collection Tones"; } }
    [Integra(0x0F000402, 0x5D170000, 100)] public sealed class SRX10ToneBank : IntegraToneBank { private SRX10ToneBank(Integra device) : base(device) { Name = "SRX-10 Big Brass Ensemble Tones"; } }
    [Integra(0x0F000402, 0x5D180000,  42)] public sealed class SRX11ToneBank : IntegraToneBank { private SRX11ToneBank(Integra device) : base(device) { Name = "SRX-11 Complete Piano Tones"; } }
    [Integra(0x0F000402, 0x5D1A0000,  50)] public sealed class SRX12ToneBank : IntegraToneBank { private SRX12ToneBank(Integra device) : base(device) { Name = "SRX-12 Classic EP's Tones"; } }

    [Integra(0x0F000402, 0x5C000000,  79)] public sealed class SRX01DrumKits : IntegraToneBank { private SRX01DrumKits(Integra device) : base(device) { Name = "SRX-01 Dynamic Drums Drum Kits"; } }
    [Integra(0x0F000402, 0x5C020000,  12)] public sealed class SRX03DrumKits : IntegraToneBank { private SRX03DrumKits(Integra device) : base(device) { Name = "SRX-03 Studio SRX Drum Kits"; } }
    [Integra(0x0F000402, 0x5C040000,  34)] public sealed class SRX05DrumKits : IntegraToneBank { private SRX05DrumKits(Integra device) : base(device) { Name = "SRX-05 Supreme Dance Drum Kits"; } }
    [Integra(0x0F000402, 0x5C070000,   5)] public sealed class SRX06DrumKits : IntegraToneBank { private SRX06DrumKits(Integra device) : base(device) { Name = "SRX-06 Complete Orchestra Drum Kits"; } }
    [Integra(0x0F000402, 0x5C0B0000,  11)] public sealed class SRX07DrumKits : IntegraToneBank { private SRX07DrumKits(Integra device) : base(device) { Name = "SRX-07 Ultimate Keys Drum Kits"; } }
    [Integra(0x0F000402, 0x5C0F0000,  21)] public sealed class SRX08DrumKits : IntegraToneBank { private SRX08DrumKits(Integra device) : base(device) { Name = "SRX-08 Platinum Trax Drum Kits"; } }
    [Integra(0x0F000402, 0x5C130000,  12)] public sealed class SRX09DrumKits : IntegraToneBank { private SRX09DrumKits(Integra device) : base(device) { Name = "SRX-09 World Collection Drum Kits"; } }

    [Integra(0x0F000402, 0x61000000, 512)] public sealed class ExPCMTonebank : IntegraToneBank { private ExPCMTonebank(Integra device) : base(device) { Name = "ExPCM HQ GM2 + HQ PCM Sound Tones"; } }
    [Integra(0x0F000402, 0x60000000,  19)] public sealed class ExPCMDrumKits : IntegraToneBank { private ExPCMDrumKits(Integra device) : base(device) { Name = "ExPCM HQ GM2 + HQ PCM Sound DrumKits"; } }

    // General MIDI
    [Integra(0x0F000402, 0x79000000, 256)]
    public sealed class GM2ToneBank : IntegraToneBank
    {
        private GM2ToneBank(Integra device) : base(device) 
        { 
            Name = "GM2 Tones";

            Requests.Clear();

            Requests.Add(new IntegraRequest(0x7900007F));
            Requests.Add(new IntegraRequest(0x79013B7F));
            Requests.Add(new IntegraRequest(0x79027F02));
        }
    }

    [Integra(0x0F000402, 0x78000000, 9)] public sealed class GM2DrumKits : IntegraToneBank { private GM2DrumKits(Integra device) : base(device) { Name = "GM2 Drum Kits"; } }

}
