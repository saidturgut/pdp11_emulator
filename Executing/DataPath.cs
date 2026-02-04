namespace pdp1120.Executing;
using Signaling.Cycles;
using Signaling;
using Components;

public partial class DataPath
{
    private readonly ClockedRegister[] Registers =
    [
        new (), // R0 * 0
        new (), // R1 * 1
        new (), // R2 * 2
        new (), // R3 * 3
        new (), // R4 * 4
        new (), // R5 * 5
        new (), // SP_U * 6
        new (), // PC * 7
        
        new (), // MDR * 8
        new (), // IR * 9
        new (), // MAR * 10
        
        new (), // TMP * 11
        new (), // DST * 12
        
        new (), // VEC * 13
        
        new (), // SP_K * 14 
        
        new (), // PSW * 15
    ];
    
    private SignalSet Signals = new ();
    
    public void Init()
    {
        Psw.PswRegister = Access(Register.PSW);

        DebugInit();
    }
    
    public void Clear(TriStateBus cpuBus, TriStateBus aluBus)
    {
        cpuBus.Clear();
        aluBus.Clear();
    }

    public void Receive(MicroUnit microUnit, TrapUnit trapUnit) 
        => Signals = microUnit.Emit(Access(Register.IR).Get(), trapUnit, Psw.CMOD);
    
    private ClockedRegister Access(Register register) 
        => Registers[(ushort)register];
    
    public byte GetPriority() 
        => Psw.PRIORITY;
    
    public void Commit(TrapUnit trapUnit)
    {
        trapUnit.Arbitrate();

        Access(Register.VEC).Set(trapUnit.VECTOR);

        foreach (ClockedRegister register in Registers) 
            register.Commit(trapUnit.ABORT);
        
        if (SUPPRESSED != 0) SUPPRESSED--;
    }
}