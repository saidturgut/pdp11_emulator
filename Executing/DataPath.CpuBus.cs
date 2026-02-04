using pdp1120.Decoding;

namespace pdp1120.Executing;
using Signaling;
using Components;

public partial class DataPath
{
    public void CpuBusDrive(TriStateBus cpuBus)
    {
        if(Signals.CpuBusDriver is Register.NONE)
            return;

        ushort value = Access(Signals.CpuBusDriver).Get();
        
        cpuBus.Set((ushort)(!Signals.UseByteMode ? value : value & 0x00FF));
    }
    
    public void CpuBusLatch(TriStateBus cpuBus, TriStateBus aluBus)
    {
        if(Signals.CpuBusLatcher is Register.NONE)
            return;
        
        if (Signals.AluAction is not null)
            cpuBus = aluBus;

        if(Signals.Condition != Condition.NONE)
            if(!CheckCondition()) return;
        
        Access(Signals.CpuBusLatcher).Set(cpuBus.Get());
        
        Psw.Update();
    }
}