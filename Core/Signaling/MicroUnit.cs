namespace pdp11_emulator.Core.Signaling;
using Decoding;
using Cycles;

public class MicroUnit : MicroUnitRom
{
    private readonly Decoder Decoder = new();
    
    private ushort currentCycle;
    
    private bool INTERRUPT;
    private bool TRAP;
    
    public bool HALT;

    public SignalSet Emit(ushort ir)
    {
        if (INTERRUPT)
            return new SignalSet();

        if (decoded.MicroCycles[currentCycle] is MicroCycle.DECODE)
        {
            decoded = Decoder.Decode(ir);
            return new SignalSet();
        }

        return MicroCycles[(int)decoded.MicroCycles[currentCycle]]();
    }
    
    public void Advance()
    {
        Console.WriteLine($"CURRENT CYCLE : {decoded.MicroCycles[currentCycle]}");
        
        if (decoded.MicroCycles[currentCycle] is MicroCycle.HALT)
        {
            HALT = true;
            return;
        }
        
        if (INTERRUPT)
            return;
        
        if (ToggleCycles.Contains(decoded.MicroCycles[currentCycle]))
            registersIndex = (byte)(registersIndex == 0 ? 1 : 0);

        if (currentCycle == decoded.MicroCycles.Count - 1)
        {
            registersIndex = 0;
            currentCycle = 0;
        }
        else
            currentCycle++;
    }
}