namespace pdp11_emulator.Signaling;
using Decoding;
using Cycles;

// SEQUENCER, TRAP UNIT
public partial class ControlUnit : ControlUnitRom
{
    private readonly Decoder Decoder = new();
    
    private ushort currentCycle;

    public bool BOUNDARY;
    
    public bool HALT;

    public SignalSet Emit(ushort ir, TrapUnit trapUnit)
    {
        BOUNDARY = false;

        if (trapUnit.TRAP && currentCycle == 0)
            decoded = Trap(trapUnit);
        
        Console.WriteLine("CURRENT CYCLE : " +  decoded.MicroCycles[currentCycle]);
        
        if (decoded.MicroCycles[currentCycle] is MicroCycle.DECODE)
        {
            decoded = Decoder.Decode(ir, trapUnit);
            return new SignalSet();
        }

        return MicroCycles[(int)decoded.MicroCycles[currentCycle]]();
    }

    private Decoded Trap(TrapUnit trapUnit)
    {
        Decoded decoded = new Decoded()
        {
            CycleLatch = trapUnit.VECTOR,
        };
        
        return decoded;
    }
    
    public void Clear()
    {
        decoded = new Decoded();
        registersIndex = 0;
        currentCycle = 0;
        BOUNDARY = false;
    }
}