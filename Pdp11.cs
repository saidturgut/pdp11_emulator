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

    private int debugger;
    
    public void Power() => Clock();

    private void Clock()
    {
        Rom.Boot(Ram);
        
        Kd11.Init();
        
        while (!HALT)
        {
            debugger++;
            
            Tick();
            
            Thread.Sleep(25);
        }
    }
    
    private void Tick()
    {
        UniBus.Clear();
        
        // TERMINAL REQUESTS INTERRUPT
        // DISK REQUESTS INTERRUPT

        if (debugger == 20)
        {
            UniBus.RequestInterrupt(new InterruptRequest()
            {
                Vector = TrapVector.IOT,
                Priority = 2,
            });
            
            UniBus.RequestInterrupt(new InterruptRequest()
            {
                Vector = TrapVector.BUS_ERROR,
                Priority = 6,
            });
        }

        // REQUESTERS
        Kd11.Tick(UniBus, TrapUnit);
        
        UniBus.ArbitrateData();

        // RESPONDERS
        Ram.Respond(UniBus, TrapUnit);
        
        HALT = Kd11.HALT;
    }
}