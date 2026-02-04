namespace pdp1120.Arbitrating;
using Signaling;

public partial class UniBus
{    
    public UniBusDriving Operation {get; private set;}
    
    private ushort address;
    private ushort data;
    
    public bool RESPOND_PERMIT { get; private set; }
    public bool BYTE_MODE { get; private set; }

    public void Clear()
    {
        RESPOND_PERMIT = false;
        BYTE_MODE = false;
        data = 0;
    }
    
    public ushort GetAddress()
        => address;

    public void SetData(ushort input)
        => data = input;

    public ushort GetData() 
        => data;
}