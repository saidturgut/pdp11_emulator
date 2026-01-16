namespace pdp11_emulator.Core.Signaling.Cycles;

public partial class MicroUnitROM
{
    private static SignalSet EMPTY() => new();

    private static SignalSet FETCH_MAR() => new()
    {
        CPUBusDriver = RegisterAction.R7,
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    };

    private static SignalSet PC_INC() => new()
    {
        CPUBusDriver = RegisterAction.R7,
        ALUAction = new ALUAction(ALUOperation.ADD, 
            RegisterAction.NONE, 2),
        CPUBusLatcher = RegisterAction.R7,
    };

    private static SignalSet FETCH_MDR() => new()
    {
        UNIBUSLatch = UNIBUSAction.READ,
        CPUBusDriver = RegisterAction.MAR,
        CPUBusLatcher = RegisterAction.IR,
    };

    private static SignalSet DECODE() => new();
}