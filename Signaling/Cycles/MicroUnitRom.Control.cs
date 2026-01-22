namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// CONTROL CYCLES
public partial class MicroUnitRom
{
    private static SignalSet REG_ALU() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        AluAction = new AluAction(decoded.Operation, Register.NONE, 2),
        CpuBusLatcher = decoded.Registers[registersIndex],
    };
    private static SignalSet REG_TO_UNI() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        CpuBusLatcher = Register.MAR,
        UniBusDriving = decoded.MemoryMode,
    };
    
    private static SignalSet PC_TO_REG() => new()
    {
        CpuBusDriver = Register.PC,
        CpuBusLatcher = decoded.Registers[registersIndex]
    };
    private static SignalSet REG_TO_PC() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        CpuBusLatcher = Register.PC,
    };
    private static SignalSet DST_TO_PC() => new()
    {
        CpuBusDriver = Register.DST,
        CpuBusLatcher = Register.PC,
    };

    private static SignalSet MDR_TO_REG() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = decoded.Registers[registersIndex],
    };
    
    // BRANCHING
    private static SignalSet BRANCH_DEC() => new()
    {
        CpuBusDriver = decoded.Registers[0],
        AluAction = new AluAction(Operation.DEC, Register.NONE, 0),
        CpuBusLatcher = decoded.Registers[0],
    };
    private static SignalSet BRANCH_COMMIT() => new()
    {
        CpuBusDriver = Register.PC,
        
        AluAction = new AluAction(decoded.Operation, Register.NONE, decoded.CycleLatch),
        Condition = decoded.Condition,
        
        CpuBusLatcher = Register.PC,
    };
}
