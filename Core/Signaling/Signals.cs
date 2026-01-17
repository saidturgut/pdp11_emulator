namespace pdp11_emulator.Core.Signaling;
using Executing.Computing;
using Signaling.Cycles;

public struct SignalSet()
{
    public UniBusLatching UniBusLatching = UniBusLatching.NONE; // MDR
    public RegisterAction CpuBusDriver = RegisterAction.NONE;
    public AluAction? AluAction = null;
    public RegisterAction CpuBusLatcher = RegisterAction.NONE;
    public UniBusDriving UniBusDriving = UniBusDriving.NONE; // MAR

    public bool ByteMode = false;
}

public struct AluAction(AluOperation operation, 
    RegisterAction registerOperand , ushort constOperand)
{
    public AluOperation AluOperation = operation;
    public RegisterAction RegisterOperand = registerOperand;
    public ushort ConstOperand = constOperand;
}

public enum UniBusDriving
{
    NONE, READ_BYTE, READ_WORD
    , WRITE_BYTE, WRITE_WORD,
}

public enum UniBusLatching
{
    NONE, READ_BYTE, READ_WORD
}

public enum RegisterAction
{
    R0 = 0, R1 = 1, R2 = 2, R3 = 3, R4 = 4, R5 = 5, R6 = 6, R7 = 7,
    MDR = 8, IR = 9, MAR = 10, TMP = 11, DST = 12,
    NONE = 13,
}
