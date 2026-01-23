namespace pdp11_emulator;
using Executing.Components;
using Executing;
using Signaling;
using Arbitrating;

public class Kd11
{
    private readonly TriStateBus CpuBus = new ();
    private readonly TriStateBus AluBus = new ();
    
    private readonly DataPath DataPath = new();
    private readonly MicroUnit MicroUnit = new();
    
    public bool HALT { get; private set; }
    public bool START { get; private set; }
    
    public void Init()
    {
        DataPath.Init();
    }
    
    public void Tick(UniBus uniBus, TrapUnit trapUnit)
    {
        DataPath.Clear(CpuBus, AluBus);
        DataPath.Receive(
        MicroUnit.Emit(DataPath.GetIr(), trapUnit));
        DataPath.ControlWord(trapUnit, MicroUnit.START());
        
        DataPath.UniBusLatch(uniBus);
        if(DataPath.STALL) return;
        DataPath.CpuBusDrive(CpuBus);
        DataPath.AluAction(CpuBus, AluBus);
        DataPath.PswAction();
        DataPath.CpuBusLatch(CpuBus, AluBus);
        DataPath.UniBusDrive(uniBus);

        DataPath.Debug();
        
        MicroUnit.Advance(trapUnit);

        HALT = MicroUnit.HALT;

        if (MicroUnit.BOUNDARY) Boundary(uniBus, trapUnit);
    }

    private void Boundary(UniBus uniBus, TrapUnit trapUnit)
    {
        uniBus.ArbitrateInterrupt(trapUnit, DataPath.GetCw());
        DataPath.Commit(trapUnit);
        MicroUnit.Clear(trapUnit);
    }
}