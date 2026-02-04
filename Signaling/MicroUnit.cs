using pdp1120.Decoding.Multiplexer;

namespace pdp1120.Signaling;
using Executing.Components;
using Decoding;
using Cycles;

// OWNS SEQUENCER
public partial class MicroUnit : MicroUnitRom
{
    private readonly Decoder Decoder = new();
    
    private ushort currentCycle;

    private bool WAIT;
    
    public bool HALT { get; private set; }
    public bool BOUNDARY { get; private set; }

    public SignalSet Emit(ushort ir, TrapUnit trapUnit, Mode mode)
    {
        if (decoded.MicroCycles[currentCycle] is MicroCycle.DECODE)
        {
            decoded = Decoder.Decode(ir, trapUnit, mode);
            currentCycle = 0;
        }
        
        Console.WriteLine($"\nCURRENT CYCLE : {decoded.MicroCycles[currentCycle]}");

        return MicroCycles[(int)decoded.MicroCycles[currentCycle]]();
    }

    public bool START() => currentCycle == 0 && decoded.MicroCycles[currentCycle] is not MicroCycle.FETCH_READ;
    
    public void Clear(TrapUnit trapUnit)
    {
        if (trapUnit.TRAP) WAIT = false;
        
        if(!WAIT) decoded = !trapUnit.TRAP ? DecoderMux.FETCH() : DecoderMux.TRAP();
        
        registersIndex = 0;
        currentCycle = 0;
        BOUNDARY = false;
    }
}