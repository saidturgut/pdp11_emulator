namespace pdp11_emulator.Executing;
using Signaling;
using Components;

public partial class DataPath
{
    public void Init()
    {
        Access(Register.R0).Debug(0x10);
       Access(Register.R1).Debug(0x100);
        Access(Register.R2).Debug(0x200);
        Access(Register.R3).Debug(0x300);
        Access(Register.R4).Debug(0x400);
        Access(Register.R5).Debug(0x500);
        Access(Register.SP).Debug(0x200);
        //Access(Register.R7).Debug(0x2000);
        //Access(Register.PSW).Debug(0xFFFF);
    }

    public void Debug()
    {
        ushort flags = Access(Register.PSW).Get();
        Console.WriteLine($"PC: {O(Access(Register.PC).Get())}");
        Console.WriteLine($"SP: {O(Access(Register.SP).Get())}");
        for (int i = 0; i < 6; i++) Console.WriteLine($"R{i}: {O(Access((Register)i).Get())}");
        Console.WriteLine($"MDR: {O(Access(Register.MDR).Get())}");
        Console.WriteLine($"IR: {O(Access(Register.IR).Get())}");
        Console.WriteLine($"MAR: {O(Access(Register.MAR).Get())}");
        Console.WriteLine($"TMP: {O(Access(Register.TMP).Get())}");
        Console.WriteLine($"DST: {O(Access(Register.DST).Get())}");
        Console.WriteLine($"VEC: {O(Access(Register.VEC).Get())}");
        Console.WriteLine($"C O Z N T P P P");
        Console.WriteLine($"{(flags >> 15) & 1} {(flags >> 14) & 1} {(flags >> 13) & 1} {(flags >> 12) & 1} {(flags >> 11) & 1} {(flags >> 7) & 1} {(flags >> 6) & 1} {(flags >> 5) & 1}");
    }
    
    private string O(int input)
        => $"0x{Convert.ToString(input, 16).ToUpper()}";
}