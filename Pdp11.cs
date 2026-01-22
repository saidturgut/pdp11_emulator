namespace pdp11_emulator;
using Executing.Components;
using Arbitrating;
using Signaling;

public class Pdp11
{
    private readonly UniBus UniBus = new ();
    private readonly TrapUnit TrapUnit = new();

    // REQUESTERS
    private readonly Kd11 Kd11 = new ();
    
    // RESPONDERS
    private readonly Rom Rom = new ();
    private readonly Ram Ram = new ();
    
    private bool HALT;
    
    public void Power() => Clock();

    private void Clock()
    {
        Rom.Boot(Ram);
        
        Kd11.Init();
        
        while (!HALT)
        {
            Tick();
            
            Thread.Sleep(100);
        }
    }
    
    private void Tick()
    {
        UniBus.Clear();
        
        // TERMINAL REQUESTS INTERRUPT
        // DISK REQUESTS INTERRUPT
        
        // REQUESTERS
        Kd11.Tick(UniBus, TrapUnit);
        
        UniBus.ArbitrateData();

        // RESPONDERS
        Ram.Respond(UniBus, TrapUnit);
        
        HALT = Kd11.HALT;
    }
}