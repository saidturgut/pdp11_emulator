using pdp11_emulator.Executing.Components;

namespace pdp11_emulator.Executing.Computing;

public partial class AluRom
{
    protected static ushort xFFFF;
    protected static ushort x8000;
    protected static uint x10000;
    protected static bool maskApplied;
    
    protected static void SetMasks(bool byteMode)
    {
        xFFFF = (ushort)(!byteMode ? 0xFFFF : 0xFF);
        x8000 = (ushort)(!byteMode ? 0x8000 : 0x80);
        x10000 = (uint)(!byteMode ? 0x10000 : 0x100);
        maskApplied = byteMode;
    }
    
    protected static readonly Func<AluInput, AluOutput>[] Operations =
    [
        NONE, PASS ,SUB, BIT, BIC, BIS, ADD, // DOUBLE OPERANDS
        ZERO, COM, INC, DEC, NEG, ADC, SBC, // SINGLE OPERANDS
        ASR, ASL, ROR, ROL, SWAB, SXT, // BITWISE OPERATIONS
        BRANCH, // MISC
    ];
}

public enum Operation
{
    NONE, PASS ,SUB, BIT, BIC, BIS, ADD, // DOUBLE OPERANDS
    ZERO, COM, INC, DEC, NEG, ADC, SBC, // SINGLE OPERANDS
    ASR, ASL, ROR, ROL, SWAB, SXT, // BITWISE OPERATIONS
    BRANCH, // MISC
};

public struct AluInput
{
    public Operation Operation;
    public ushort A;
    public ushort B;
    public bool ByteMode;
    public ControlWord Cw;
}

public struct AluOutput
{
    public ushort Result;
    public ushort Flags;
}

[Flags]
public enum PswFlag
{
    None = 0,
    Trace = 1 << 11,
    Negative = 1 << 12,
    Zero = 1 << 13,
    Overflow = 1 << 14,
    Carry = 1 << 15,
}
