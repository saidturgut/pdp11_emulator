namespace pdp1120.Executing;
using Components;
using Signaling;
using Arbitrating;

public partial class DataPath
{
    private const Requester requesterType = Requester.CPU;
    
    public bool STALL { get; private set; }

    public void UniBusLatch(UniBus uniBus)
    {
        if(!Signals.UniBusLatching)
            return;
        
        if (uniBus.DataRequests[(ushort)requesterType] != null)
        {
            STALL = true;
            return;
        }

        STALL = false;
        
        Access(Register.MDR).Set(uniBus.GetData());
    }
    
    public void UniBusDrive(UniBus uniBus, TrapUnit trapUnit)
    {
        if(Signals.UniBusDriving is UniBusDriving.NONE)
            return;
        
        uniBus.RequestData(new DataRequest
        {
            Requester = (byte)requesterType,
            
            Address = Access(Register.MAR).Get(),
           
            Data = Access(Register.TMP).Get(),
            Operation = Signals.UniBusDriving,
        });
    }
}