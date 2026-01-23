namespace pdp11_emulator.Executing.Components;

public class RegisterObject
{
    protected ushort committed;
    protected ushort value;

    public void Init()
        => value = committed;
    
    public void Debug(ushort input)
    {
        value = input;
        committed = value;
    }
    
    public virtual void Set(ushort input)
        => value = input;

    public ushort Get() 
        => value;
    
    public void Commit()
    {
        committed = value;
    }
}