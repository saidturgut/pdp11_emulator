namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// EFFECTIVE ADDRESS CYCLES
public partial class ControlUnitRom
{
    private static readonly Register[] EaLatchers 
        = [Register.TMP, Register.DST];
    
    private static SignalSet EA_REG_DATA() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        CpuBusLatcher = EaLatchers[registersIndex],
    };// EXIT
    private static SignalSet EA_REG_ADDR() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };

    private static SignalSet EA_INC() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        AluAction = new AluAction(AluOperation.ADD,
            Register.NONE, AluFlag.None),
        CpuBusLatcher = decoded.Drivers[registersIndex],
    };
    private static SignalSet EA_DEC() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        AluAction = new AluAction(AluOperation.SUB,
            Register.NONE, AluFlag.None),
        CpuBusLatcher = decoded.Drivers[registersIndex],
    };
    
    private static SignalSet EA_INDEX_ADDR() => new()
    {
        CpuBusDriver = Register.R7,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    private static SignalSet EA_INDEX_DATA() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        AluAction = new AluAction(AluOperation.ADD, 
            decoded.Drivers[registersIndex], AluFlag.None),
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    
    private static SignalSet EA_UNI_ADDR() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    
    private static SignalSet EA_UNI_DATA() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = EaLatchers[registersIndex],
    };// EXIT

    private static SignalSet EA_TOGGLE() => new();
}
