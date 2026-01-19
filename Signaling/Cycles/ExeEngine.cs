namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// EXECUTE CYCLES
public partial class ControlUnitRom
{
    private static SignalSet HALT() => new()
    {
    };
    
    private static SignalSet EXE_WRITE_BACK() => new()
    {
        CpuBusDriver = RegisterAction.TMP,
        AluAction = new AluAction(decoded.AluOperation, 
            RegisterAction.DST, 0, decoded.FlagMask),
        CpuBusLatcher = RegisterAction.TMP,
    };

    private static SignalSet EXE_FLAGS() => new()
    {
        CpuBusDriver = RegisterAction.TMP,
        AluAction = new AluAction(decoded.AluOperation,
            RegisterAction.DST, 0, decoded.FlagMask),
    };
}