namespace pdp11_emulator.Executing.Components;

public class RegisterObject
{
    private ushort committed;
    private ushort value;

    public void Debug(ushort input)
    {
        value = input;
        committed = value;
    }

    public void Init()
        => value = committed;
    
    public void Set(ushort input)
        => value = input;

    public ushort Get() 
        => value;
    
    public void Commit()
    {
        committed = value;
    }
}