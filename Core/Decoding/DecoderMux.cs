using pdp11_emulator.Core.Signaling.Cycles;

namespace pdp11_emulator.Core.Decoding;

public partial class DecoderMux
{
    protected Decoded DOUBLE_OPERAND(ushort opcode)
    {
        byte operation = (byte)((opcode & 0xF000) >> 12);
        ushort operands = (ushort)(opcode & 0xFFF);
        byte source = (byte)((operands & 0xFC0) >> 6);
        byte destination = (byte)(operands & 0x3F);
        
        Decoded decoded = new()
        {
            Registers = [(RegisterAction)(zzz_xxx(source)), 
                (RegisterAction)zzz_xxx(destination)],
        };
        decoded.MicroCycles.AddRange(EaEngine[xxx_zzz(source)]);
        decoded.MicroCycles.AddRange(EaEngine[xxx_zzz(destination)]);

        return decoded;
    }
    
    protected Decoded SINGLE_OPERAND(ushort opcode)
    {
        ushort operation = (ushort)((opcode & 0xFFC0) >> 6);
        byte source = (byte)(opcode & 0x3F);
        
        Decoded decoded = new()
        {
            Registers = [(RegisterAction)(zzz_xxx(source))],
        };
        decoded.MicroCycles.AddRange(EaEngine[xxx_zzz(source)]);
        
        return decoded;
    }

    protected Decoded BRANCH(ushort opcode)
    {
        return new Decoded();
    }
    
    protected Decoded JSR(ushort opcode)
    {
        Decoded decoded = new() { };
        return decoded;
    }
    
    protected Decoded RTS(ushort opcode)
    {
        return new Decoded();
    }
    
    private byte xxx_zzz(byte input) => (byte)((input & 0b111_000) >> 3);
    private byte zzz_xxx(byte input) => (byte)(input & 0b000_111);
}