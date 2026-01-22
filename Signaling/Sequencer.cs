namespace pdp11_emulator.Signaling;
using Decoding;
using Cycles;

public partial class MicroUnit
{
    public void Advance(TrapUnit trapUnit)
    {
        switch (decoded.MicroCycles[currentCycle])
        {
            case MicroCycle.HALT: HALT = true; return;
            case MicroCycle.WAIT: WAIT = true; break;
        }
        
        if (decoded.MicroCycles[currentCycle] == MicroCycle.INDEX_TOGGLE)
            registersIndex = (byte)(registersIndex == 0 ? 1 : 0);

        if (currentCycle == decoded.MicroCycles.Count - 1)
        {
            if (trapUnit.TRAP) trapUnit.Clear();
            
            BOUNDARY = true;
        }
        else
        {
            currentCycle++;
        }
    }
}