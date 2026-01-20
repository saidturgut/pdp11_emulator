namespace pdp11_emulator.Signaling.Cycles;
using Decoding;

public partial class ControlUnitRom
{    
    protected static Decoded decoded = new();
    protected static byte registersIndex;

    private static byte GetStepSize()
        => (byte)(decoded.CycleMode != CycleMode.BYTE_MODE || 
                  (decoded.Drivers[registersIndex] is Register.R7 or Register.R6) ? 2 : 1);

    private static UniBusDriving GetReadMode()
        => decoded.CycleMode != CycleMode.BYTE_MODE
            ? UniBusDriving.READ_WORD : UniBusDriving.READ_BYTE;
    
    private static UniBusDriving GetWriteMode()
        => decoded.CycleMode != CycleMode.BYTE_MODE
            ? UniBusDriving.WRITE_WORD : UniBusDriving.WRITE_BYTE;
    
    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        EMPTY,
        FETCH_READ, PC_INC, FETCH_LATCH,
        DECODE, HALT,
    
        EA_REG_LATCH, 
        EA_READ_MODDED, EA_READ_WORD,
        EA_INC, EA_DEC,
        EA_INDEX_ADDR, EA_INDEX_MODDED, EA_INDEX_WORD,
        EA_DEFERRED, EA_UNI_LATCH,
        EA_TOGGLE,
        
        EXECUTE_LATCH, EXECUTE_FLAGS, EXECUTE_PSW,
        COMMIT_BRANCH,
        COMMIT_ONE, COMMIT_TWO, COMMIT_RAM,
    ];
}

public enum MicroCycle
{ 
    EMPTY,
    FETCH_READ, PC_INC, FETCH_LATCH,
    DECODE, HALT,
    
    EA_REG_LATCH, 
    EA_READ_MODDED, EA_READ_WORD,
    EA_INC, EA_DEC,
    EA_INDEX_ADDR, EA_INDEX_MODDED, EA_INDEX_WORD,
    EA_DEFERRED, EA_UNI_LATCH,
    EA_TOGGLE,
        
    EXECUTE_LATCH, EXECUTE_FLAGS, EXECUTE_PSW,
    COMMIT_BRANCH,
    COMMIT_ONE, COMMIT_TWO, COMMIT_RAM,
}
