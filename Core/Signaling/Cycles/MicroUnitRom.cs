namespace pdp11_emulator.Core.Signaling.Cycles;
using Decoding;

public partial class MicroUnitRom
{
    protected static Decoded decoded = new();

    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        EMPTY,
        FETCH_MAR, PC_INC, FETCH_MDR,
        DECODE,
    
        EA_REG, EA_REG_MAR,
        EA_INC, EA_DEC,
        EA_INDEX_MAR, EA_INDEX_MDR,
        EA_RAM_MAR, EA_RAM_MDR,
    ];
}

public enum MicroCycle
{ 
    EMPTY,
    FETCH_MAR, PC_INC, FETCH_MDR,
    DECODE,
    
    EA_REG, EA_REG_MAR,
    EA_INC, EA_DEC,
    EA_INDEX_MAR, EA_INDEX_MDR,
    EA_RAM_MAR, EA_RAM_MDR,
}
