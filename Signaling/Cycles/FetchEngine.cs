namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// FETCH CYCLES
public partial class ControlUnitRom
{
    private static SignalSet EMPTY() => new();

    private static SignalSet FETCH_MAR() => new()
    {
        CpuBusDriver = RegisterAction.R7,
        CpuBusLatcher = RegisterAction.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };

    private static SignalSet PC_INC() => new()
    {
        CpuBusDriver = RegisterAction.R7,
        AluAction = new AluAction(AluOperation.ADD, 
            RegisterAction.NONE, 2, AluFlag.None),
        CpuBusLatcher = RegisterAction.R7,
    };

    private static SignalSet FETCH_MDR() => new()
    {
        UniBusLatching = UniBusLatching.READ_WORD,
        CpuBusDriver = RegisterAction.MDR,
        CpuBusLatcher = RegisterAction.IR,
    };

    private static SignalSet DECODE() => new();
}
