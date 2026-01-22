namespace pdp11_emulator.Signaling;
using Decoding;
using Cycles;

// OWNS SEQUENCER
public partial class MicroUnit : MicroUnitRom
{
    private readonly Decoder Decoder = new();
    
    private ushort currentCycle;

    public bool HALT { get; private set; }
    public bool WAIT { get; private set; }
    
    public bool BOUNDARY { get; private set; }

    public SignalSet Emit(ushort ir, TrapUnit trapUnit)
    {
        if (decoded.MicroCycles[currentCycle] is MicroCycle.DECODE)
        {
            decoded = Decoder.Decode(ir, trapUnit);
            currentCycle = 0;
        }
        
        Console.WriteLine("CURRENT CYCLE : " +  decoded.MicroCycles[currentCycle]);

        return MicroCycles[(int)decoded.MicroCycles[currentCycle]]();
    }
    
    public void Clear(TrapUnit trapUnit)
    {
        if (trapUnit.TRAP) WAIT = false;
        
        if(!WAIT) decoded = !trapUnit.TRAP ? Decoder.FETCH() : Decoder.TRAP();
        
        registersIndex = 0;
        currentCycle = 0;
        BOUNDARY = false;
    }
}