namespace pdp11_emulator.Signaling;

public class TrapUnit
{
    private static readonly Dictionary<TrapVector, ushort> VectorTable = new()
    {
        {TrapVector.ODD_ADDRESS, 0x4 }, {TrapVector.ILLEGAL_INSTRUCTION, 0x10 },
        {TrapVector.DIVIDE_BY_ZERO, 0x10 }, {TrapVector.OVERFLOW, 0x10 },
        {TrapVector.BPT, 0x14 }, {TrapVector.IOT, 0x20 }, {TrapVector.EMT, 0x30 },
        {TrapVector.TRACE, 0x14 }, {TrapVector.TRAP, 0x34 }, {TrapVector.POWER_FAIL, 0x24 },
    };

    public ushort VECTOR;
    private bool ABORT;
    public bool TRAP;
    
    public void Request(TrapVector vector, bool abort)
    {
        VECTOR = VectorTable[vector];
        ABORT = abort;
        TRAP = true;
    }

    public bool State()
        => ABORT;
}

public enum TrapVector
{
    ODD_ADDRESS, ILLEGAL_INSTRUCTION, 
    DIVIDE_BY_ZERO, OVERFLOW,
    EMT, TRAP, BPT, IOT,
    TRACE, POWER_FAIL, // AND DEVICE VECTORS
}