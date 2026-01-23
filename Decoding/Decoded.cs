namespace pdp11_emulator.Decoding;
using Executing.Computing;
using Executing.Components;
using Signaling.Cycles;
using Signaling;

public struct Decoded()
{
    public Register[] Registers = new Register[2];

    public Condition Condition = Condition.NONE;
    public Operation Operation = Operation.NONE;
    
    public UniBusDriving MemoryMode = UniBusDriving.NONE;
    
    public PswFlag FlagMask = PswFlag.NONE;
    
    public bool SuppressTrace = false;
    public bool ByteMode = false;
    public ushort CycleLatch = 0;
    
    public List<MicroCycle> MicroCycles = [MicroCycle.EMPTY];
}