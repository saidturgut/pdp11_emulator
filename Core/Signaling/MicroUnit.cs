namespace pdp11_emulator.Core.Signaling;
using Decoding;
using Cycles;

public class MicroUnit : MicroUnitRom
{
    private readonly Decoder Decoder = new();
    
    private ushort currentCycle;
    private bool interrupt;

    public SignalSet Emit()
    {
        if (interrupt)
        {
            return new SignalSet();
        }

        return MicroCycles[(int)decoded.MicroCycles[currentCycle]]();
    }

    public void Decode(ushort ir)
        => decoded = Decoder.Decode(ir);

    public void Advance()
    {
        if (interrupt)
        {
            return;
        }

        if (currentCycle == decoded.MicroCycles.Count - 1)
        {
            currentCycle = 0;
        }
        else
        {
            currentCycle++;
        }
    }
}