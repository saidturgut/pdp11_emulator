namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// WRITE BACK CYCLES
public partial class ControlUnitRom
{
    private static SignalSet WRITE_BACK_ONE() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = decoded.Drivers[0],
        UseByteMode = decoded.ByteMode,
    };
    
    private static SignalSet WRITE_BACK_TWO() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = decoded.Drivers[1],
        UseByteMode = decoded.ByteMode,
    };

    private static SignalSet WRITE_BACK_RAM() => new()
        { UniBusDriving = GetWriteMode(), };
}
