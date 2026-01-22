namespace pdp11_emulator.Executing;
using Signaling.Cycles;
using Signaling;
using Components;
using Arbitrating;

public partial class DataPath
{
    private const Requester requesterType = Requester.CPU;
    
    public void UniBusLatch(UniBus uniBus)
    {
        if(!signals.UniBusLatching)
            return;
        
        if (uniBus.DataRequests[(ushort)requesterType] != null)
        {
            STALL = true;
            return;
        }

        STALL = false;
        
        Access(Register.MDR).Set(uniBus.GetData());
    }
    
    public void UniBusDrive(UniBus uniBus)
    {
        if(signals.UniBusDriving is UniBusDriving.NONE)
            return;
        
        uniBus.RequestData(new DataRequest
        {
            Requester = (byte)requesterType,
            Address = Access(Register.MAR).Get(),
            Data = Access(Register.TMP).Get(),
            Operation = signals.UniBusDriving,
        });
    }
}