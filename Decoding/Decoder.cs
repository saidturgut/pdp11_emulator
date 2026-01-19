using pdp11_emulator.Signaling.Cycles;

namespace pdp11_emulator.Decoding;
using Multiplexer;

public class Decoder : DecoderMux
{
    private readonly MicroCycle[] FixedOpcodes =
    [
        MicroCycle.HALT, // 0x1
    ];
    
    public Decoded Decode(ushort IR)
    {
        if (IR < FixedOpcodes.Length)
            return FIXED_OPCODE(FixedOpcodes[IR]);
        
        if ((IR & 0xFFF8) == 0x0080)
            return RTS(IR);
        if ((IR & 0xFFF8) == 0x0800)
            return JSR(IR);
        if ((IR & 0xF800) == 0x0800 || (IR & 0xF800) == 0x8800)
            return SINGLE_OPERAND(IR);
        if ((IR & 0xF000) == 0x1000)
            return BRANCH(IR);
        if ((IR & 0x7000) >= 0x1000 && (IR & 0x7000) <= 0x6000)
            return DOUBLE_OPERAND(IR);

        throw new Exception("ILLEGAL OPCODE!!");    
    }
}