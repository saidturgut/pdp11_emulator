namespace pdp11_emulator.Executing;
using Signaling.Cycles;
using Signaling;
using Components;

public partial class DataPath
{
    private readonly RegisterObject[] Registers =
    [
        new (), // R0 * 0
        new (), // R1 * 1
        new (), // R2 * 2
        new (), // R3 * 3
        new (), // R4 * 4
        new (), // R5 * 5
        new (), // SP * 6
        new (), // PC * 7
        
        new (), // MDR * 8
        new (), // IR * 9
        new (), // MAR * 10
        
        new (), // TMP * 11
        new (), // DST * 12
        
        new (), // PSW * 13 
    ];
    
    private SignalSet signals;

    public bool STALL;
    
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
    
    public void Clear(TriStateBus cpuBus, TriStateBus aluBus)
    {
        cpuBus.Clear();
        aluBus.Clear();
    }

    public void Receive(SignalSet input) 
        => signals = input;

    private RegisterObject Access(Register register) 
        => Registers[(ushort)register];
    
    public ushort GetIr() 
        => Registers[(ushort)Register.IR].Get();

    public void Commit(bool abort)
    {
        foreach (RegisterObject register in Registers)
        {
            if (!abort)
                register.Commit();
            
            register.Init();
        }
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
        Console.WriteLine($"C O Z N T");
        Console.WriteLine($"{(flags >> 15) & 1} {(flags >> 14) & 1} {(flags >> 13) & 1} {(flags >> 12) & 1} {(flags >> 11) & 1}");
    }
    
    private string O(int input)
        => $"0x{Convert.ToString(input, 16).ToUpper()}";
}