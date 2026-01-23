namespace pdp11_emulator.Signaling;
using Executing.Components;
using Executing.Computing;
using Signaling.Cycles;

public struct SignalSet()
{
    public bool UniBusLatching = false; // MDR
    public Register CpuBusDriver = Register.NONE;
    
    public AluAction? AluAction = null;
    public PswAction? PswAction = null;
    
    public Condition Condition = Condition.NONE;
    
    public Register CpuBusLatcher = Register.NONE;
    public UniBusDriving UniBusDriving = UniBusDriving.NONE; // MAR
    
    public PswFlag FlagMask = PswFlag.NONE;
    public bool UseByteMode = false;
    public bool SuppressTrace = false;
}

public struct AluAction(Operation operation, Register registerOperand, ushort stepSize)
{
    public readonly Operation Operation = operation;
    public readonly Register RegisterOperand = registerOperand;
    public readonly ushort StepSize = stepSize;
}

public struct PswAction(ushort buffer)
{
    public readonly ushort Buffer = buffer;
}

public enum Condition
{
    NONE, 
    BR, BNE, BEQ, BGE, BLT, BGT, BLE, 
    BPL, BMI, BHI, BLOS, BVC, BVS, BCC, BCS,
    SOB,
}

public enum UniBusDriving
{
    NONE, READ_BYTE, READ_WORD
    , WRITE_BYTE, WRITE_WORD,
}

public enum Register
{
    NONE = -1,
    R0 = 0, R1 = 1, R2 = 2, R3 = 3, R4 = 4, R5 = 5, SP_U = 6, PC = 7,
    MDR = 8, IR = 9, MAR = 10, TMP = 11, DST = 12,
    VEC = 13, SP_K = 14, PSW = 15,
}
