namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded DOUBLE_OPERAND(ushort opcode)
    {
        byte operation = (byte)((opcode >> 12) & 0xF);
        bool byteMode =  ((opcode >> 11) & 1) != 0;
        
        DoubleOperandType type = (DoubleOperandType)operation;
        
        Decoded decoded = new()
        {
            Drivers = [(RegisterAction)((opcode >> 6)  & 0x7), 
                (RegisterAction)(opcode & 0x7)],
            AluOperation = DoubleOperandTable[(ushort)type],
            FlagMask = FlagMasks[type == DoubleOperandType.MOV ? 
                FlagMask.NZO : FlagMask.NZOC], 
            StepSize = 2,
        };
        
        // EFFECTIVE ADDRESS ENGINE
        if (type is not DoubleOperandType.ADD)
            decoded.StepSize = (byte)(byteMode ? 1 : 2);
        
        decoded.MicroCycles.AddRange(EaEngine[(opcode >> 9) & 0x7]);        
        decoded.MicroCycles.AddRange(EaEngine[(opcode >> 3)  & 0x7]);

        // EXECUTE ENGINE
        decoded.MicroCycles.Add(type is not (DoubleOperandType.MOV or DoubleOperandType.CMP or DoubleOperandType.BIT)
            ? MicroCycle.EXE_WRITE_BACK : MicroCycle.EXE_FLAGS);

        // WRITE BACK ENGINE
        if (type is not (DoubleOperandType.CMP or DoubleOperandType.BIT))
        {
            decoded.MicroCycles.Add(((opcode >> 3) & 0x38) == 0 
                ? MicroCycle.WRITE_BACK_REG : MicroCycle.WRITE_BACK_RAM);
        }
        
        return decoded;
    }

    private enum DoubleOperandType
    {
        MOV = 0x1, CMP = 0x2, BIT = 0x3, BIC = 0x4, BIS = 0x5, 
        ADD = 0x6,
    }

    public AluOperation[] DoubleOperandTable =
    [
        AluOperation.NONE,
        AluOperation.PASS, AluOperation.SUB, 
        AluOperation.AND, AluOperation.NAND, 
        AluOperation.OR, AluOperation.ADD,
    ];
}