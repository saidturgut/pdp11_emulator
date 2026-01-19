namespace pdp11_emulator.Signaling.Cycles;
using Decoding;

public partial class ControlUnitRom
{    
    protected static Decoded decoded = new();
    protected static byte registersIndex;

    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        EMPTY,
        FETCH_MAR, PC_INC, FETCH_MDR,
        DECODE, HALT,
    
        EA_REG, EA_REG_MAR,
        EA_INC, EA_DEC,
        EA_INDEX_MAR, EA_INDEX_MDR,
        EA_RAM_MAR, EA_RAM_MDR,
        
        EXE_WRITE_BACK, EXE_FLAGS,
        WRITE_BACK, WRITE_BACK_REG, WRITE_BACK_RAM,
    ];

    protected static readonly MicroCycle[] ToggleCycles =
    [
        MicroCycle.EA_REG, MicroCycle.EA_RAM_MDR,
    ];
}

public enum MicroCycle
{ 
    EMPTY,
    FETCH_MAR, PC_INC, FETCH_MDR,
    DECODE, HALT,
    
    EA_REG, EA_REG_MAR,
    EA_INC, EA_DEC,
    EA_INDEX_MAR, EA_INDEX_MDR,
    EA_RAM_MAR, EA_RAM_MDR,
        
    EXE_WRITE_BACK, EXE_FLAGS,
    WRITE_BACK, WRITE_BACK_REG, WRITE_BACK_RAM,
}
