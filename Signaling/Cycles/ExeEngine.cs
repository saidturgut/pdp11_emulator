namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// EXECUTE CYCLES
public partial class ControlUnitRom
{
    private static SignalSet HALT() => new();
    
    private static SignalSet EXE_LATCH() => new()
    {
        CpuBusDriver = Register.TMP,
        AluAction = new AluAction(decoded.AluOperation, 
            Register.DST, 0, decoded.FlagMask),
        CpuBusLatcher = Register.TMP,
        UseByteMode = decoded.ByteMode,
    };

    private static SignalSet EXE_FLAGS() => new()
    {
        CpuBusDriver = Register.TMP,
        AluAction = new AluAction(decoded.AluOperation,
            Register.DST, 0, decoded.FlagMask),
        UseByteMode = decoded.ByteMode,
    };
}