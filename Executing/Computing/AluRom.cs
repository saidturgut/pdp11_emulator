namespace pdp11_emulator.Executing.Computing;

public partial class AluRom
{
    protected static ushort xFFFF;
    protected static ushort x8000;
    protected static uint x10000;
    
    protected static void SetMasks(bool byteMode)
    {
        xFFFF = (ushort)(!byteMode ? 0xFFFF : 0xFF);
        x8000 = (ushort)(!byteMode ? 0x8000 : 0x80);
        x10000 = (uint)(!byteMode ? 0x10000 : 0x100);
    }
    
    protected static readonly Func<AluInput, AluOutput>[] Operations =
    [
        NONE, PASS ,SUB, BIT, BIC, BIS, ADD, // DOUBLE OPERANDS
        ZERO, COM, INC, DEC, NEG, ADC, SBC, // SINGLE OPERANDS
        ASR, ASL, ROR, ROL, SWAB, // BITWISE OPERATIONS
    ];
}

public enum AluOperation
{
    NONE, PASS ,SUB, BIT, BIC, BIS, ADD, // DOUBLE OPERANDS
    ZERO, COM, INC, DEC, NEG, ADC, SBC, // SINGLE OPERANDS
    ASR, ASL, ROR, ROL, SWAB, // BITWISE OPERATIONS
};

public struct AluInput
{
    public AluOperation Operation;
    public ushort A;
    public ushort B;
    public bool C;
    public bool ByteMode;
}

public struct AluOutput
{
    public ushort Result;
    public ushort Flags;
}

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
