namespace pdp1120;
using Arbitrating;
using Signaling;

public class Pdp1140
{
    private readonly UniBus UniBus = new ();
    private readonly TrapUnit TrapUnit = new();

    private readonly Kd11a Kd11a = new ();
    
    private readonly Rom Rom = new ();
    private readonly Ram Ram = new ();
    
    private bool HALT;
    
    public void Power() => Clock();

    private void Clock()
    {
        Rom.Boot(Ram);
        
        Kd11a.Init();
        
        while (!HALT)
        {
            Tick();
            
            Thread.Sleep(100);
        }
    }
    
    private void Tick()
    {
        UniBus.Clear();
        
        // INTERRUPT

        // REQUESTERS
        Kd11a.Tick(UniBus, TrapUnit);
        
        UniBus.ArbitrateData(TrapUnit);

        // RESPONDERS
        
        Ram.Respond(UniBus);
        
        HALT = Kd11a.HALT;
        if (Kd11a.COMMIT) Commit();
        TrapUnit.Clear();
    }
    
    private void Commit()
    {
        Ram.Commit(TrapUnit.ABORT);

        Console.WriteLine("\n-->> ON BOUNDARY <<--");
        
        Kd11a.COMMIT = false;
    }
}