namespace pdp1120.Arbitrating;
using Signaling;

public struct DataRequest
{
    public byte Requester;
    public ushort Address;
    public ushort Data;
    public UniBusDriving Operation;
}

public struct InterruptRequest
{
    public TrapVector Vector;
    public byte Priority;
}

public enum Requester
{
    NONE,
    CPU, 
}
