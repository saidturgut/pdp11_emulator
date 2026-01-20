namespace pdp11_emulator.Executing.Computing;
using Signaling;

public class Alu : AluRom
{
    public AluOutput Compute(AluInput input)
    {
        SetMasks(input.ByteMode);
        
        AluOutput output = Operations[(ushort)input.Operation](input);
        
        if((output.Result & x8000) != 0) 
            output.Flags |= (ushort)AluFlag.Negative;
        if(output.Result == 0) 
            output.Flags |= (ushort)AluFlag.Zero;

        if (input.ByteMode)
            output.Result &= 0xFF;

        return output;
    }
}
