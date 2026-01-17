namespace pdp11_emulator.Core.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded DOUBLE_OPERAND(ushort opcode)
    {
        byte operation = (byte)((opcode & 0xF000) >> 12);
        bool byteMode =  ((opcode >> 11) & 0x1) == 1;
        
        Decoded decoded = new()
        {
            Drivers = [(RegisterAction)((opcode >> 6)  & 0x7), (RegisterAction)(opcode & 0x7)],
            AluOperation = DoubleOperandTable[operation],
            StepSize = 2,
        };
        
        Console.WriteLine(opcode);
        
        Console.WriteLine(decoded.Drivers[0]);
        Console.WriteLine(decoded.Drivers[1]);

        Environment.Exit(5);
        
        DoubleOperandType type = (DoubleOperandType)operation;

        // EFFECTIVE ADDRESS ENGINE
        if (type is not (DoubleOperandType.ADD or DoubleOperandType.SUB))
            decoded.StepSize = (byte)(byteMode ? 1 : 2);
        
        decoded.MicroCycles.AddRange(EaEngine[(opcode >> 9) & 0x7]);        
        decoded.MicroCycles.AddRange(EaEngine[(opcode >> 3)  & 0x7]);

        // EXECUTE ENGINE
        if (type is not DoubleOperandType.MOV)
            decoded.MicroCycles.Add(MicroCycle.ALU_EXECUTE);
        
        // WRITE BACK ENGINE
        if (type is not (DoubleOperandType.CMP or DoubleOperandType.BIT))
        {
            decoded.MicroCycles.Add(((opcode >> 3) & 0x7) == 0 ? 
                MicroCycle.WRITE_BACK_REG : MicroCycle.WRITE_BACK_RAM);
        }
        
        return decoded;
    }

    private enum DoubleOperandType
    {
        MOV = 0b01, CMP = 0b10, BIT = 0b11, BIC = 0b100, BIS = 0b101, 
        ADD = 0b110, SUB = 0b1110
    }

    public AluOperation[] DoubleOperandTable =
    [
        AluOperation.NONE,
        AluOperation.ADD,
        AluOperation.SUB, AluOperation.SUB,
        AluOperation.AND, AluOperation.AND,
        AluOperation.OR,
    ];
}