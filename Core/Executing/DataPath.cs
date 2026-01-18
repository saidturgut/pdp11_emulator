namespace pdp11_emulator.Core.Executing;
using Signaling.Cycles;
using Signaling;
using Components;

public partial class DataPath
{
    private readonly Register[] Registers =
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
        Access(RegisterAction.R0).Set(0x8000);
        Access(RegisterAction.R1).Set(0x0001);
        Access(RegisterAction.R2).Set(64);
        Access(RegisterAction.R3).Set(128);
        Access(RegisterAction.R4).Set(192);
        Access(RegisterAction.R5).Set(256);
        Access(RegisterAction.R6).Set(512);
        Access(RegisterAction.R7).Set(0x0200);
    }

    public void Clear(TriStateBus cpuBus, TriStateBus aluBus)
    {
        cpuBus.Clear();
        aluBus.Clear();
    }

    public void Receive(SignalSet input) 
        => signals = input;

    private Register Access(RegisterAction register) 
        => Registers[(ushort)register];
    
    public ushort GetIr() 
        => Registers[(ushort)RegisterAction.IR].Get();
    
    public void Debug()
    {
        ushort flags = Access(RegisterAction.PSW).Get();
        Console.WriteLine($"PC: {O(Access(RegisterAction.R7).Get())}");
        Console.WriteLine($"SP: {O(Access(RegisterAction.R6).Get())}");
        for (int i = 0; i < 6; i++) Console.WriteLine($"R{i}: {O(Access((RegisterAction)i).Get())}");
        Console.WriteLine($"MDR: {O(Access(RegisterAction.MDR).Get())}");
        Console.WriteLine($"IR: {O(Access(RegisterAction.IR).Get())}");
        Console.WriteLine($"MAR: {O(Access(RegisterAction.MAR).Get())}");
        Console.WriteLine($"TMP: {O(Access(RegisterAction.TMP).Get())}");
        Console.WriteLine($"DST: {O(Access(RegisterAction.DST).Get())}");
        Console.WriteLine($"C O Z N T");
        Console.WriteLine($"{(flags >> 15) & 1} {(flags >> 14) & 1} {(flags >> 13) & 1} {(flags >> 12) & 1} {(flags >> 11) & 1}");
    }
    
    private string O(int input)
        => $"{Convert.ToString(input, 8)}";
}