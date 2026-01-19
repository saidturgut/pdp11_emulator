namespace pdp11_emulator.Executing.Computing;

public partial class AluRom
{
    private static AluOutput NONE(AluInput input) => new();
    private static AluOutput PASS(AluInput input) => new() 
        { Result = input.B };

    private static AluOutput ADD(AluInput input)
    {
        AluOutput output = new();
        var sum = (uint)(input.A + input.B);
        output.Result = (ushort)(sum & (input.ByteMode ? 0xFFu : 0xFFFFu));
        
        var carryCondition = input.ByteMode ? 0x100u : 0x10000u;
        var overflowCondition = input.ByteMode ? 0x80u : 0x8000u;
        
        if ((sum & carryCondition) != 0)
            output.Flags |= (ushort)AluFlag.Carry;

        if ((~(input.A ^ input.B) & (input.A ^ output.Result) & overflowCondition) != 0)
            output.Flags |= (ushort)AluFlag.Overflow;
        
        return output;
    }
    private static AluOutput SUB(AluInput input)
    {
        AluOutput output = new();

        var sum = (uint)(input.A - input.B);
        output.Result = (ushort)(sum & (input.ByteMode ? 0xFFu : 0xFFFFu));

        var carryBit = input.ByteMode ? 0x100u : 0x10000u;
        var overflowCondition = input.ByteMode ? 0x80u : 0x8000u;

        if ((sum & carryBit) == 0)
            output.Flags |= (ushort)AluFlag.Carry;

        if (((input.A ^ input.B) & (input.A ^ output.Result) & overflowCondition) != 0)
            output.Flags |= (ushort)AluFlag.Overflow;

        return output;
    }
    
    private static AluOutput AND(AluInput input) => new() 
        { Result = (ushort)(input.A & input.B) };
    private static AluOutput NAND(AluInput input) => new() 
        { Result = (ushort)(input.A & ~input.B) };
    private static AluOutput OR(AluInput input) => new() 
        { Result = (ushort)(input.A | input.B) };
}