namespace pdp11_emulator;
using Core.Executing.Components;

public class Pdp11
{
    private readonly TriStateBus UniBus = new ();
    
    // MASTERS
    private readonly Cpu Cpu = new ();
    
    // SLAVES
    private readonly Ram Ram = new ();
    
    private bool HALT = false;
    
    public void Power() => Clock();

    private void Clock()
    {
        Cpu.Init();
        
        while (!HALT)
        {
            Tick();
        }
    }
    
    private void Tick()
    {
        Cpu.Tick(UniBus);
    }
}