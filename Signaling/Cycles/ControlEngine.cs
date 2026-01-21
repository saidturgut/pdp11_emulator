namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// CONTROL CYCLES
public partial class ControlUnitRom
{
    private static SignalSet REG_ALU() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        AluAction = new AluAction(decoded.Operation, Register.NONE, 2),
        CpuBusLatcher = decoded.Registers[registersIndex],
    };
    private static SignalSet REG_TO_MAR() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        CpuBusLatcher = Register.MAR,
        UniBusDriving = decoded.UniBusMode,
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
}