namespace pdp1120.Executing;
using Signaling;
using Computing;
using Components;

public partial class DataPath
{
    private readonly Psw Psw = new();
    
    private byte SUPPRESSED;
    
    public void StatusWord(bool START, TrapUnit trapUnit)
    {  
        if (START)
        {
            if (Signals.SuppressTrace) SUPPRESSED = 2;
            
            Psw.Set((ushort)(Psw.CMOD == Mode.KERNEL ? 0 : 0xFFFF), PswFlag.PMOD1 | PswFlag.PMOD2);
            
            if (trapUnit.TRAP)
            {
                Psw.Set(0, PswFlag.CMOD1 | PswFlag.CMOD2); // ENTER KERNEL MODE ON TRAPS
            }
        }

        if (Psw.TRACE && !trapUnit.TRAP && SUPPRESSED == 0)
        {
            trapUnit.Request(TrapVector.TRACE);
        }

        if (Psw.CMOD == Mode.KERNEL)
        {
            if(Signals.CpuBusDriver == Register.SP_U) Signals.CpuBusDriver = Register.SP_K;
            if(Signals.CpuBusLatcher == Register.SP_U) Signals.CpuBusLatcher = Register.SP_K;
        }
    }
    
    public void PswAction()
    {
        if (Signals.PswAction is not null)
        {
            Psw.Set(Signals.PswAction.Value.Buffer, Signals.FlagMask);
        }
    }

    private bool CheckCondition() => Signals.Condition switch
    {
        Condition.BR => true,
        
        Condition.BEQ => Psw.ZERO,
        Condition.BNE => !Psw.ZERO,
        Condition.BMI => Psw.NEGATIVE,
        Condition.BPL => !Psw.NEGATIVE,
        Condition.BCS => Psw.CARRY,
        Condition.BCC => !Psw.CARRY,
        Condition.BVS => Psw.OVERFLOW,
        Condition.BVC => !Psw.OVERFLOW,

        Condition.BGE => Psw.NEGATIVE == Psw.OVERFLOW,
        Condition.BLT => Psw.NEGATIVE != Psw.OVERFLOW,
        Condition.BLE => Psw.ZERO || Psw.NEGATIVE != Psw.OVERFLOW,
        Condition.BGT => !Psw.ZERO && Psw.NEGATIVE == Psw.OVERFLOW,
        Condition.BLOS => Psw.CARRY || Psw.ZERO,
        Condition.BHI => !Psw.CARRY && !Psw.ZERO,
        
        Condition.SOB => !Psw.ZERO_LATCH,
        
        _ => throw new Exception("INVALID CONDITION!!")
    };
}
