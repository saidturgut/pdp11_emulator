using pdp11_emulator.Signaling.Cycles;

namespace pdp11_emulator.Decoding;
using Multiplexer;

public class Decoder : DecoderMux
{
    private readonly MicroCycle[] FixedOpcodes =
    [
        MicroCycle.HALT, // 0x1
    ];
    
    public Decoded Decode(ushort ir)
    {
        SetNibbles(ir);
        
        if (ir == 0)
            return ZERO_OPERAND(FixedOpcodes[ir]);
        
        if (fzzz is (>= 1 and <= 6) or (>= 9 and <= 0xE))
            return TWO_OPERAND(ir);
        
        if (fzzz is 0 or 8 && 
            zfzz is >= 0xA and <= 0xC)
            return ONE_OPERAND(ir);
        
        throw new Exception($"ILLEGAL OPCODE ON ROW {fzzz}, COLUMN  {zfzz}");
    }
    
    private void SetNibbles(ushort ir)
    {
        fzzz = (byte)(ir >> 12);
        zfzz = (byte)((ir & 0xF00) >> 8);
        zzfz = (byte)((ir & 0xF0) >> 4);
        zzzf = (byte)(ir & 0xF);
    }
}