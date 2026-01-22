namespace pdp11_emulator.Executing.Computing;
using Components;

public partial class AluRom
{
    private static AluOutput ZERO(AluInput input) => new()
        { Result = 0 };

    private static AluOutput COM(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)(~input.A & xFFFF) };

        return output;
    }

    private static AluOutput INC(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A + 1) & xFFFF) };

        if (input.A == x8000 - 1)
            output.Flags |= (ushort)PswFlag.Overflow;
        
        return output;
    }
    private static AluOutput DEC(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A - 1) & xFFFF) };

        if (input.A == x8000)
            output.Flags |= (ushort)PswFlag.Overflow;
        
        return output;
    }
    private static AluOutput NEG(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)(-input.A & xFFFF) };

        if ((input.A & xFFFF) != 0)
            output.Flags |= (ushort)PswFlag.Carry;

        if ((input.A & xFFFF) == x8000)
            output.Flags |= (ushort)PswFlag.Overflow;
        
        return output;
    }

    private static AluOutput ADC(AluInput input)
    {
        AluOutput output = input.Cw.Carry ? INC(input) : PASS(input);
        
        if(input.Cw.Carry && input.A == 0)
            output.Flags |= (ushort)PswFlag.Carry;
        
        return output;
    }
    private static AluOutput SBC(AluInput input)
    {
        AluOutput output = input.Cw.Carry ? DEC(input) : PASS(input);
        
        if (!input.Cw.Carry || input.A != 0)
            output.Flags |= (ushort)PswFlag.Carry;
        
        return output;
    }

    private static AluOutput BRANCH(AluInput input) => new()
        { Result = (ushort)(input.A + (((sbyte)input.B) << 1)) };
}