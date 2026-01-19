namespace pdp11_emulator.Executing.Components;

public class Register()
{
    private ushort value;
    
    public void Set(ushort input) 
        => value = input;

    public ushort Get() 
        => value;
}