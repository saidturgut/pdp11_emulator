namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// FETCH CYCLES
public partial class MicroUnitRom
{
    private static SignalSet EMPTY() => new();

    private static SignalSet FETCH_READ() => new()
    {
        CpuBusDriver = Register.PC,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };

    private static SignalSet PC_INC() => new()
    {
        CpuBusDriver = Register.PC,
        AluAction = new AluAction(Operation.ADD, Register.NONE, 2),
        CpuBusLatcher = Register.PC,
    };

    private static SignalSet FETCH_LATCH() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = Register.IR,
    };

    private static SignalSet DECODE() => new();
}
