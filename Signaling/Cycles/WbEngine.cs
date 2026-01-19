namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// WRITE BACK CYCLES
public partial class ControlUnitRom
{
    private static SignalSet WRITE_BACK() => new()
    {
        CpuBusDriver = RegisterAction.TMP,
        CpuBusLatcher = decoded.Drivers[0],
        ByteMode = decoded.StepSize == 1,
    };
    
    private static SignalSet WRITE_BACK_REG() => new()
    {
        CpuBusDriver = RegisterAction.TMP,
        CpuBusLatcher = decoded.Drivers[1],
        ByteMode = decoded.StepSize == 1,
    };

    private static SignalSet WRITE_BACK_RAM() => new()
    {
        UniBusDriving = decoded.StepSize == 1 
            ? UniBusDriving.WRITE_BYTE : UniBusDriving.WRITE_WORD
    };
}