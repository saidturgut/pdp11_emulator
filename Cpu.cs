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
    
    public void Init()
    {
        DataPath.Init();
    }
    
    public void Tick(TriStateBus uniBus)
    {
        // UNIBUS_LATCH
        // CPUBUS_DRIVE
        // ALU_COMPUTE
        // CPUBUS_LATCH
        // UNIBUS_DRIVE
    }
}