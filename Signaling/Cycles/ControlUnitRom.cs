namespace pdp11_emulator.Signaling.Cycles;
using Decoding;

public partial class ControlUnitRom
{    
    protected static Decoded decoded = new();
    protected static byte registersIndex;

    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        EMPTY,
        FETCH_READ, PC_INC, FETCH_LATCH,
        DECODE, HALT,
    
        EA_REG_LATCH, 
        EA_READ_MODDED, EA_READ_WORD,
        EA_INC, EA_DEC,
        EA_INDEX_ADDR, 
        EA_INDEX_MODDED, EA_INDEX_WORD,
        EA_DEFERRED, EA_UNI_LATCH,
        EA_TOGGLE,
        
        EXE_LATCH, EXE_FLAGS,
        WRITE_BACK_ONE, WRITE_BACK_TWO, WRITE_BACK_RAM,
    ];
    
    private static byte GetStepSize()
        => (byte)(!decoded.ByteMode || 
                  (decoded.Drivers[registersIndex] is Register.R7 or Register.R6) ? 2 : 1);

    private static UniBusDriving GetReadMode()
        => !decoded.ByteMode
            ? UniBusDriving.READ_WORD : UniBusDriving.READ_BYTE;
    
    private static UniBusDriving GetWriteMode()
        => !decoded.ByteMode
            ? UniBusDriving.WRITE_WORD : UniBusDriving.WRITE_BYTE;
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
        
    EXE_LATCH, EXE_FLAGS,
    WRITE_BACK_ONE, WRITE_BACK_TWO, WRITE_BACK_RAM,
}
