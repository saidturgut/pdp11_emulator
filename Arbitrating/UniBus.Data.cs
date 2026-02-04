namespace pdp1120.Arbitrating;
using Signaling;

public partial class UniBus
{
    public readonly DataRequest?[] DataRequests = new DataRequest?[5];

    public void RequestData(DataRequest request)
    {
        DataRequests[request.Requester] = request;
    }
    public void ArbitrateData(TrapUnit trapUnit)
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
                
                ValidateRequest(trapUnit);
                return;
            }
        }
    }

    private void ValidateRequest(TrapUnit trapUnit)
    {
        BYTE_MODE = Operation is UniBusDriving.READ_BYTE or UniBusDriving.WRITE_BYTE;
        
        if (address % 2 != 0 && !BYTE_MODE)
        {
            trapUnit.Request(TrapVector.ODD_ADDRESS);
            return;
        }
        
        RESPOND_PERMIT = true;
    }
}