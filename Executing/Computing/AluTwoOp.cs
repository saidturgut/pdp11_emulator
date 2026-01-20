namespace pdp11_emulator.Executing.Computing;

public partial class AluRom
{
    private static AluOutput NONE(AluInput input) => new();
    private static AluOutput PASS(AluInput input) => new() 
        { Result = input.A };

    private static AluOutput ADD(AluInput input)
    {
        AluOutput output = new();
        var sum = (uint)(input.A + input.B);
        output.Result = (ushort)(sum & xFFFF);
        
        if ((sum & x10000) != 0)
            output.Flags |= (ushort)PswFlag.Carry;

        if ((~(input.A ^ input.B) & (input.A ^ output.Result) & x8000) != 0)
            output.Flags |= (ushort)PswFlag.Overflow;
        
        return output;
    }
    private static AluOutput SUB(AluInput input)
    {
        AluOutput output = new();
        var sum = (uint)(input.A - input.B);
        output.Result = (ushort)(sum & xFFFF);
        
        if ((sum & x10000) == 0)
            output.Flags |= (ushort)PswFlag.Carry;

        if (((input.A ^ input.B) & (input.A ^ output.Result) & x8000) != 0)
            output.Flags |= (ushort)PswFlag.Overflow;

        return output;
    }
    
    private static AluOutput BIT(AluInput input) => new() 
        { Result = (ushort)(input.A & input.B) };
    private static AluOutput BIC(AluInput input) => new() 
        { Result = (ushort)(input.A & ~input.B) };
    private static AluOutput BIS(AluInput input) => new() 
        { Result = (ushort)(input.A | input.B) };
}