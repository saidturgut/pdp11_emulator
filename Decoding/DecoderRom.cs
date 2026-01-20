namespace pdp11_emulator.Decoding;
using Signaling.Cycles;
using Executing.Computing;

public class DecoderRom
{
    protected MicroCycle[] FetchEngine =
    [
        MicroCycle.FETCH_READ, MicroCycle.PC_INC, MicroCycle.FETCH_LATCH, MicroCycle.DECODE,
    ];

    protected readonly MicroCycle[][] AddressEngine =
    [
        // 0 |  R
        [MicroCycle.EA_REG_LATCH],
        
        // 1 |  @R or (R)
        [MicroCycle.EA_READ_MODDED ,MicroCycle.EA_UNI_LATCH],
        
        // 2 |  (R)+ 
        [MicroCycle.EA_READ_MODDED, MicroCycle.EA_UNI_LATCH, MicroCycle.EA_INC],
        // 3 |  @(R)+ 
        [MicroCycle.EA_READ_WORD, MicroCycle.EA_DEFERRED, MicroCycle.EA_UNI_LATCH, MicroCycle.EA_INC],
        
        // 4 |  -(R)
        [MicroCycle.EA_DEC, MicroCycle.EA_READ_MODDED, MicroCycle.EA_UNI_LATCH],
        // 5 |  @-(R)
        [MicroCycle.EA_DEC, MicroCycle.EA_READ_WORD, MicroCycle.EA_DEFERRED, MicroCycle.EA_UNI_LATCH],
        
        // 6 |  X(R)
        [MicroCycle.EA_INDEX_ADDR, MicroCycle.EA_INDEX_MODDED, MicroCycle.EA_UNI_LATCH, MicroCycle.PC_INC],
        // 7 |  @X(R) 
        [MicroCycle.EA_INDEX_ADDR, MicroCycle.EA_INDEX_WORD, MicroCycle.EA_DEFERRED, MicroCycle.EA_UNI_LATCH, MicroCycle.PC_INC],
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
