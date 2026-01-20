namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// EFFECTIVE ADDRESS CYCLES
public partial class ControlUnitRom
{
    private static readonly Register[] EaLatchers 
        = [Register.TMP, Register.DST];
    
    private static SignalSet EA_REG_LATCH() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        CpuBusLatcher = EaLatchers[registersIndex],
        UseByteMode = decoded.ByteMode,
    };// EXIT
    
    private static SignalSet EA_READ_MODDED() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        CpuBusLatcher = Register.MAR,
        UniBusDriving = GetReadMode(),
    };

    private static SignalSet EA_READ_WORD() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    
    private static SignalSet EA_INC() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        AluAction = new AluAction(AluOperation.ADD,
            Register.NONE, GetStepSize(), AluFlag.None),
        CpuBusLatcher = decoded.Drivers[registersIndex],
    };
    private static SignalSet EA_DEC() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        AluAction = new AluAction(AluOperation.SUB,
            Register.NONE, GetStepSize(), AluFlag.None),
        CpuBusLatcher = decoded.Drivers[registersIndex],
    };
    
    private static SignalSet EA_INDEX_ADDR() => new()
    {
        CpuBusDriver = Register.R7,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    private static SignalSet EA_INDEX_WORD() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        AluAction = new AluAction(AluOperation.ADD, 
            decoded.Drivers[registersIndex], 0, AluFlag.None),
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    private static SignalSet EA_INDEX_MODDED() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        AluAction = new AluAction(AluOperation.ADD, 
            decoded.Drivers[registersIndex], 0, AluFlag.None),
        CpuBusLatcher = Register.MAR,
        UniBusDriving = GetReadMode(),
    };
    
    private static SignalSet EA_DEFERRED() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = GetReadMode(),
    };
    
    private static SignalSet EA_UNI_LATCH() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = EaLatchers[registersIndex],
        UseByteMode = decoded.ByteMode,
    };// EXIT

    private static SignalSet EA_TOGGLE() => new();
}
