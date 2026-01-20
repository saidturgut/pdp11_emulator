namespace pdp11_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux : DecoderRom
{
    protected byte fzzz;
    protected byte zfzz;
    protected byte zzfz;
    protected byte zzzf;
    
    protected Decoded ZERO_OPERAND(MicroCycle microCycle)
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