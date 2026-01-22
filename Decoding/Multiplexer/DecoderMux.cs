namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Components;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux : DecoderRom
{
    protected byte fzzz;
    protected byte zfzz;
    protected byte zzfz;
    protected byte zzzf;

    public Decoded FETCH() => new()
        { MicroCycles = [MicroCycle.FETCH_READ, MicroCycle.PC_INC, MicroCycle.FETCH_LATCH, MicroCycle.DECODE] };
    
    protected Decoded ZERO_OPERAND(MicroCycle microCycle)
        => new() { MicroCycles = [microCycle] };
}
