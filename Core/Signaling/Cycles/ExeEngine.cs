namespace pdp11_emulator.Core.Signaling.Cycles;
using Executing.Computing;

// EXECUTE CYCLES
public partial class MicroUnitRom
{
    private static SignalSet HALT() => new()
    {

    };
    
    private static SignalSet ALU_EXECUTE() => new()
    {
        CpuBusDriver = RegisterAction.TMP,
        AluAction = new AluAction(decoded.AluOperation, 
            RegisterAction.DST, 0),
        CpuBusLatcher = RegisterAction.TMP,
    };
}