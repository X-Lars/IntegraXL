﻿using IntegraXL.Core;

namespace IntegraXL.File
{
    public static class FileManager
    {
        

        /// <summary>
        /// Creates a blank studio set file structure.
        /// </summary>
        /// <returns>A blank studio set file structure.</returns>
        public static StudioSetFile CreateStudioSetFile()
        {
            return new StudioSetFile();
        }

        /// <summary>
        /// Creates a blank temporary tone file structure.
        /// </summary>
        /// <returns>A blank temporary tone file structure.</returns>
        public static TemporaryToneFile CreateTemporaryToneFile()
        {
            return new TemporaryToneFile();
        }

        public static TemporaryToneFile LoadTemporaryTone(byte[] data)
        {
            TemporaryToneFile file = new();

            MemoryStream stream = new(data);
            BinaryReader reader = new(stream);

            file.ToneType = reader.ReadUInt32();

            switch((IntegraToneTypes)file.ToneType)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    file.SuperNATURALAcousticToneCommon = reader.ReadBytes(file.SuperNATURALAcousticToneCommon.Length);
                    break;

                case IntegraToneTypes.SuperNATURALSynthTone:
                    file.SuperNATURALSynthToneCommon = reader.ReadBytes(file.SuperNATURALSynthToneCommon.Length);
                    for (int i = 0; i < IntegraConstants.SNS_PARTIAL_COUNT; i++)
                    {
                        file.SuperNATURALSynthTonePartials[i] = reader.ReadBytes(file.SuperNATURALSynthTonePartials[i].Length);
                    }

                    break;

                case IntegraToneTypes.SuperNATURALDrumkit:
                    file.SuperNATURALDrumKitCommon = reader.ReadBytes(file.SuperNATURALDrumKitCommon.Length);
                    file.SuperNATURALDrumKitCommonCompEQ = reader.ReadBytes(file.SuperNATURALDrumKitCommonCompEQ.Length);
                    for (int i = 0; i < IntegraConstants.SND_NOTE_COUNT; i++)
                    {
                        file.SuperNATURALDrumKitNotes[i] = reader.ReadBytes(file.SuperNATURALDrumKitNotes[i].Length);
                    }
                    break;

                case IntegraToneTypes.PCMSynthTone:

                    file.PCMSynthToneCommon = reader.ReadBytes(file.PCMSynthToneCommon.Length);
                    file.PMT = reader.ReadBytes(file.PMT.Length);
                    for (int i = 0; i < IntegraConstants.PCM_PARTIAL_COUNT; i++)
                    {
                        file.PCMSynthTonePartials[i] = reader.ReadBytes(file.PCMSynthTonePartials[i].Length);
                    }
                    file.PCMSynthToneCommon2 = reader.ReadBytes(file.PCMSynthToneCommon2.Length);
                    break;

                case IntegraToneTypes.PCMDrumkit:
                    file.PCMDrumKitCommon = reader.ReadBytes(file.PCMDrumKitCommon.Length);
                    file.PCMDrumKitCommonCompEQ = reader.ReadBytes(file.PCMDrumKitCommonCompEQ.Length);
                    for (int i = 0; i < IntegraConstants.PCM_NOTE_COUNT; i++)
                    {
                        file.PCMDrumKitNotes[i] = reader.ReadBytes(file.PCMDrumKitNotes[i].Length);
                    }
                    file.PCMDrumKitCommon2 = reader.ReadBytes(file.PCMDrumKitCommon2.Length);
                    break;
            }

            file.MFX = reader.ReadBytes(file.MFX.Length);

            return file;
        }

        internal static MemoryStream WriteTemporaryToneFile(TemporaryToneFile file)
        {
            MemoryStream stream = new();
            BinaryWriter writer = new(stream);

            writer.Write(file.ToneType);

            switch((IntegraToneTypes)file.ToneType)
            {
                case IntegraToneTypes.SuperNATURALAcousticTone:
                    writer.Write(file.SuperNATURALAcousticToneCommon);
                    break;

                case IntegraToneTypes.SuperNATURALSynthTone:
                    writer.Write(file.SuperNATURALSynthToneCommon);

                    for (int i = 0; i < IntegraConstants.SNS_PARTIAL_COUNT; i++)
                    {
                        writer.Write(file.SuperNATURALSynthTonePartials[i]);
                    }

                    writer.Write(file.SuperNATURALAcousticToneCommon);
                    break;

                case IntegraToneTypes.SuperNATURALDrumkit:
                    writer.Write(file.SuperNATURALDrumKitCommon);
                    writer.Write(file.SuperNATURALDrumKitCommonCompEQ);
                    for (int i = 0; i < IntegraConstants.SND_NOTE_COUNT; i++)
                    {
                        writer.Write(file.SuperNATURALDrumKitNotes[i]);
                    }

                    break;

                case IntegraToneTypes.PCMSynthTone:
                    writer.Write(file.PCMSynthToneCommon);
                    writer.Write(file.PMT);

                    for (int i = 0; i < IntegraConstants.PCM_PARTIAL_COUNT; i++)
                    {
                        writer.Write(file.PCMSynthTonePartials[i]);
                    }

                    writer.Write(file.PCMSynthToneCommon2);

                    break;

                case IntegraToneTypes.PCMDrumkit:
                    writer.Write(file.PCMDrumKitCommon);
                    writer.Write(file.PCMDrumKitCommonCompEQ);
                    for (int i = 0; i < IntegraConstants.PCM_NOTE_COUNT; i++)
                    {
                        writer.Write(file.PCMDrumKitNotes[i]);
                    }
                    writer.Write(file.PCMDrumKitCommon2);

                    break;

                default:

                    throw new IntegraException($"[{nameof(FileManager)}.{nameof(WriteTemporaryToneFile)}]\n" +
                                               $"Undefined tone type");
            }

            writer.Write(file.MFX);
            
            return stream;
        }

        public static StudioSetFile LoadStudioSet(byte[] data)
        {
            StudioSetFile file = new();

            MemoryStream stream = new(data);
            BinaryReader reader = new(stream);

            file.Expansions = reader.ReadBytes(file.Expansions.Length);

            file.Common = reader.ReadBytes(file.Common.Length);
            file.CommonChorus = reader.ReadBytes(file.CommonChorus.Length);
            file.CommonReverb = reader.ReadBytes(file.CommonReverb.Length);
            file.MotionalSurround = reader.ReadBytes(file.MotionalSurround.Length);
            file.MasterEQ = reader.ReadBytes(file.MasterEQ.Length);

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                file.Midis[i] = reader.ReadBytes(file.Midis[i].Length);
            }

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                file.Parts[i] = reader.ReadBytes(file.Parts[i].Length);
            }

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                file.PartEQs[i] = reader.ReadBytes(file.PartEQs[i].Length);
            }

            return file;
        }

        /// <summary>
        /// Writes the provided <see cref="StudioSetFile"/> to a binary memory stream and returns the stream as an array that can be written to disk.
        /// </summary>
        /// <param name="file">The file to write as binary stream.</param>
        /// <returns>A memory stream containing the binary formatted studio set file data.</returns>
        internal static MemoryStream WriteStudioSet(StudioSetFile file)
        {
            MemoryStream stream = new();
            BinaryWriter writer = new(stream);

            writer.Write(file.Expansions);

            writer.Write(file.Common);
            writer.Write(file.CommonChorus);
            writer.Write(file.CommonReverb);
            writer.Write(file.MotionalSurround);
            writer.Write(file.MasterEQ);

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                writer.Write(file.Midis[i]);
            }

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                writer.Write(file.Parts[i]);
            }

            for (int i = 0; i < IntegraConstants.PART_COUNT; i++)
            {
                writer.Write(file.PartEQs[i]);
            }

            return stream;
        }
    }
}
