namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// EXECUTE CYCLES
public partial class ControlUnitRom
{
    private static SignalSet HALT() => new();
    
    private static SignalSet EXECUTE_LATCH() => new()
    {
        CpuBusDriver = Register.TMP,
        AluAction = new AluAction(decoded.Operation, Register.DST, 0),
        CpuBusLatcher = Register.TMP,
        CycleMode = decoded.CycleMode,
        
        FlagMask = decoded.FlagMask
    };

    private static SignalSet EXECUTE_FLAGS() => new()
    {
        CpuBusDriver = Register.TMP,
        AluAction = new AluAction(decoded.Operation, Register.DST, 0),
        CycleMode = decoded.CycleMode,
        
        FlagMask = decoded.FlagMask
    };

    private static SignalSet EXECUTE_PSW() => new()
    {
        PswAction = new PswAction(decoded.CycleLatch),
        FlagMask = decoded.FlagMask,
    };
}