namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// FETCH CYCLES
public partial class ControlUnitRom
{
    private static SignalSet EMPTY() => new();

    private static SignalSet FETCH_READ() => new()
    {
        CpuBusDriver = Register.R7,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };

    private static SignalSet PC_INC() => new()
    {
        CpuBusDriver = Register.R7,
        AluAction = new AluAction(Operation.ADD, Register.NONE, 2),
        CpuBusLatcher = Register.R7,
    };

    private static SignalSet FETCH_LATCH() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = Register.IR,
    };

    private static SignalSet DECODE() => new();
}
