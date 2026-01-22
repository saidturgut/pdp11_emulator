namespace pdp11_emulator;
using Signaling;

public class UniBus
{
    private ushort address;
    private ushort data;
    
    public readonly Request?[] requesters = new Request?[5];

    public bool respondPermit;
    public UniBusDriving operation;

    public void Clear()
    {
        respondPermit = false;
    }
    
    public void Request(Request request)
    {
        requesters[request.Requester] = request;
    }

    public void Arbitrate()
    {
        for (int i = 0; i < requesters.Length; i++)
        {
            if (requesters[i] != null)
            {
                var requester = requesters[i]!.Value;
                address = requester.Address;
                data = requester.Data;
                operation = requester.Operation;
                
                requesters[i] = null;
                respondPermit = true;
                
                return;
            }
        }
    }

    public ushort GetAddress()
        => address;

    public void SetData(ushort input)
        => data = input;

    public ushort GetData() 
        => data;
}

public struct Request
{
    public byte Requester;
    public ushort Address;
    public ushort Data;
    public UniBusDriving Operation;
}

public enum Requester
{
    NONE,
    CPU, 
}
