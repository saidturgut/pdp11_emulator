namespace pdp11_emulator.Executing.Computing;
using Signaling;

public class Alu : AluRom
{
    public AluOutput Compute(AluInput input)
    {
        AluOutput output = Operations[(ushort)input.Operation](input);
        
        output.Result = !input.ByteMode
            ? output.Result : (byte)(output.Result & 0xFF);
        
        if((output.Result & (!input.ByteMode ? 0x8000 : 0x80)) != 0) 
            output.Flags |= (ushort)AluFlag.Negative;
        if(output.Result == 0) 
            output.Flags |= (ushort)AluFlag.Zero;

        return output;
    }
}

public struct AluInput
{
    public AluOperation Operation;
    public ushort A;
    public ushort B;
    public bool C;
    public bool ByteMode;
}

public struct AluOutput
{
    public ushort Result;
    public ushort Flags;
}

