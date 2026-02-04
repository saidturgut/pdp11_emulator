namespace pdp1120.Decoding.Multiplexer;
using Executing.Components;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux : DecoderRom
{
    protected static byte fzzz;
    protected static byte zfzz;

    public static Decoded FETCH() => new()
        { MicroCycles = [MicroCycle.FETCH_READ, MicroCycle.PC_INC, MicroCycle.FETCH_LATCH, MicroCycle.DECODE] };
    
    protected static Decoded ZERO_OPERAND(MicroCycle microCycle)
        => new() { MicroCycles = [microCycle] };
}
