namespace pdp11_emulator.Arbitrating;
using Signaling;

public partial class UniBus
{
    public void RequestData(DataRequest request)
    {
        DataRequests[request.Requester] = request;
    }
    public void ArbitrateData()
    {
        for (byte i = 0; i < DataRequests.Length; i++)
        {
            if (DataRequests[i] != null)
            {
                var request = DataRequests[i]!.Value;
                Operation = request.Operation;
                address = request.Address;
                data = request.Data;
                
                DataRequests[i] = null;
                respondPermit = true;
                return;
            }
        }
    }
    
    public void RequestInterrupt(InterruptRequest request)
    {
        InterruptRequests[request.Priority] = request;
    }
    public void ArbitrateInterrupt(TrapUnit trapUnit, byte priorityLevel)
    {
        for (byte i = 0; i < InterruptRequests.Length; i++)
        {
            if (InterruptRequests[i] != null)
            {
                var requester = InterruptRequests[i]!.Value;

                if (requester.Priority > priorityLevel)
                {
                    InterruptRequests[i] = null; 
                }
                else
                {
                    trapUnit.Request(requester.Vector);
                    
                    InterruptRequests[i] = null;
                    return;
                }
            }
        }
    }
}