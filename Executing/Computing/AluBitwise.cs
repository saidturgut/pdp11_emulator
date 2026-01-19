namespace pdp11_emulator.Executing.Computing;

public partial class AluRom
{
    private static AluOutput ASR(AluInput input) => new();
    private static AluOutput ASL(AluInput input) => new();
    
    private static AluOutput ROR(AluInput input) => new();
    private static AluOutput ROL(AluInput input) => new();
    
    private static AluOutput SWAB(AluInput input) => new();
}