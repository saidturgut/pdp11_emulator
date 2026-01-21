namespace pdp11_emulator.Signaling;
using Decoding;
using Cycles;

public partial class ControlUnit
{
    public void Advance()
    {
        if (decoded.MicroCycles[currentCycle] is MicroCycle.HALT)
        {
            HALT = true;
            return;
        }
        
        if (decoded.MicroCycles[currentCycle] == MicroCycle.INDEX_TOGGLE)
            registersIndex = (byte)(registersIndex == 0 ? 1 : 0);
        
        if (currentCycle == decoded.MicroCycles.Count - 1)
        {
            registersIndex = 0;
            currentCycle = 0;
            BOUNDARY = true;
        }
        else
        {
            currentCycle++;
        }
    }
}