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
            Registers = [(Register)((ir >> 6) & 0x7), (Register)(ir & 0x7)],
            Operation = TwoOperandTable[(ushort)type],
            FlagMask = FlagMasks.Table[type == TwoOperandType.MOV ? FlagMask.NZO : FlagMask.NZOC],
            MicroCycles =
            [
                ..AddressEngine[(ir >> 9) & 0x7],

                MicroCycle.INDEX_TOGGLE,

                ..AddressEngine[(ir >> 3) & 0x7],
                
                type is not (TwoOperandType.MOV or TwoOperandType.CMP or TwoOperandType.BIT)
                    ? MicroCycle.EXECUTE_EA : MicroCycle.EXECUTE_FLAGS,
            ]
        };
        
        // EXECUTE ENGINE
        if (type is not (TwoOperandType.CMP or TwoOperandType.BIT))
        {
            decoded.MicroCycles.Add(((ir >> 3) & 0x7) == 0 ? MicroCycle.TMP_TO_REG : MicroCycle.TMP_TO_UNI);
        }
        
        if (byteMode) decoded.CycleMode = CycleMode.BYTE_MODE;
        
        return decoded;
    }

    private enum TwoOperandType
    {
        MOV = 0x1, CMP = 0x2, BIT = 0x3, BIC = 0x4, BIS = 0x5, 
        ADD = 0x6, SUB = 0x7,
    }

    public Operation[] TwoOperandTable =
    [
        Operation.NONE,
        Operation.PASS, Operation.SUB, 
        Operation.BIT, Operation.BIC, 
        Operation.BIS, Operation.ADD,
        Operation.SUB,
    ];
}