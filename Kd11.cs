namespace pdp11_emulator;
using Executing.Components;
using Executing;
using Signaling;

public class Kd11
{
    private readonly TriStateBus CpuBus = new ();
    private readonly TriStateBus AluBus = new ();
    
    private readonly DataPath DataPath = new();
    private readonly MicroUnit MicroUnit = new();
    
    public bool HALT;
    
    public void Init()
    {
        DataPath.Init();
    }
    
    public void Tick(UniBus uniBus, TrapUnit trapUnit)
    {
        DataPath.Clear(CpuBus, AluBus);
        DataPath.Receive(
        MicroUnit.Emit(DataPath.GetIr(), trapUnit));
        
        DataPath.UniBusLatch(uniBus);
        if(DataPath.STALL) return;
        DataPath.CpuBusDrive(CpuBus);
        DataPath.AluAction(CpuBus, AluBus, trapUnit);
        DataPath.PswAction();
        DataPath.CpuBusLatch(CpuBus, AluBus);
        DataPath.UniBusDrive(uniBus);

        DataPath.Debug();
        
        MicroUnit.Advance(trapUnit);

        HALT = MicroUnit.HALT;

        if (!MicroUnit.BOUNDARY) return;
        
        DataPath.SetVector(trapUnit.VECTOR);
        DataPath.Commit(trapUnit.State());
        MicroUnit.Clear(trapUnit);
    }
}