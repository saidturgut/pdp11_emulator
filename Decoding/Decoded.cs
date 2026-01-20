namespace pdp11_emulator.Decoding;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public struct Decoded()
{
    public Register[] Drivers = new Register[2];

    public Condition Condition = Condition.NONE;
    public Operation Operation = Operation.NONE;
    
    public PswFlag FlagMask = PswFlag.None;
    
    public CycleMode CycleMode = CycleMode.NONE;
    public ushort CycleLatch = 0;
    
    public readonly List<MicroCycle> MicroCycles 
        = [MicroCycle.FETCH_READ, MicroCycle.PC_INC
            , MicroCycle.FETCH_LATCH, MicroCycle.DECODE];
}