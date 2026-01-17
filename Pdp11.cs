namespace pdp11_emulator;
using Core.Executing.Components;

public class Pdp11
{
    private readonly UniBus UniBus = new ();
    
    // REQUESTERS
    private readonly Cpu Cpu = new ();
    
    // RESPONDERS
    private readonly Rom Rom = new ();
    private readonly Ram Ram = new ();
    
    private bool HALT;
    
    public void Power() => Clock();

    private void Clock()
    {
        Rom.Boot(Ram);
        
        Cpu.Init();
        
        while (!HALT)
        {
            Tick();
            
            Thread.Sleep(50);
        }
    }
    
    private void Tick()
    {
        UniBus.Clear();
        
        // REQUESTERS
        Cpu.Tick(UniBus);
        
        UniBus.Arbitrate();

        // RESPONDERS
        Ram.Respond(UniBus);

        HALT = Cpu.HALT;
    }
}