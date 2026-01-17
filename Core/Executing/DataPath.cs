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
    ];
    
    private SignalSet signals;

    private const Requester requesterType = Requester.CPU;
    public bool STALL;
    
    public void Init()
    {
        Access(RegisterAction.R0).Set(8);
        Access(RegisterAction.R1).Set(5);
        Access(RegisterAction.R2).Set(1);
        Access(RegisterAction.R3).Set(4);
        Access(RegisterAction.R4).Set(7);
        Access(RegisterAction.R5).Set(9);
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
        Console.WriteLine($"PC: {Access(RegisterAction.R7).Get()}");
        Console.WriteLine($"SP: {Access(RegisterAction.R6).Get()}");
        for (int i = 0; i < 6; i++) Console.WriteLine($"R{i} {Access((RegisterAction)i).Get()}");
        Console.WriteLine($"MDR: {Access(RegisterAction.MDR).Get()}");
        Console.WriteLine($"IR: {Access(RegisterAction.IR).Get()}");
        Console.WriteLine($"MAR: {Access(RegisterAction.MAR).Get()}");
        Console.WriteLine($"TMP: {Access(RegisterAction.TMP).Get()}");
        Console.WriteLine($"DST: {Access(RegisterAction.DST).Get()}");
    }
}