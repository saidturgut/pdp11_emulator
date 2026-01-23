namespace pdp11_emulator.Executing;
using Signaling;
using Components;

public partial class DataPath
{
    public void CpuBusDrive(TriStateBus cpuBus)
    {
        if(Signals.CpuBusDriver is Register.NONE)
            return;

        ushort value = Access(Signals.CpuBusDriver).Get();

        Console.WriteLine(Signals.CpuBusDriver);
        
        cpuBus.Set((ushort)(Signals.CycleMode != CycleMode.BYTE_MODE 
            ? value : value & 0x00FF));
    }
    
    public void CpuBusLatch(TriStateBus cpuBus, TriStateBus aluBus)
    {
        if(Signals.CpuBusLatcher is Register.NONE)
            return;
        
        if (Signals.AluAction is not null)
            cpuBus = aluBus;

        if(Signals.Condition != Condition.NONE)
            if(!CheckCondition()) return;
        
        Console.WriteLine(Signals.CpuBusLatcher);
        
        Access(Signals.CpuBusLatcher).Set(cpuBus.Get());
    }
}