namespace pdp11_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux : DecoderRom
{
    protected Decoded FIXED_OPCODE(MicroCycle microCycle)
    {
        Decoded decoded = new();
        decoded.MicroCycles.Add(microCycle);
        return decoded;
    }
    
    protected Decoded BRANCH(ushort opcode)
    {
        Decoded decoded = new();
        return decoded;
    }
    
    protected Decoded JSR(ushort opcode)
    {
        Decoded decoded = new();
        return decoded;
    }
    
    protected Decoded RTS(ushort opcode)
    {
        Decoded decoded = new();
        return decoded;
    }
}