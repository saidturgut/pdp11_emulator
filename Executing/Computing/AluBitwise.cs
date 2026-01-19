namespace pdp11_emulator.Executing.Computing;

public partial class AluRom
{
    private static AluOutput ASR(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A >> 1) | (input.A & 0x8000)) };

        if ((input.A & 0x0001) != 0)
            output.Flags |= (ushort)AluFlag.Carry;

        return output;
    }
    private static AluOutput ASL(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)(input.A << 1) };
        
        if ((input.A & 0x8000) != 0)
            output.Flags |= (ushort)AluFlag.Carry;

        if ((output.Result & 0x8000) != 0 ^ (input.A & 0x8000) != 0)
            output.Flags |= (ushort)AluFlag.Overflow;

        return output;        
    }

    private static AluOutput ROR(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A >> 1) | (input.C ? 0x8000 : 0)) };

        if ((input.A & 0x0001) != 0)
            output.Flags |= (ushort)AluFlag.Carry;

        return output;
    }
    private static AluOutput ROL(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A << 1) | (input.C ? 1 : 0)) };

        if ((input.A & 0x8000) != 0)
            output.Flags |= (ushort)AluFlag.Carry;

        return output;
    }

    private static AluOutput SWAB(AluInput input)
    {
        AluOutput output = new()
            { Result = (ushort)((input.A >> 8) | (input.A << 8)) };

        return output;
    }
}