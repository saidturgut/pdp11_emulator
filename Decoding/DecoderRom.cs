namespace pdp11_emulator.Decoding;
using Signaling.Cycles;
using Executing.Components;

public class DecoderRom
{
    protected MicroCycle[] FetchEngine =
    [
        MicroCycle.FETCH_READ, MicroCycle.PC_INC, MicroCycle.FETCH_LATCH, MicroCycle.DECODE,
    ];

    protected readonly MicroCycle[][] AddressEngine =
    [
        // 0 |  R
        [MicroCycle.REG_TO_TEMP],
        
        // 1 |  @R or (R)
        [MicroCycle.REG_TO_MAR_MOD ,MicroCycle.MDR_TO_TEMP],
        
        // 2 |  (R)+ 
        [MicroCycle.REG_TO_MAR_MOD, MicroCycle.MDR_TO_TEMP, MicroCycle.REG_INC],
        // 3 |  @(R)+ 
        [MicroCycle.REG_TO_MAR_WORD, MicroCycle.MDR_TO_MAR, MicroCycle.MDR_TO_TEMP, MicroCycle.REG_INC],
        
        // 4 |  -(R)
        [MicroCycle.REG_DEC, MicroCycle.REG_TO_MAR_MOD, MicroCycle.MDR_TO_TEMP],
        // 5 |  @-(R)
        [MicroCycle.REG_DEC, MicroCycle.REG_TO_MAR_WORD, MicroCycle.MDR_TO_MAR, MicroCycle.MDR_TO_TEMP],
        
        // 6 |  X(R)
        [MicroCycle.PC_TO_MAR, MicroCycle.MDR_INDEX_MAR_MOD, MicroCycle.MDR_TO_TEMP, MicroCycle.PC_INC],
        // 7 |  @X(R) 
        [MicroCycle.PC_TO_MAR, MicroCycle.MDR_INDEX_MAR_WORD, MicroCycle.MDR_TO_MAR, MicroCycle.MDR_TO_TEMP, MicroCycle.PC_INC],
    ];

}

public static class FlagMasks
{
    public static readonly Dictionary<FlagMask, PswFlag> Table = new()
    {
        { FlagMask.NZOC, PswFlag.Negative | PswFlag.Zero | PswFlag.Overflow | PswFlag.Carry },
        { FlagMask.NZO, PswFlag.Negative | PswFlag.Zero | PswFlag.Overflow },
        { FlagMask.NZ, PswFlag.Negative | PswFlag.Zero },
        { FlagMask.Z, PswFlag.Zero },
    };
}

public enum FlagMask
{
    NZOC, NZO, NZ, Z,
}
