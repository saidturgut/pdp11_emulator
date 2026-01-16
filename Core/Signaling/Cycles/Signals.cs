namespace pdp11_emulator.Core.Signaling.Cycles;

public struct SignalSet()
{
    public UniBusAction UniBusLatch = UniBusAction.NONE; // MDR
    public RegisterAction CpuBusDriver = RegisterAction.NONE;
    public AluAction? AluAction = null;
    public RegisterAction CpuBusLatcher = RegisterAction.NONE;
    public UniBusAction UniBusDrive = UniBusAction.NONE; // MAR
}

public struct AluAction(AluOperation operation, 
    RegisterAction registerOperand , ushort constOperand)
{
    public AluOperation AluOperation = operation;
    public RegisterAction RegisterOperand = registerOperand;
    public ushort ConstOperand = constOperand;
}

public enum UniBusAction
{
    NONE, READ, WRITE,
}

public enum AluOperation
{
    NONE, ADD, SUB
}

public enum RegisterAction
{
    NONE,
    R0, R1, R2, R3, R4, R5, R6, R7,
    IR, MDR, MAR, TMP,
}
