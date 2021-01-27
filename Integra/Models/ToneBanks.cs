using Integra.Core;
using Integra.Database;

namespace Integra.Models
{
    /// <summary>
    /// Generic tone bank class to access the INTEGRA-7 tone banks by type.
    /// </summary>
    /// <typeparam name="T">A <typeparamref name="T"/> specifying the type of tone bank.</typeparam>
    //public sealed class ToneBank<T> : IntegraBase<ToneBank<T>> where T: IntegraBaseToneBank, new()
    //{
    //    public ToneBank() : base(0x0F000402) { }
    //}

    // Preset tone banks

    public class SNAPresetToneBank : IntegraBaseToneBank { public SNAPresetToneBank() : base(0x59, 0x40, 256)  { Name = "SuperNATURAL Acoustic Preset Tone Bank"; } }
    public class SNDPresetToneBank : IntegraBaseToneBank { public SNDPresetToneBank() : base(0x58, 0x40, 26)   { Name = "SuperNATURAL Preset Drum Kits"; } }
    public class SNSPresetToneBank : IntegraBaseToneBank { public SNSPresetToneBank() : base(0x5F, 0x40, 1109) { Name = "SuperNATURAL Synth Preset Tone Bank"; } }
    public class PCMPresetToneBank : IntegraBaseToneBank { public PCMPresetToneBank() : base(0x57, 0x40, 896)  { Name = "PCM Preset Tone Bank"; } }
    public class PCMPresetDrumKits : IntegraBaseToneBank { public PCMPresetDrumKits() : base(0x56, 0x40, 14)   { Name = "PCM Preset Drum Kits"; } }

    // User tone banks

    public class SNAUserToneBank : IntegraBaseToneBank { public SNAUserToneBank() : base(0x59, 0x00, 256) { Name = "SuperNATURAL Acoustic User Tone Bank"; } }
    public class SNDUserToneBank : IntegraBaseToneBank { public SNDUserToneBank() : base(0x58, 0x00, 64)  { Name = "SuperNATURAL User Drum Kits"; } }
    public class SNSUserToneBank : IntegraBaseToneBank { public SNSUserToneBank() : base(0x5F, 0x00, 512) { Name = "SuperNATURAL Synth User Tone Bank"; } }
    public class PCMUserToneBank : IntegraBaseToneBank { public PCMUserToneBank() : base(0x57, 0x00, 256) { Name = "PCM User Tone Bank"; } }
    public class PCMUserDrumKits : IntegraBaseToneBank { public PCMUserDrumKits() : base(0x56, 0x00, 32)  { Name = "PCM User Drum Kits"; } }

    // Expansions

    public class ExSN01ToneBank : IntegraBaseToneBank { public ExSN01ToneBank() : base(0x59, 0x60, 17) { Name = "ExSN-01 Etnic"; } }
    public class ExSN02ToneBank : IntegraBaseToneBank { public ExSN02ToneBank() : base(0x59, 0x61, 17) { Name = "ExSN-02 Woodwinds"; } }
    public class ExSN03ToneBank : IntegraBaseToneBank { public ExSN03ToneBank() : base(0x59, 0x62, 50) { Name = "ExSN-03 Session"; } }
    public class ExSN04ToneBank : IntegraBaseToneBank { public ExSN04ToneBank() : base(0x59, 0x63, 12) { Name = "ExSN-04 Acoustic Guitar"; } }
    public class ExSN05ToneBank : IntegraBaseToneBank { public ExSN05ToneBank() : base(0x59, 0x64, 12) { Name = "ExSN-05 Brass"; } }
    public class ExSN06ToneBank : IntegraBaseToneBank { public ExSN06ToneBank() : base(0x58, 0x65, 7)  { Name = "ExSN-06 SFX"; } }

    public class SRX01ToneBank : IntegraBaseToneBank { public SRX01ToneBank() : base(0x5D, 0x00, 41)  { Name = "SRX-01 Dynamic Drums"; } }
    public class SRX02ToneBank : IntegraBaseToneBank { public SRX02ToneBank() : base(0x5D, 0x01, 50)  { Name = "SRX-02 Concert Piano"; } }
    public class SRX03ToneBank : IntegraBaseToneBank { public SRX03ToneBank() : base(0x5D, 0x02, 128) { Name = "SRX-03 Studio SRX"; } }
    public class SRX04ToneBank : IntegraBaseToneBank { public SRX04ToneBank() : base(0x5D, 0x03, 128) { Name = "SRX-04 Symphonique Strings"; } }
    public class SRX05ToneBank : IntegraBaseToneBank { public SRX05ToneBank() : base(0x5D, 0x04, 312) { Name = "SRX-05 Supreme Dance"; } }
    public class SRX06ToneBank : IntegraBaseToneBank { public SRX06ToneBank() : base(0x5D, 0x07, 449) { Name = "SRX-06 Complete Orchestra"; } }
    public class SRX07ToneBank : IntegraBaseToneBank { public SRX07ToneBank() : base(0x5D, 0x0B, 475) { Name = "SRX-07 Ultimate Keys"; } }
    public class SRX08ToneBank : IntegraBaseToneBank { public SRX08ToneBank() : base(0x5D, 0x0F, 448) { Name = "SRX-08 Platinum Trax"; } }
    public class SRX09ToneBank : IntegraBaseToneBank { public SRX09ToneBank() : base(0x5D, 0x13, 414) { Name = "SRX-09 World Collection"; } }
    public class SRX10ToneBank : IntegraBaseToneBank { public SRX10ToneBank() : base(0x5D, 0x17, 100) { Name = "SRX-10 Big Brass Ensemble"; } }
    public class SRX11ToneBank : IntegraBaseToneBank { public SRX11ToneBank() : base(0x5D, 0x18, 42)  { Name = "SRX-11 Complete Piano"; } }
    public class SRX12ToneBank : IntegraBaseToneBank { public SRX12ToneBank() : base(0x5D, 0x1A, 50)  { Name = "SRX-12 Classic EP's"; } }

    public class SRX01DrumKits : IntegraBaseToneBank { public SRX01DrumKits() : base(0x5C, 0x00, 79) { Name = "SRX-01 Drum Kits"; } }
    public class SRX03DrumKits : IntegraBaseToneBank { public SRX03DrumKits() : base(0x5C, 0x02, 12) { Name = "SRX-03 Drum Kits"; } }
    public class SRX05DrumKits : IntegraBaseToneBank { public SRX05DrumKits() : base(0x5C, 0x04, 34) { Name = "SRX-05 Drum Kits"; } }
    public class SRX06DrumKits : IntegraBaseToneBank { public SRX06DrumKits() : base(0x5C, 0x07, 5)  { Name = "SRX-06 Drum Kits"; } }
    public class SRX07DrumKits : IntegraBaseToneBank { public SRX07DrumKits() : base(0x5C, 0x0B, 11) { Name = "SRX-07 Drum Kits"; } }
    public class SRX08DrumKits : IntegraBaseToneBank { public SRX08DrumKits() : base(0x5C, 0x0F, 21) { Name = "SRX-08 Drum Kits"; } }
    public class SRX09DrumKits : IntegraBaseToneBank { public SRX09DrumKits() : base(0x5C, 0x13, 12) { Name = "SRX-09 Drum Kits"; } }

    public class ExPCMTonebank : IntegraBaseToneBank { public ExPCMTonebank() : base(0x61, 0x00, 512) { Name = "ExPCM HQ GM2 + HQ PCM Sound"; } }
    public class ExPCMDrumKits : IntegraBaseToneBank { public ExPCMDrumKits() : base(0x60, 0x00, 19)  { Name = "ExPCM DrumKits"; } }

    // General MIDI
    public class GM2ToneBank : IntegraBaseToneBank
    {
        public GM2ToneBank() : base()
        {
            Name = "GM2 Tone Bank";

            MSB = 0x79;
            LSB = 0x00;
            Size = 256;


            Requests.Add(0x7900007F);
            Requests.Add(0x79013B7F);
            Requests.Add(0x79027F02);
        }
    }

    public class GM2DrumKits : IntegraBaseToneBank { public GM2DrumKits() : base(0x78, 0x00, 9) { Name = "GM2 Drum Kits"; } }

    public class Favorites : IntegraBaseToneBank 
    { 
        public Favorites() : base(0x00, 0x00, 0) 
        { 
            Name = "Favorites"; 
        }

        public override void Initialize()
        {
            DataAccess.Select(this, new IntegraTone()).ForEach(Collection.Add);
        }

        
    }
}
