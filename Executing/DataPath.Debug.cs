namespace pdp11_emulator.Executing;
using Signaling;
using Components;

public partial class DataPath
{
    public void Init()
    {
        Access(Register.R0).Debug(0x00FF);
        Access(Register.R1).Debug(0x0F00);
        Access(Register.R2).Debug(0x200);
        Access(Register.R3).Debug(0x300);
        Access(Register.R4).Debug(0x400);
        Access(Register.R5).Debug(0x500);
        Access(Register.SP_U).Debug(0x1000);
        Access(Register.SP_K).Debug(0x20);
        //Access(Register.R7).Debug(0x2000);
    }

    public void Debug()
    {
        ushort flags = Psw.Get();
        Console.WriteLine($"PC: {O(Access(Register.PC).Get())}");
        Console.WriteLine($"SP_K: {O(Access(Register.SP_K).Get())}");
        Console.WriteLine($"SP_U: {O(Access(Register.SP_U).Get())}");
        for (int i = 0; i < 6; i++) Console.WriteLine($"R{i}: {O(Access((Register)i).Get())}");
        Console.WriteLine($"MDR: {O(Access(Register.MDR).Get())}");
        Console.WriteLine($"IR: {O(Access(Register.IR).Get())}");
        Console.WriteLine($"MAR: {O(Access(Register.MAR).Get())}");
        Console.WriteLine($"TMP: {O(Access(Register.TMP).Get())}");
        Console.WriteLine($"DST: {O(Access(Register.DST).Get())}");
        Console.WriteLine($"VEC: {O(Access(Register.VEC).Get())}");
        Console.WriteLine($"COZN T PPP PM CM");
        Console.WriteLine($"{(flags >> 0) & 1}{(flags >> 1) & 1}{(flags >> 2) & 1}{(flags >> 3) & 1} {(flags >> 4) & 1} {(flags >> 7) & 1}{(flags >> 6) & 1}{(flags >> 5) & 1} {(flags >> 13) & 1}{(flags >> 12) & 1} {(flags >> 15) & 1}{(flags >> 14) & 1}");
        Console.WriteLine("\n*********************");
    }
    
    private string O(int input)
        => $"0x{Convert.ToString(input, 16).ToUpper()}";
}