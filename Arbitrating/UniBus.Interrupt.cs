namespace pdp1120.Arbitrating;
using Signaling;

public partial class UniBus
{
    private readonly InterruptRequest?[] InterruptRequests = new InterruptRequest?[7];
    
    public void RequestInterrupt(InterruptRequest request)
    {
        InterruptRequests[request.Priority] = request;
    }
    
    public void ArbitrateInterrupt(TrapUnit trapUnit, byte priorityLevel)
    {
        for (byte i = 6; i > 0; i--)
        {
            if (InterruptRequests[i] != null)
            {
                var request = InterruptRequests[i]!.Value;

                if (request.Priority > priorityLevel)
                {
                    InterruptRequests[i] = null; 
                }
                else
                {
                    trapUnit.Request(request.Vector);
                    
                    InterruptRequests[i] = null;
                    return;
                }
            }
        }
    }
}