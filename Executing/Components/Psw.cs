namespace pdp11_emulator.Executing.Components;

public class Psw : RegisterObject
{
    public bool CARRY { get; private set; }
    public bool OVERFLOW { get; private set; }
    public bool ZERO { get; private set; }
    public bool NEGATIVE { get; private set; }
    
    public bool TRACE { get; private set; }

    public byte PRIORITY { get; private set; }
    
    // 00 KERNEL, 11 USER
    public Mode PMOD { get; private set; } 
    public Mode CMOD { get; private set; }

    private PswFlag FlagMask = PswFlag.NONE;

    public void SetMask(PswFlag mask)
        => FlagMask = mask;
    
    public override void Set(ushort input)
    {
        value = (ushort)((value & (ushort)~FlagMask) | (input & (ushort)FlagMask));
        Update();
    }

    private void Update()
    {
        CARRY = (value & (ushort)PswFlag.CARRY) != 0;
        OVERFLOW = (value & (ushort)PswFlag.OVERFLOW) != 0;
        ZERO = (value & (ushort)PswFlag.ZERO) != 0;
        NEGATIVE = (value & (ushort)PswFlag.NEGATIVE) != 0;
        
        TRACE = (value & (ushort)PswFlag.TRACE) != 0;
        
        PRIORITY = (byte)(((value & (ushort)PswFlag.P7) | (value & (ushort)PswFlag.P6) | (value & (ushort)PswFlag.P5)) >> 5);

        PMOD = (Mode)(((value & (ushort)PswFlag.PMOD1) | (value & (ushort)PswFlag.PMOD2)) >> 12);
        CMOD = (Mode)(((value & (ushort)PswFlag.CMOD1) | (value & (ushort)PswFlag.CMOD2)) >> 14);
    }
}

public enum Mode
{
    KERNEL = 0b00, USER = 0b11
}

[Flags]
public enum PswFlag
{
    NONE = 0,
    
    CARRY = 1 << 0,
    OVERFLOW = 1 << 1,
    ZERO = 1 << 2,
    NEGATIVE = 1 << 3,
    
    TRACE = 1 << 4,
    
    P5 = 1 << 5,
    P6 = 1 << 6,
    P7 = 1 << 7,
    
    PMOD1 = 1 << 12,
    PMOD2 = 1 << 13,
    CMOD1 = 1 << 14,
    CMOD2 = 1 << 15,
}
