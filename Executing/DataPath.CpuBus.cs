using pdp11_emulator.Decoding;

namespace pdp11_emulator.Executing;
using Signaling;
using Components;

public partial class DataPath
{
    public void CpuBusDrive(TriStateBus cpuBus)
    {
        if(Signals.CpuBusDriver is Register.NONE)
            return;

        ushort value = Signals.CpuBusDriver != Register.PSW 
            ? Access(Signals.CpuBusDriver).Get() : Psw.Get();
        
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

        if (Signals.CpuBusLatcher == Register.PSW)
        {
            Psw.SetMask(FlagMasks.Table[FlagMask.ALL]);
            Psw.Set(cpuBus.Get()); return;
        }
        
        Access(Signals.CpuBusLatcher).Set(cpuBus.Get());
    }
}