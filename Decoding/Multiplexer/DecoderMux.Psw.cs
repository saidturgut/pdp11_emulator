namespace pdp1120.Decoding.Multiplexer;
using Executing.Components;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded PSW(ushort ir)
    {
        ushort maskBits = (ushort)(ir & 0xF);
        
        PswFlag mask = PswFlag.NONE;

        if ((maskBits & 0x8) != 0) mask |= PswFlag.NEGATIVE;
        if ((maskBits & 0x4) != 0) mask |= PswFlag.ZERO;
        if ((maskBits & 0x2) != 0) mask |= PswFlag.OVERFLOW;
        if ((maskBits & 0x1) != 0) mask |= PswFlag.CARRY;

        Decoded decoded = new()
        {
            CycleLatch = (ushort)((ir & 0x00F0) == 0x00B0 ? 0xFFFF : 0x0000),
            FlagMask = mask,
            MicroCycles = [MicroCycle.EXECUTE_PSW]
        };
        
        return decoded;
    }

    protected Decoded SPL(ushort ir)
    {
        Decoded decoded = new()
        {
            CycleLatch = (byte)((ir & 0x7) << 5),
            FlagMask = PswFlag.P5 | PswFlag.P6 | PswFlag.P7,
            MicroCycles = [MicroCycle.EXECUTE_PSW]
        };
        return decoded;
    }
}