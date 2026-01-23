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
        
        new (), // PSW * 13 
        new (), // VEC * 14 
        
        new (), // SP_K * 15 
    ];
    
    private SignalSet Signals = new ();

    public bool STALL { get; private set; }
    
    public void Clear(TriStateBus cpuBus, TriStateBus aluBus)
    {
        cpuBus.Clear();
        aluBus.Clear();
    }

    public void Receive(SignalSet input) 
        => Signals = input;

    public void ControlWord(TrapUnit trapUnit, bool START)
    {        
        Cw.Update(Access(Register.PSW).Get());

        if (START)
        {
            if (trapUnit.TRAP)
            {            
                PswSet(0, PswFlag.CMOD1 | PswFlag.CMOD2); // ENTER KERNEL MODE ON TRAPS
            }
        
            PswSet((ushort)(Cw.CMOD == Mode.KERNEL ? 0 : 0xFFFF), PswFlag.PMOD1 | PswFlag.PMOD2);
            
            Cw.Update(Access(Register.PSW).Get());
        }
        
        if (Cw.TRACE) trapUnit.Request(TrapVector.TRACE);

        if (Cw.CMOD == Mode.KERNEL)
        {
            if(Signals.CpuBusDriver == Register.SP_U) Signals.CpuBusDriver = Register.SP_K;
            if(Signals.CpuBusLatcher == Register.SP_U) Signals.CpuBusLatcher = Register.SP_K;
        }
    }

    private RegisterObject Access(Register register) 
        => Registers[(ushort)register];
    
    public ushort GetIr() 
        => Registers[(ushort)Register.IR].Get();
    
    public void Commit(TrapUnit trapUnit)
    {
        trapUnit.Arbitrate();

        Access(Register.VEC).Set(trapUnit.VECTOR);
        
        foreach (RegisterObject register in Registers)
        {
            if (!trapUnit.ABORT)
            {
                register.Commit();
            }
            
            register.Init();
        }
    }
}