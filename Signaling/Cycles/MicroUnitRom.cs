namespace pdp11_emulator.Signaling.Cycles;
using Decoding;

public partial class MicroUnitRom
{    
    protected static Decoded decoded = new();
    protected static byte registersIndex;

    private static byte GetStepSize()
        => (byte)(!decoded.ByteMode || (decoded.Registers[registersIndex] is Register.PC or Register.SP_U) ? 2 : 1);

    private static UniBusDriving GetReadMode()
        => !decoded.ByteMode ? UniBusDriving.READ_WORD : UniBusDriving.READ_BYTE;
    
    private static UniBusDriving GetWriteMode()
        => !decoded.ByteMode ? UniBusDriving.WRITE_WORD : UniBusDriving.WRITE_BYTE;

    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        EMPTY,
        //FETCH ENGINE
        FETCH_READ, PC_INC, FETCH_LATCH,
        DECODE, HALT, WAIT, RESET,
    
        // ADDRESS ENGINE
        REG_TO_TEMP, 
        REG_TO_UNI_MOD, REG_TO_UNI_WORD,
        PC_TO_UNI, 
        REG_INC, REG_DEC,
        MDR_TO_TEMP, MDR_TO_UNI, 
        MDR_INDEX_UNI_MOD, MDR_INDEX_UNI_WORD,
        INDEX_TOGGLE,
    
        // CONTROL ENGINE
        REG_ALU, REG_TO_UNI,
        PC_TO_REG, REG_TO_PC, DST_TO_PC,
        MDR_TO_REG, REG_TO_TMP,
        
        // EXECUTE AND COMMIT ENGINE
        EXECUTE_EA, EXECUTE_FLAGS, EXECUTE_PSW,
        BRANCH_DEC, BRANCH_COMMIT,
        TMP_TO_REG, TMP_TO_UNI,
    
        // TRAP ENGINE
        SP_ALU, SP_TO_UNI, VEC_INC, VEC_TO_UNI
    ];
}

public enum MicroCycle
{ 
    EMPTY,
    //FETCH ENGINE
    FETCH_READ, PC_INC, FETCH_LATCH,
    DECODE, HALT, WAIT, RESET,
    
    // ADDRESS ENGINE
    REG_TO_TEMP, 
    REG_TO_UNI_MOD, REG_TO_UNI_WORD,
    PC_TO_UNI, 
    REG_INC, REG_DEC,
    MDR_TO_TEMP, MDR_TO_UNI, 
    MDR_INDEX_UNI_MOD, MDR_INDEX_UNI_WORD,
    INDEX_TOGGLE,
    
    // CONTROL ENGINE
    REG_ALU, REG_TO_UNI,
    PC_TO_REG, REG_TO_PC, DST_TO_PC,
    MDR_TO_REG, REG_TO_TMP,
        
    // EXECUTE AND COMMIT ENGINE
    EXECUTE_EA, EXECUTE_FLAGS, EXECUTE_PSW,
    BRANCH_DEC, BRANCH_COMMIT,
    TMP_TO_REG, TMP_TO_UNI,
    
    // TRAP ENGINE
    SP_ALU, SP_TO_UNI, VEC_INC, VEC_TO_UNI
}
