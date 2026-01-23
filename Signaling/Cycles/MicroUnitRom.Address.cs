namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// ADDRESS CYCLES
public partial class MicroUnitRom
{
    private static readonly Register[] TemporaryRegisters 
        = [Register.TMP, Register.DST];
    
    private static SignalSet REG_TO_TEMP() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        CpuBusLatcher = TemporaryRegisters[registersIndex],
        UseByteMode = decoded.ByteMode,
    };// EXIT
    private static SignalSet REG_TO_UNI_WORD() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    private static SignalSet REG_TO_UNI_MOD() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        CpuBusLatcher = Register.MAR,
        UniBusDriving = GetReadMode(),
    };
    
    private static SignalSet REG_INC() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        AluAction = new AluAction(Operation.ADD, Register.NONE, GetStepSize()),
        CpuBusLatcher = decoded.Registers[registersIndex],
    };
    private static SignalSet REG_DEC() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        AluAction = new AluAction(Operation.SUB, Register.NONE, GetStepSize()),
        CpuBusLatcher = decoded.Registers[registersIndex],
    };
    
    private static SignalSet PC_TO_UNI() => new()
    {
        CpuBusDriver = Register.PC,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    private static SignalSet MDR_INDEX_UNI_MOD() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = decoded.Registers[registersIndex],
        AluAction = new AluAction(Operation.ADD, Register.MDR, 0),
        CpuBusLatcher = Register.MAR,
        UniBusDriving = GetReadMode(),
    };
    private static SignalSet MDR_INDEX_UNI_WORD() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = decoded.Registers[registersIndex],
        AluAction = new AluAction(Operation.ADD, Register.MDR, 0),
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD
    };

    private static SignalSet MDR_TO_UNI() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = GetReadMode(),
    };
    private static SignalSet MDR_TO_TEMP() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = TemporaryRegisters[registersIndex],
        UseByteMode = decoded.ByteMode,
    };// EXIT
    
    private static SignalSet INDEX_TOGGLE() => new();
}
