namespace pdp11_emulator.Arbitrating;
using Signaling;

public partial class UniBus
{    
    public readonly DataRequest?[] DataRequests = new DataRequest?[5];
    private readonly InterruptRequest?[] InterruptRequests = new InterruptRequest?[7];

    public UniBusDriving Operation {get; private set;}
    
    private ushort address;
    private ushort data;
    
    public bool respondPermit { get; private set; }

    public void Clear()
    {
        respondPermit = false;
    }
    
    public ushort GetAddress()
        => address;

    public void SetData(ushort input)
        => data = input;

    public ushort GetData() 
        => data;
}