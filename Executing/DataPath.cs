namespace pdp11_emulator.Executing;
using Signaling.Cycles;
using Signaling;
using Components;

public partial class DataPath
{
    private readonly RegisterObject[] Registers =
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
    ];
    
    private SignalSet Signals = new ();

    public bool STALL { get; private set; }
    private byte SUPPRESSED;
    
    public void Clear(TriStateBus cpuBus, TriStateBus aluBus)
    {
        cpuBus.Clear();
        aluBus.Clear();
    }

    public void Receive(SignalSet input) 
        => Signals = input;
    
    private RegisterObject Access(Register register) 
        => Registers[(ushort)register];
    
    public ushort GetIr() 
        => Registers[(ushort)Register.IR].Get();
    
    public byte GetPriority() 
        => Psw.PRIORITY;
    
    public void Commit(TrapUnit trapUnit)
    {
        trapUnit.Arbitrate();

        Access(Register.VEC).Set(trapUnit.VECTOR);
        
        foreach (RegisterObject register in Registers)
        {
            if (!trapUnit.ABORT) register.Commit();
            register.Init();
        }

        if (!trapUnit.ABORT) Psw.Commit();
        Psw.Init();

        if (SUPPRESSED != 0) SUPPRESSED--;
    }
}