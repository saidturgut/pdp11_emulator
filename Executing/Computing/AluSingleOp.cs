namespace pdp11_emulator.Executing.Computing;

public partial class AluRom
{
    private static AluOutput ZERO(AluInput input) => new() 
        { Result = 0 };

    private static AluOutput NOT(AluInput input)
    {
        AluOutput output = new();
        
        var mask = input.ByteMode ? (ushort)0x00FF : (ushort)0xFFFF;
        output.Result = (ushort)(~input.B & mask);
        output.Flags = (ushort)AluFlag.Carry;
        
        return output;
    }

    private static AluOutput INC(AluInput input)
    {
        AluOutput output = new();
        
        var mask = input.ByteMode ? 0xFFu : 0xFFFFu;
        var signBit = input.ByteMode ? 0x80u : 0x8000u;

        var sum = (uint)(input.B + 1);
        var result = sum & mask;

        output.Result = (ushort)result;

        if (input.B == (signBit - 1))
            output.Flags |= (ushort)AluFlag.Overflow;
        
        return output;
    }
    private static AluOutput DEC(AluInput input)
    {
        AluOutput output = new();
        
        var mask = input.ByteMode ? 0xFFu : 0xFFFFu;
        var signBit = input.ByteMode ? 0x80u : 0x8000u;

        var sum = (uint)(input.B - 1);
        var result = sum & mask;

        output.Result = (ushort)result;

        if (input.B == signBit)
            output.Flags |= (ushort)AluFlag.Overflow;
        
        return output;
    }
    private static AluOutput NEG(AluInput input)
    {
        AluOutput output = new();
            
        var mask = input.ByteMode ? 0xFFu : 0xFFFFu;
        var signBit = input.ByteMode ? 0x80u : 0x8000u;

        var result = ((uint)(0 - input.B)) & mask;
        output.Result = (ushort)result;

        if ((input.B & mask) != 0)
            output.Flags |= (ushort)AluFlag.Carry;

        if ((input.B & mask) == signBit)
            output.Flags |= (ushort)AluFlag.Overflow;
        
        return output;
    }

    private static AluOutput ADC(AluInput input)
    {
        AluOutput output = input.C ? INC(input) : PASS(input);
        return output;
    }
    private static AluOutput SBC(AluInput input)
    {
        AluOutput output = input.C ? DEC(input) : PASS(input);
        
        if(!input.C) output.Flags |= (ushort)AluFlag.Carry;

        return output;
    }
}