namespace pdp11_emulator.Core.Signaling.Cycles;

public struct SignalSet()
{
    public UNIBUSAction UNIBUSLatch = UNIBUSAction.NONE; // MDR
    public RegisterAction CPUBusDriver = RegisterAction.NONE;
    public ALUAction? ALUAction = null;
    public RegisterAction CPUBusLatcher = RegisterAction.NONE;
    public UNIBUSAction UNIBUSDrive = UNIBUSAction.NONE; // MAR
}

public struct ALUAction(ALUOperation operation, 
    RegisterAction registerOperand , ushort constOperand)
{
    public ALUOperation ALUOperation = operation;
    public RegisterAction RegisterOperand = registerOperand;
    public ushort ConstOperand = constOperand;
}

public enum UNIBUSAction
{
    NONE, READ, WRITE,
}

public enum ALUOperation
{
    NONE, ADD, SUB
}

public enum RegisterAction
{
    NONE,
    R0, R1, R2, R3, R4, R5, R6, R7,
    IR, MDR, MAR, TMP,
}
