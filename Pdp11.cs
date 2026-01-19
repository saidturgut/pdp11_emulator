namespace pdp11_emulator;
using Executing.Components;

public class Pdp11
{
    private readonly UniBus UniBus = new ();
    
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
            
            Thread.Sleep(50);
        }
    }
    
    private void Tick()
    {
        UniBus.Clear();
        
        // REQUESTERS
        Kd11.Tick(UniBus);
        
        UniBus.Arbitrate();

        // RESPONDERS
        Ram.Respond(UniBus);

        HALT = Kd11.HALT;
    }
}