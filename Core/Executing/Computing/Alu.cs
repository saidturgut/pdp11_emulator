namespace pdp11_emulator.Core.Executing.Computing;
using Signaling;

public class Alu
{
    public AluOutput Compute(AluInput input)
    {
        AluOutput output = new AluOutput();
        
        switch (input.Operation)
        {
            case AluOperation.MOV:
            {
                output.Result = input.B;
                break;
            }
            case AluOperation.ADD:
            {
                uint sum = (uint)(input.A + input.B);
                output.Result = (ushort)sum;
                
                if ((sum & 0x10000) != 0) 
                    output.Flags |= (ushort)AluFlag.Carry;
                if ((~(input.A ^ input.B) & (input.A ^ output.Result) & 0x8000) != 0) 
                    output.Flags |= (ushort)AluFlag.Overflow;
                break;
            }
            case AluOperation.SUB:
            {
                uint sum = (uint)(input.A - input.B);
                output.Result = (ushort)sum;
                
                if (input.A >= input.B)
                    output.Flags |= (ushort)AluFlag.Carry;
                if (((input.A ^ input.B) & (input.A ^ output.Result) & 0x8000) != 0)
                    output.Flags |= (ushort)AluFlag.Overflow;
                break;
            }
            case AluOperation.AND:
            {
                output.Result = (ushort)(input.A & input.B); 
                break;
            }
            case AluOperation.NAND:
            {
                output.Result = (ushort)(input.A & ~input.B); 
                break;
            }
            case AluOperation.OR:
            {
                output.Result = (ushort)(input.A | input.B); 
                break;
            }
            default:
                throw new Exception("OPERATION NOT IMPLEMENTED YET!!");
        }
        
        if((output.Result & 0x8000) != 0) 
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
}

public struct AluOutput
{
    public ushort Result;
    public ushort Flags;
}

