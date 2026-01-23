namespace pdp11_emulator.Executing;
using Signaling;
using Computing;
using Components;

public partial class DataPath
{
    private readonly ControlWord Cw = new ();

    private bool zeroLatch;

    public byte GetCw() => Cw.PRIORITY;
    
    public void PswAction()
    {
        if (Signals.PswAction is not null)
        {
            PswSet(Signals.PswAction.Value.Buffer, Signals.FlagMask);
        }
    }

    private bool CheckCondition() => Signals.Condition switch
    {
        Condition.BR => true,
        
        Condition.BEQ => Cw.ZERO,
        Condition.BNE => !Cw.ZERO,
        Condition.BMI => Cw.NEGATIVE,
        Condition.BPL => !Cw.NEGATIVE,
        Condition.BCS => Cw.CARRY,
        Condition.BCC => !Cw.CARRY,
        Condition.BVS => Cw.OVERFLOW,
        Condition.BVC => !Cw.OVERFLOW,

        Condition.BGE => Cw.NEGATIVE == Cw.OVERFLOW,
        Condition.BLT => Cw.NEGATIVE != Cw.OVERFLOW,
        Condition.BLE => Cw.ZERO || Cw.NEGATIVE != Cw.OVERFLOW,
        Condition.BGT => !Cw.ZERO && Cw.NEGATIVE == Cw.OVERFLOW,
        Condition.BLOS => Cw.CARRY || Cw.ZERO,
        Condition.BHI => !Cw.CARRY && !Cw.ZERO,
        
        Condition.SOB => !zeroLatch,
        
        _ => throw new Exception("INVALID CONDITION!!")
    };

    private void PswSet(ushort flags, PswFlag mask)
        => Access(Register.PSW).Set((ushort)
            ((Access(Register.PSW).Get() & (ushort)~mask) | (flags & (ushort)mask)));
}
