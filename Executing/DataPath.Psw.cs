namespace pdp11_emulator.Executing;
using Signaling;
using Computing;
using Components;

public partial class DataPath
{
    private readonly ControlWord Cw = new ();

    private bool zeroLatch;

    public byte GetPriorityLevel() => Cw.Priority;
    
    public void PswAction(TrapUnit trapUnit)
    {
        if (Cw.Trace) trapUnit.Request(TrapVector.TRACE);
        
        if (signals.PswAction is not null)
        {
            Environment.Exit(5);
            
            SetFlags(signals.PswAction.Value.Buffer, signals.FlagMask);
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
        
        Condition.SOB => !zeroLatch,
        
        _ => throw new Exception("INVALID CONDITION!!")
    };

    private void SetFlags(ushort flags, PswFlag mask)
        => Access(Register.PSW).Set((ushort)
            ((Access(Register.PSW).Get() & (ushort)~mask) | (flags & (ushort)mask)));
}
