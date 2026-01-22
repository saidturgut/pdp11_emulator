namespace pdp11_emulator.Executing;
using Signaling;
using Components;

public partial class DataPath
{
    public void CpuBusDrive(TriStateBus cpuBus)
    {
        if(signals.CpuBusDriver is Register.NONE)
            return;

        ushort value = Access(signals.CpuBusDriver).Get();
        
        cpuBus.Set((ushort)(signals.CycleMode != CycleMode.BYTE_MODE 
            ? value : value & 0x00FF));
    }
    
    public void CpuBusLatch(TriStateBus cpuBus, TriStateBus aluBus)
    {
        if(signals.CpuBusLatcher is Register.NONE)
            return;
        
        if (signals.AluAction is not null)
            cpuBus = aluBus;

        if(signals.Condition != Condition.NONE)
            if(!CheckCondition()) return;
        
        Access(signals.CpuBusLatcher).Set(cpuBus.Get());
    }
}