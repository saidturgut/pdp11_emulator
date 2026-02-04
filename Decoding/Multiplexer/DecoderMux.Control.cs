namespace pdp1120.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected static Decoded JMP(ushort ir) => new()
    {
        Registers = [(Register)(ir & 0x7), Register.PC],

        MicroCycles =
        [
            ..AddressEngine[(ir >> 3) & 0x7],
            MicroCycle.INDEX_TOGGLE, MicroCycle.TMP_TO_REG
        ]
    };

    protected static Decoded JSR(ushort ir) => new()
    {
        Registers = [(Register)((ir >> 6) & 0x7), (Register)(ir & 0x7)],
        Operation = Operation.SUB,
        MemoryMode = UniBusDriving.WRITE_WORD,
        
        MicroCycles =
        [
            MicroCycle.REG_TO_TEMP,
            MicroCycle.REG_ALU,
            MicroCycle.REG_TO_UNI,
            MicroCycle.PC_TO_REG,

            MicroCycle.INDEX_TOGGLE,

            ..AddressEngine[(ir >> 3) & 0x7],
            
            MicroCycle.DST_TO_PC,
        ]
    };

    protected static Decoded RTS(ushort ir) => new()
    {
        Registers = [(Register)(ir & 0x7), Register.PC],
        Operation = Operation.ADD,
        CycleLatch = 2,
        MemoryMode = UniBusDriving.READ_WORD,
        
        MicroCycles =
        [
            MicroCycle.REG_TO_TMP, 
            MicroCycle.SP_TO_UNI,
            MicroCycle.MDR_TO_REG, 
            MicroCycle.SP_ALU,
            MicroCycle.INDEX_TOGGLE,
            MicroCycle.TMP_TO_REG,
        ]
    };

    protected static Decoded MARK(ushort ir) => new()
    {
        Registers = [Register.PC, Register.SP_U],
        CycleLatch = (ushort)((ir & 0x3F) * 2),
        Operation = Operation.ADD,
        MemoryMode = UniBusDriving.READ_WORD,
        MicroCycles =
        [
            MicroCycle.SP_ALU,
            MicroCycle.SP_TO_UNI,
            MicroCycle.MDR_TO_REG,
            MicroCycle.INDEX_TOGGLE,
            MicroCycle.REG_INC,
        ]
    };
}