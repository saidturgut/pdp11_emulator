namespace pdp11_emulator.Decoding;
using Signaling.Cycles;
using Executing.Components;

public class DecoderRom
{
    protected static readonly MicroCycle[][] AddressEngine =
    [
        // 0 |  R
        [MicroCycle.REG_TO_TEMP],
        
        // 1 |  @R or (R)
        [MicroCycle.REG_TO_UNI_MOD ,MicroCycle.MDR_TO_TEMP],
        
        // 2 |  (R)+ 
        [MicroCycle.REG_TO_UNI_MOD, MicroCycle.MDR_TO_TEMP, MicroCycle.REG_INC],
        // 3 |  @(R)+ 
        [MicroCycle.REG_TO_UNI_WORD, MicroCycle.MDR_TO_UNI, MicroCycle.MDR_TO_TEMP, MicroCycle.REG_INC],
        
        // 4 |  -(R)
        [MicroCycle.REG_DEC, MicroCycle.REG_TO_UNI_MOD, MicroCycle.MDR_TO_TEMP],
        // 5 |  @-(R)
        [MicroCycle.REG_DEC, MicroCycle.REG_TO_UNI_WORD, MicroCycle.MDR_TO_UNI, MicroCycle.MDR_TO_TEMP],
        
        // 6 |  X(R)
        [MicroCycle.PC_TO_UNI, MicroCycle.MDR_INDEX_UNI_MOD, MicroCycle.MDR_TO_TEMP, MicroCycle.PC_INC],
        // 7 |  @X(R) 
        [MicroCycle.PC_TO_UNI, MicroCycle.MDR_INDEX_UNI_WORD, MicroCycle.MDR_TO_UNI, MicroCycle.MDR_TO_TEMP, MicroCycle.PC_INC],
    ];

}

public static class FlagMasks
{
    public static readonly Dictionary<FlagMask, PswFlag> Table = new()
    {
        { FlagMask.ALL, PswFlag.CMOD2 | PswFlag.CMOD1 | PswFlag.PMOD2 | PswFlag.PMOD1 | PswFlag.P7 |PswFlag.P6 | PswFlag.P5 | PswFlag.TRACE | PswFlag.NEGATIVE | PswFlag.ZERO | PswFlag.OVERFLOW | PswFlag.CARRY },
        { FlagMask.NZOC, PswFlag.NEGATIVE | PswFlag.ZERO | PswFlag.OVERFLOW | PswFlag.CARRY },
        { FlagMask.NZO, PswFlag.NEGATIVE | PswFlag.ZERO | PswFlag.OVERFLOW },
        { FlagMask.NZ, PswFlag.NEGATIVE | PswFlag.ZERO },
        { FlagMask.Z, PswFlag.ZERO },
    };
}

public  enum FlagMask
{
    ALL, NZOC, NZO, NZ, Z,
}
