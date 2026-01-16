namespace pdp11_emulator.Core.Signaling.Cycles;

// EFFECTIVE ADDRESS MICRO CYCLES
public partial class MicroUnitROM
{
    protected static byte currentRegister;
    
    private static SignalSet EA_REG() => new()
    {
        CPUBusDriver = decoded.Registers[currentRegister],
        CPUBusLatcher = RegisterAction.TMP,
    };// EXIT
    private static SignalSet EA_REG_MAR() => new()
    {
        CPUBusDriver = decoded.Registers[currentRegister],
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    };
    
    private static SignalSet EA_INC() => new()
    {
        CPUBusDriver = decoded.Registers[currentRegister],
        ALUAction = new ALUAction(ALUOperation.ADD, 
            RegisterAction.NONE,2),
        CPUBusLatcher = decoded.Registers[currentRegister],
    };
    private static SignalSet EA_DEC() => new()
    {
        CPUBusDriver = decoded.Registers[currentRegister],
        ALUAction = new ALUAction(ALUOperation.SUB, 
            RegisterAction.NONE,2),
        CPUBusLatcher = decoded.Registers[currentRegister],
    };
    
    private static SignalSet EA_INDEX_MAR() => new()
    {
        CPUBusDriver = RegisterAction.R7,
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    };
    private static SignalSet EA_INDEX_MDR() => new()
    {
        UNIBUSLatch = UNIBUSAction.READ,
        CPUBusDriver = RegisterAction.MDR,
        ALUAction = new ALUAction(ALUOperation.ADD, 
            decoded.Registers[currentRegister], 0),
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    };
    
    private static SignalSet EA_RAM_MAR() => new()
    {
        UNIBUSLatch = UNIBUSAction.READ,
        CPUBusDriver = RegisterAction.MDR,
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    };
    private static SignalSet EA_RAM_MDR() => new()
    {
        UNIBUSLatch = UNIBUSAction.READ,
        CPUBusDriver = RegisterAction.MDR,
        CPUBusLatcher = RegisterAction.TMP,
    };// EXIT
}