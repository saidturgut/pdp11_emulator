namespace pdp1120.Executing.Computing;
using Components;

public partial class AluRom
{
    private static AluOutput MUL(AluInput input) => new();
    private static AluOutput DIV(AluInput input) => new();
    private static AluOutput ASH(AluInput input) => new();
    private static AluOutput ASHC(AluInput input) => new();
    
    private static AluOutput XOR(AluInput input) => new()
        { Result = (ushort)(input.A ^ input.B) };
}