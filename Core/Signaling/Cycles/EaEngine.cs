namespace pdp11_emulator.Core.Signaling.Cycles;
using Executing.Computing;

// EFFECTIVE ADDRESS MICRO CYCLES
public partial class MicroUnitRom
{
    private static readonly RegisterAction[] EaLatchers 
        = [RegisterAction.TMP, RegisterAction.DST];
    
    private static SignalSet EA_REG() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        CpuBusLatcher = EaLatchers[registersIndex],
    };// EXIT
    private static SignalSet EA_REG_MAR() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        CpuBusLatcher = RegisterAction.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };

    private static SignalSet EA_INC() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        AluAction = new AluAction(AluOperation.ADD,
            RegisterAction.NONE, decoded.StepSize),
        CpuBusLatcher = decoded.Drivers[registersIndex],
    };
    private static SignalSet EA_DEC() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        AluAction = new AluAction(AluOperation.SUB,
            RegisterAction.NONE, decoded.StepSize),
        CpuBusLatcher = decoded.Drivers[registersIndex],
    };
    
    private static SignalSet EA_INDEX_MAR() => new()
    {
        CpuBusDriver = RegisterAction.R7,
        CpuBusLatcher = RegisterAction.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    private static SignalSet EA_INDEX_MDR() => new()
    {
        UniBusLatching = UniBusLatching.READ_WORD,
        CpuBusDriver = RegisterAction.MDR,
        AluAction = new AluAction(AluOperation.ADD, 
            decoded.Drivers[registersIndex], 0),
        CpuBusLatcher = RegisterAction.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    
    private static SignalSet EA_RAM_MAR() => new()
    {
        UniBusLatching = UniBusLatching.READ_WORD,
        CpuBusDriver = RegisterAction.MDR,
        CpuBusLatcher = RegisterAction.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    private static SignalSet EA_RAM_MDR() => new()
    {
        UniBusLatching = decoded.StepSize == 1 
            ? UniBusLatching.READ_BYTE : UniBusLatching.READ_WORD,
        CpuBusDriver = RegisterAction.MDR,
        CpuBusLatcher = EaLatchers[registersIndex],
    };// EXIT
}
