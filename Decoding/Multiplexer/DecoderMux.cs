namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Components;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux : DecoderRom
{
    protected static byte fzzz;
    protected static byte zfzz;
    protected static byte zzfz;
    protected static byte zzzf;

    public static Decoded FETCH() => new()
        { MicroCycles = [MicroCycle.FETCH_READ, MicroCycle.PC_INC, MicroCycle.FETCH_LATCH, MicroCycle.DECODE] };
    
    protected static Decoded ZERO_OPERAND(MicroCycle microCycle)
        => new() { MicroCycles = [microCycle] };
}
