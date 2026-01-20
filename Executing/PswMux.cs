namespace pdp11_emulator.Executing;
using Signaling;
using Computing;
using Components;

public partial class DataPath
{
    private readonly ControlWord Cw = new ();
    
    public void PswAction()
    {
        if (signals.PswAction is not null)
        {
            CommitFlags(signals.PswAction.Value.Buffer, signals.FlagMask);
        }
    }

    private bool CheckCondition() => signals.Condition switch
    {
        Condition.BR => true,
        
        Condition.BEQ => Cw.Zero,
        Condition.BNE => !Cw.Zero,
        Condition.BMI => Cw.Negative,
        Condition.BPL => !Cw.Negative,
        Condition.BCS => Cw.Carry,
        Condition.BCC => !Cw.Carry,
        Condition.BVS => Cw.Overflow,
        Condition.BVC => !Cw.Overflow,

        Condition.BGE => Cw.Negative == Cw.Overflow,
        Condition.BLT => Cw.Negative != Cw.Overflow,
        Condition.BLE => Cw.Zero || Cw.Negative != Cw.Overflow,
        Condition.BGT => !Cw.Zero && Cw.Negative == Cw.Overflow,
        Condition.BLOS => Cw.Carry || Cw.Zero,
        Condition.BHI => !Cw.Carry && !Cw.Zero,
        
        Condition.NONE => throw new Exception("MISSING CONDITION!!"),
        _ => throw new Exception("INVALID CONDITION!!")
    };

    private void CommitFlags(ushort flags, PswFlag mask)
        => Access(Register.PSW).Set((ushort)
            ((Access(Register.PSW).Get() & (ushort)~mask) | (flags & (ushort)mask)));
}