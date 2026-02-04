namespace pdp1120;
using Executing.Components;
using Executing;
using Signaling;
using Arbitrating;

public class Kd11a
{
    private readonly TriStateBus CpuBus = new ();
    private readonly TriStateBus AluBus = new ();
    
    private readonly DataPath DataPath = new();
    private readonly MicroUnit MicroUnit = new();
    
    public bool HALT { get; private set; }
    public bool COMMIT;
    
    public void Init()
    {
        DataPath.Init();
    }
    
    public void Tick(UniBus uniBus, TrapUnit trapUnit)
    {
        DataPath.Clear(CpuBus, AluBus);
        DataPath.Receive(MicroUnit, trapUnit);
        DataPath.StatusWord(MicroUnit.START(), trapUnit);
        
        DataPath.UniBusLatch(uniBus);
        if(DataPath.STALL) return;
        DataPath.CpuBusDrive(CpuBus);
        DataPath.AluAction(CpuBus, AluBus);
        DataPath.PswAction();
        DataPath.CpuBusLatch(CpuBus, AluBus);
        DataPath.UniBusDrive(uniBus, trapUnit);

        DataPath.Debug();
        
        MicroUnit.Advance(trapUnit);

        HALT = MicroUnit.HALT;

        if (MicroUnit.BOUNDARY) Boundary(uniBus, trapUnit);
    }

    private void Boundary(UniBus uniBus, TrapUnit trapUnit)
    {
        uniBus.ArbitrateInterrupt(trapUnit, DataPath.GetPriority());
        DataPath.Commit(trapUnit);
        MicroUnit.Clear(trapUnit);
        COMMIT = true;
    }
}