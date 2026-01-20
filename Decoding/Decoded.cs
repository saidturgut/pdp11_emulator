namespace pdp11_emulator.Decoding;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public struct Decoded()
{
    public Register[] Drivers = new Register[2];

    public AluOperation AluOperation = AluOperation.NONE;
    public AluFlag FlagMask = AluFlag.None;
    
    public bool ByteMode = false;
    
    public readonly List<MicroCycle> MicroCycles 
        = [MicroCycle.FETCH_READ, MicroCycle.PC_INC
            , MicroCycle.FETCH_LATCH, MicroCycle.DECODE];
}