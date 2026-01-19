namespace pdp11_emulator.Executing.Computing;

public partial class AluRom
{
    private static AluOutput ZERO(AluInput input) => new()
        { Result = 0 };

    private static AluOutput NOT(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)(~input.A & 0xFFFF) };

        return output;
    }

    private static AluOutput INC(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A + 1) & 0xFFFF) };

        if (input.A == 0x8000 - 1)
            output.Flags |= (ushort)AluFlag.Overflow;
        
        return output;
    }
    private static AluOutput DEC(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A - 1) & 0xFFFF) };

        if (input.A == 0x8000)
            output.Flags |= (ushort)AluFlag.Overflow;
        
        return output;
    }
    private static AluOutput NEG(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((0 - input.A) & 0xFFFF) };

        if ((input.A & 0xFFFF) != 0)
            output.Flags |= (ushort)AluFlag.Carry;

        if ((input.A & 0xFFFF) == 0x8000)
            output.Flags |= (ushort)AluFlag.Overflow;
        
        return output;
    }

    private static AluOutput ADC(AluInput input)
    {
        AluOutput output = input.C ? INC(input) : PASS(input);
        
        if (input is { C: true, A: 0xFFFF })
            output.Flags |= (ushort)AluFlag.Carry;
        
        return output;
    }
    private static AluOutput SBC(AluInput input)
    {
        AluOutput output = input.C ? DEC(input) : PASS(input);
        
        if (!input.C || input.A != 0x0000)
            output.Flags |= (ushort)AluFlag.Carry;
        
        return output;
    }
}