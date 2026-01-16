namespace pdp11_emulator.Core.Decoding;

public class Decoder : DecoderMux
{
    public Decoded Decode(ushort IR)
    {
        if ((IR & 0xF000) >= 0x1000 && (IR & 0xF000) <= 0x6000)
            return DOUBLE_OPERAND(IR);
        if ((IR & 0xF800) == 0x0800)
            return SINGLE_OPERAND(IR);
        if ((IR & 0xF000) == 0x1000)
            return BRANCH(IR);
        if ((IR & 0xFFF8) == 0x0800)
            return JSR(IR);
        if ((IR & 0xFFF8) == 0x0080)
            return RTS(IR);

        throw new Exception("ILLEGAL OPCODE!!");
    }
}