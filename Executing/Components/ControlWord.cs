namespace pdp11_emulator.Executing.Components;
using Computing;

public class ControlWord
{
    public bool Carry;
    public bool Overflow;
    public bool Zero;
    public bool Negative;

    public void SetFlags(ushort psw)
    {
        Carry = (psw & (ushort)PswFlag.Carry) != 0;
        Overflow = (psw & (ushort)PswFlag.Overflow) != 0;
        Zero = (psw & (ushort)PswFlag.Zero) != 0;
        Negative = (psw & (ushort)PswFlag.Negative) != 0;
    }
}