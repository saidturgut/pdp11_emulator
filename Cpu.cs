namespace pdp11_emulator;
using Core.Executing.Components;
using Core.Executing;
using Core.Signaling;

public class Cpu
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
    
    public void Tick(UniBus uniBus)
    {
        DataPath.Clear(CpuBus, AluBus);
        DataPath.Receive(
        MicroUnit.Emit(DataPath.GetIr()));
        
        DataPath.UniBusLatch(uniBus);
        if(DataPath.STALL) return;
        DataPath.CpuBusDrive(CpuBus);
        DataPath.AluAction(CpuBus, AluBus);
        DataPath.CpuBusLatch(CpuBus, AluBus);
        DataPath.UniBusDrive(uniBus);

        DataPath.Debug();
        
        MicroUnit.Advance();

        HALT = MicroUnit.HALT;
    }
}