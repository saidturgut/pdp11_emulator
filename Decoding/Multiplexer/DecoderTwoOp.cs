namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded TWO_OPERAND(ushort ir)
    {
        // SELECT TYPE
        byte operation = fzzz;
        bool byteMode = false;
        if (fzzz != 6 && (ir & 0x8000) != 0)
        {
            byteMode = true;
            operation -= 8;
        }
        if (fzzz == 0xE) operation = 7;
        
        TwoOperandType type = (TwoOperandType)operation;
        
        // ASSIGN ESSENTIALS
        Decoded decoded = new()
        {
            Drivers = [(Register)((ir >> 6)  & 0x7), (Register)(ir & 0x7)],
            AluOperation = TwoOperandTable[(ushort)type],
            FlagMask = FlagMasks[type == TwoOperandType.MOV ? FlagMask.NZO : FlagMask.NZOC],
            ByteMode = byteMode,
        };
        
        // EFFECTIVE ADDRESS ENGINE
        decoded.MicroCycles.AddRange(EaEngine[(ir >> 9) & 0x7]);
        decoded.MicroCycles.Add(MicroCycle.EA_TOGGLE);
        decoded.MicroCycles.AddRange(EaEngine[(ir >> 3) & 0x7]);
        decoded.MicroCycles.Add(MicroCycle.EA_TOGGLE);

        // EXECUTE ENGINE
        decoded.MicroCycles.Add(type is not (TwoOperandType.MOV or TwoOperandType.CMP or TwoOperandType.BIT)
            ? MicroCycle.EXE_LATCH : MicroCycle.EXE_FLAGS);

        // WRITE BACK ENGINE
        if (type is not (TwoOperandType.CMP or TwoOperandType.BIT))
        {
            decoded.MicroCycles.Add(
                ((ir >> 3) & 0x7) == 0 ? MicroCycle.WRITE_BACK_TWO : MicroCycle.WRITE_BACK_RAM
            );
        }
        
        return decoded;
    }

    private enum TwoOperandType
    {
        MOV = 0x1, CMP = 0x2, BIT = 0x3, BIC = 0x4, BIS = 0x5, 
        ADD = 0x6, SUB = 0x7,
    }

    public AluOperation[] TwoOperandTable =
    [
        AluOperation.NONE,
        AluOperation.PASS, AluOperation.SUB, 
        AluOperation.BIT, AluOperation.BIC, 
        AluOperation.BIS, AluOperation.ADD,
        AluOperation.SUB,
    ];
}