namespace pdp11_emulator.Decoding;
using Signaling.Cycles;
using Signaling;
using Multiplexer;

public class Decoder : DecoderMux
{
    private readonly Dictionary<ushort, MicroCycle> FixedOpcodes = new()
    {
        {0x00, MicroCycle.HALT}, // HALT
        {0xA0, MicroCycle.EMPTY} // NOP
    };
    
    public Decoded Decode(ushort ir, TrapUnit trapUnit)
    {
        SetNibbles(ir);
        
        if (ir == 0)
            return ZERO_OPERAND(FixedOpcodes[ir]);
        
        if (fzzz is (>= 1 and <= 6) or (>= 9 and <= 0xE))
            return TWO_OPERAND(ir);
        
        if ((fzzz is 0 or 8 && zfzz is >= 0xA and <= 0xC) 
            || ir >> 6 is 3 or 0x37)
            return ONE_OPERAND(ir);
        
        if(ir >> 6 == 1)
            return JMP(ir);
        
        switch (ir >> 9)
        {
            case 0x4: return JSR(ir);
            case 0x3F: return SOB(ir);
        }
        
        if (ir >> 3 == 0x10)
            return RTS(ir);
        
        if (((fzzz == 0 && zfzz >= 1) || fzzz == 8) && zfzz <= 7)
            return BRANCH(ir);
        
        if((ir & 0xFC00) == 0)
            return PSW(ir);
        
        // IF FALL THROUGH ->
        trapUnit.Request(TrapVector.ILLEGAL_INSTRUCTION, true);
        return new Decoded();
    }
    
    private void SetNibbles(ushort ir)
    {
        fzzz = (byte)(ir >> 12);
        zfzz = (byte)((ir & 0xF00) >> 8);
        zzfz = (byte)((ir & 0xF0) >> 4);
        zzzf = (byte)(ir & 0xF);
    }
}