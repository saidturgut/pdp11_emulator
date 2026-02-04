namespace pdp1120.Executing.Components;

public class ClockedRegister
{
    private ushort committed;
    private ushort value;
    
    public void Init(ushort input)
    {
        value = input;
        committed = value;
    }
    
    public void Set(ushort input)
        => value = input;

    public ushort Get() 
        => value;
    
    public void Commit(bool abort)
    {
        if (!abort)
        {
            committed = value;
        }
        
        value = committed;
    }
}

public class MemoryRegister
{
    private ushort value;
    
    public void Set(ushort input)
        => value = input;

    public ushort Get() 
        => value;
}