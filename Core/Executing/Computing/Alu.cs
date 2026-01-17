namespace pdp11_emulator.Core.Executing.Computing;
using Signaling;

public class Alu
{
    public AluOutput Compute(AluInput input)
    {
        AluOutput output = new AluOutput();

        switch (input.Operation)
        {
            case AluOperation.ADD:
            {
                uint sum = (uint)(input.A + input.B);
                output.Result = (ushort)sum;
                break;
            }
            default:
                throw new Exception("OPERATION NOT IMPLEMENTED YET!!");
        }
        
        return output;
    }
}

public struct AluInput
{
    public AluOperation Operation;
    public ushort A;
    public ushort B;
}

public struct AluOutput
{
    public ushort Result;
    public byte Flags;
}

