namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded SINGLE_OPERAND(ushort opcode)
    {
        byte bbb = (byte)((opcode >> 13) & 0b111);

        Console.WriteLine(opcode);
        
        Decoded decoded = new()
        {
            Drivers = [(RegisterAction)(opcode & 0x7)],
            AluOperation = SingleOperandTable[(opcode >> 6) & 0x7],
            StepSize = (byte)((bbb is 0b100 or 0b101) && (opcode & 0x8000) != 0 ? 1 : 2),
        };
        
        // FLAG MASK
        decoded.FlagMask = decoded.AluOperation switch
        {
            AluOperation.INC or AluOperation.DEC => FlagMasks[FlagMask.NZO],
            AluOperation.PASS or AluOperation.SWAB => FlagMasks[FlagMask.NZ],
            _ => FlagMasks[FlagMask.NZOC]
        };
        
        // EA AND EXE ENGINES
        decoded.MicroCycles.AddRange(EaEngine[(opcode >> 3) & 0x7]);
        decoded.MicroCycles.Add(MicroCycle.EXE_WRITE_BACK);

        // WRITE BACK ENGINE
        if (decoded.AluOperation is not AluOperation.PASS)
            decoded.MicroCycles.Add(MicroCycle.WRITE_BACK);
        
        return decoded;
    }

    private readonly AluOperation[] SingleOperandTable =
    [
        AluOperation.ZERO, AluOperation.NOT, AluOperation.INC, AluOperation.DEC,
        AluOperation.NEG, AluOperation.ADC, AluOperation.SBC, AluOperation.PASS,
        AluOperation.ASR, AluOperation.ASL, AluOperation.ROR, AluOperation.ROL, AluOperation.SWAB,
    ];
}