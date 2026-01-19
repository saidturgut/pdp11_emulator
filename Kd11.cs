namespace pdp11_emulator;
using Executing.Components;
using Executing;
using Signaling;

public class Kd11
{
    private readonly TriStateBus CpuBus = new ();
    private readonly TriStateBus AluBus = new ();
    
    private readonly DataPath DataPath = new();
    private readonly ControlUnit ControlUnit = new();
    
    public bool HALT;
    
    public void Init()
    {
        DataPath.Init();
    }
    
    public void Tick(UniBus uniBus)
    {
        DataPath.Clear(CpuBus, AluBus);
        DataPath.Receive(
        ControlUnit.Emit(DataPath.GetIr()));
        
        DataPath.UniBusLatch(uniBus);
        if(DataPath.STALL) return;
        DataPath.CpuBusDrive(CpuBus);
        DataPath.AluAction(CpuBus, AluBus);
        DataPath.CpuBusLatch(CpuBus, AluBus);
        DataPath.UniBusDrive(uniBus);

        DataPath.Debug();
        
        ControlUnit.Advance();

        HALT = ControlUnit.HALT;
    }
}