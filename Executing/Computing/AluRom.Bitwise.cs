namespace pdp11_emulator.Executing.Computing;
using Components;

public partial class AluRom
{
    private static AluOutput ASR(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A >> 1) | (input.A & x8000)) };

        if ((input.A & 1) != 0)
            output.Flags |= (ushort)PswFlag.CARRY;

        return output;
    }
    private static AluOutput ASL(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)(input.A << 1) };
        
        if ((input.A & x8000) != 0)
            output.Flags |= (ushort)PswFlag.CARRY;

        if ((output.Result & x8000) != 0 ^ (input.A & x8000) != 0)
            output.Flags |= (ushort)PswFlag.OVERFLOW;

        return output;        
    }

    private static AluOutput ROR(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A >> 1) | (input.Cw.CARRY ? x8000 : 0)) };

        if ((input.A & 1) != 0)
            output.Flags |= (ushort)PswFlag.CARRY;

        return output;
    }
    private static AluOutput ROL(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A << 1) | (input.Cw.CARRY ? 1 : 0)) };

        if ((input.A & x8000) != 0)
            output.Flags |= (ushort)PswFlag.CARRY;
        
        if ((output.Result & x8000) != 0 ^ (input.A & x8000) != 0)
            output.Flags |= (ushort)PswFlag.OVERFLOW;

        return output;
    }

    private static AluOutput SWAB(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A >> 8) | (input.A << 8)) };
        
        SetMasks(true);

        return output;
    }

    private static AluOutput SXT(AluInput input) => new()
        { Result = (ushort)(input.Cw.NEGATIVE ? 0xFFFF : 0x0000), };
}