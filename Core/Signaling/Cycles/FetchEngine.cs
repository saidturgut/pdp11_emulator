namespace pdp11_emulator.Core.Signaling.Cycles;
using Executing.Computing;

public partial class MicroUnitRom
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
            RegisterAction.NONE, 2),
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
