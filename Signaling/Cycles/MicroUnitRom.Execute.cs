namespace pdp11_emulator.Signaling.Cycles;
using Executing.Components;

// EXECUTE CYCLES
public partial class MicroUnitRom
{
    private static SignalSet EMPTY() => new();
    private static SignalSet HALT() => new();
    private static SignalSet WAIT() => new();
    private static SignalSet RESET() => new();
    
    private static SignalSet TMP_TO_REG() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = decoded.Registers[registersIndex],
        UseByteMode = decoded.ByteMode,
    };
    private static SignalSet TMP_TO_UNI() => new()
        { UniBusDriving = GetWriteMode(), };
    
    private static SignalSet EXECUTE_EA() => new()
    {
        CpuBusDriver = Register.TMP,
        AluAction = new AluAction(decoded.Operation, Register.DST, 0),
        CpuBusLatcher = Register.TMP,
        UseByteMode = decoded.ByteMode,
        
        FlagMask = decoded.FlagMask
    };
    private static SignalSet EXECUTE_FLAGS() => new()
    {
        CpuBusDriver = Register.TMP,
        AluAction = new AluAction(decoded.Operation, Register.DST, 0),
        UseByteMode = decoded.ByteMode,
        
        FlagMask = decoded.FlagMask
    };

    private static SignalSet EXECUTE_PSW() => new()
    {
        PswAction = new PswAction(decoded.CycleLatch),
        FlagMask = decoded.FlagMask,
    };
}