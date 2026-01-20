namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded ONE_OPERAND(ushort ir)
    {
        Decoded decoded = new()
        {
            Drivers = [(Register)(ir & 0x7)],
            
            AluOperation = (ir >> 6) switch
            {
                0x30 => AluOperation.ROR,
                0x31 => AluOperation.ROL,
                0x32 => AluOperation.ASR,
                0x33 => AluOperation.ASL,
                _ => SingleOperandTable[(ir >> 6) & 0x7],
            },
        };
        
        Console.WriteLine("OPERATION  " + decoded.AluOperation);
        
        // FLAG MASK
        decoded.FlagMask = decoded.AluOperation switch
        {
            AluOperation.INC or AluOperation.DEC => FlagMasks[FlagMask.NZO],
            AluOperation.PASS or AluOperation.SWAB => FlagMasks[FlagMask.NZ],
            _ => FlagMasks[FlagMask.NZOC]
        };
        
        // EA AND EXE ENGINES
        decoded.MicroCycles.AddRange(EaEngine[(ir >> 3) & 0x7]);
        decoded.MicroCycles.Add(MicroCycle.EXE_LATCH);

        // WRITE BACK ENGINE
        if (decoded.AluOperation is not AluOperation.PASS)
            decoded.MicroCycles.Add(
                ((ir >> 3) & 0x7) == 0 ? MicroCycle.WRITE_BACK_ONE : MicroCycle.WRITE_BACK_RAM);
        
        return decoded;
    }

    private readonly AluOperation[] SingleOperandTable =
    [
        AluOperation.ZERO, AluOperation.COM, AluOperation.INC, AluOperation.DEC,
        AluOperation.NEG, AluOperation.ADC, AluOperation.SBC, AluOperation.PASS,
        AluOperation.ASR, AluOperation.ASL, AluOperation.ROR, AluOperation.ROL, AluOperation.SWAB,
    ];
}