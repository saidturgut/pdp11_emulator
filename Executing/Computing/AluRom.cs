namespace pdp11_emulator.Executing.Computing;

public partial class AluRom
{
    protected static readonly Func<AluInput, AluOutput>[] Operations =
    [
        NONE, PASS ,SUB, AND, NAND, OR, ADD, // DOUBLE OPERANDS
        ZERO, NOT, INC, DEC, NEG, ADC, SBC, // SINGLE OPERANDS
        ASR, ASL, ROR, ROL, SWAB, // BITWISE OPERATIONS
    ];
}

public enum AluOperation
{
    NONE, PASS ,SUB, AND, NAND, OR, ADD, // DOUBLE OPERANDS
    ZERO, NOT, INC, DEC, NEG, ADC, SBC, // SINGLE OPERANDS
    ASR, ASL, ROR, ROL, SWAB, // BITWISE OPERATIONS
};

[Flags]
public enum AluFlag
{
    None = 0,
    Trace = 1 << 11,
    Negative = 1 << 12,
    Zero = 1 << 13,
    Overflow = 1 << 14,
    Carry = 1 << 15,
}
