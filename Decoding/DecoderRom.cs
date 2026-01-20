namespace pdp11_emulator.Decoding;
using Signaling.Cycles;
using Executing.Computing;

public class DecoderRom
{
    protected MicroCycle[] FetchEngine =
    [
        MicroCycle.FETCH_READ, MicroCycle.PC_INC, MicroCycle.FETCH_LATCH, MicroCycle.DECODE,
    ];

    protected readonly MicroCycle[][] EaEngine =
    [
        // 0 |  R
        [MicroCycle.EA_REG_LATCH],
        
        // 1 |  @R or (R)
        [MicroCycle.EA_READ_MODDED,MicroCycle.EA_UNI_LATCH],
        
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

    protected readonly Dictionary<FlagMask, AluFlag> FlagMasks = new()
    {
        { FlagMask.NZOC, AluFlag.Negative | AluFlag.Zero | AluFlag.Overflow | AluFlag.Carry },
        { FlagMask.NZO, AluFlag.Negative | AluFlag.Zero | AluFlag.Overflow },
        { FlagMask.NZ, AluFlag.Negative | AluFlag.Zero },
    };
    
    protected enum FlagMask
    {
        NZOC, NZO, NZ,
    }
}