namespace pdp11_emulator.Signaling;

public class TrapUnitRom
{
    protected static readonly Dictionary<TrapVector, Trap> TrapTable = new()
    {
        // ABORT
        {TrapVector.ODD_ADDRESS, new(){ Address = 0x4 , Abort = true, Priority = 0 } }, 
        {TrapVector.BUS_ERROR, new(){ Address = 0x4 , Abort = true, Priority = 0 } },
        {TrapVector.ILLEGAL_INSTRUCTION, new(){ Address = 0x10 , Abort = true, Priority = 1 } }, 
        
        //INTERRUPTS -> 2
        
        // NOT ABORT
        {TrapVector.BPT, new(){ Address = 0x14, Priority = 3 } }, 
        {TrapVector.TRACE, new(){ Address = 0x14, Priority = 3 } }, 
        {TrapVector.IOT, new(){ Address = 0x20, Priority = 4 } }, 
        {TrapVector.EMT, new(){ Address = 0x30, Priority = 5 } },
        {TrapVector.TRAP, new(){ Address = 0x34, Priority = 5 } }, 
    };
}

public enum TrapVector
{
    ODD_ADDRESS, BUS_ERROR, // 0
    ILLEGAL_INSTRUCTION, // 1
    BPT, TRACE,  // 3
    IOT, // 4
    EMT, TRAP, // 5
}

public struct Trap()
{
    public ushort Address;
    public bool Abort = false;
    public byte Priority;
}