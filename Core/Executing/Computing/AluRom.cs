namespace pdp11_emulator.Core.Executing.Computing;

public class AluRom
{
    
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

public enum AluOperation
{
    NONE ,MOV ,ADD, SUB, OR, AND, XOR, NAND, ZERO
}