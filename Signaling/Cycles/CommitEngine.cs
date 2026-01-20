namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// COMMIT CYCLES
public partial class ControlUnitRom
{
    private static SignalSet COMMIT_ONE() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = decoded.Drivers[0],
        CycleMode = decoded.CycleMode,
    };
    
    private static SignalSet COMMIT_TWO() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = decoded.Drivers[1],
        CycleMode = decoded.CycleMode,
    };

    private static SignalSet COMMIT_RAM() => new()
        { UniBusDriving = GetWriteMode(), };
    
    private static SignalSet COMMIT_BRANCH() => new()
    {
        CpuBusDriver = Register.R7,
        
        AluAction = new AluAction(Operation.BRANCH, Register.NONE, decoded.CycleLatch),
        Condition = decoded.Condition,
        
        CpuBusLatcher = Register.R7,
    };
}
