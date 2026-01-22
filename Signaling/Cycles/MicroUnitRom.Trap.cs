namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// TRAP CYCLES
public partial class MicroUnitRom
{
    private static SignalSet SP_ALU() => new()
    {
        CpuBusDriver = Register.SP,
        AluAction = new AluAction(decoded.Operation, Register.NONE, 2),
        CpuBusLatcher = Register.SP,
    };
        
    private static SignalSet REG_TO_TMP() => new()
    {
        CpuBusDriver = decoded.Registers[registersIndex],
        CpuBusLatcher = Register.TMP,
    };
    
    private static SignalSet SP_TO_UNI() => new()
    {
        CpuBusDriver = Register.SP,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = decoded.MemoryMode,
    };
    
    private static SignalSet VEC_INC() => new()
    {
        CpuBusDriver = Register.VEC,
        AluAction = new AluAction(Operation.ADD, Register.NONE, 2),
        CpuBusLatcher = Register.VEC,
    };

    private static SignalSet VEC_TO_UNI() => new()
    {
        CpuBusDriver = Register.VEC,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
}