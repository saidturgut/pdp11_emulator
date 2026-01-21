namespace pdp11_emulator.Executing.Components;

public class ControlWord
{
    public bool Carry;
    public bool Overflow;
    public bool Zero;
    public bool Negative;
    public bool Trace;

    public void SetFlags(ushort psw)
    {
        Carry = (psw & (ushort)PswFlag.Carry) != 0;
        Overflow = (psw & (ushort)PswFlag.Overflow) != 0;
        Zero = (psw & (ushort)PswFlag.Zero) != 0;
        Negative = (psw & (ushort)PswFlag.Negative) != 0;
        Trace = (psw & (ushort)PswFlag.Trace) != 0;
    }
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
