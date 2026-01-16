namespace pdp11_emulator.Core.Signaling.Cycles;

// EFFECTIVE ADDRESS MICRO CYCLES
public partial class MicroUnitRom
{
    protected static byte currentRegister;
    
    private static SignalSet EA_REG() => new()
    {
        CpuBusDriver = decoded.Registers[currentRegister],
        CpuBusLatcher = RegisterAction.TMP,
    };// EXIT
    private static SignalSet EA_REG_MAR() => new()
    {
        CpuBusDriver = decoded.Registers[currentRegister],
        CpuBusLatcher = RegisterAction.MAR,
        UniBusDrive = UniBusAction.READ,
    };
    
    private static SignalSet EA_INC() => new()
    {
        CpuBusDriver = decoded.Registers[currentRegister],
        AluAction = new AluAction(AluOperation.ADD, 
            RegisterAction.NONE,2),
        CpuBusLatcher = decoded.Registers[currentRegister],
    };
    private static SignalSet EA_DEC() => new()
    {
        CpuBusDriver = decoded.Registers[currentRegister],
        AluAction = new AluAction(AluOperation.SUB, 
            RegisterAction.NONE,2),
        CpuBusLatcher = decoded.Registers[currentRegister],
    };
    
    private static SignalSet EA_INDEX_MAR() => new()
    {
        CpuBusDriver = RegisterAction.R7,
        CpuBusLatcher = RegisterAction.MAR,
        UniBusDrive = UniBusAction.READ,
    };
    private static SignalSet EA_INDEX_MDR() => new()
    {
        UniBusLatch = UniBusAction.READ,
        CpuBusDriver = RegisterAction.MDR,
        AluAction = new AluAction(AluOperation.ADD, 
            decoded.Registers[currentRegister], 0),
        CpuBusLatcher = RegisterAction.MAR,
        UniBusDrive = UniBusAction.READ,
    };
    
    private static SignalSet EA_RAM_MAR() => new()
    {
        UniBusLatch = UniBusAction.READ,
        CpuBusDriver = RegisterAction.MDR,
        CpuBusLatcher = RegisterAction.MAR,
        UniBusDrive = UniBusAction.READ,
    };
    private static SignalSet EA_RAM_MDR() => new()
    {
        UniBusLatch = UniBusAction.READ,
        CpuBusDriver = RegisterAction.MDR,
        CpuBusLatcher = RegisterAction.TMP,
    };// EXIT
}