namespace pdp11_emulator.Executing.Computing;
using Components;
using Signaling;

public class Alu : AluRom
{
    public AluOutput Compute(AluInput input)
    {
        SetMasks(input.ByteMode);
        
        AluOutput output = Operations[(ushort)input.Operation](input);
        
        // COMPUTE N AND Z FLAGS
        ushort result = (ushort)(!maskApplied 
            ? output.Result : output.Result & 0xFF);
        if((result & x8000) != 0) 
            output.Flags |= (ushort)PswFlag.NEGATIVE;
        if(result == 0)
            output.Flags |= (ushort)PswFlag.ZERO;

        if (input.ByteMode)
            output.Result &= 0xFF;

        return output;
    }
}
