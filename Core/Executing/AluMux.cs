namespace pdp11_emulator.Core.Executing;
using Signaling;
using Computing;
using Components;

public partial class DataPath
{
    private Alu Alu = new();
    
    public void AluAction(TriStateBus cpuBus, TriStateBus aluBus)
    {
        if(signals.AluAction is null)
            return;

        AluAction action = signals.AluAction.Value;
        
        AluOutput output = Alu.Compute(new AluInput
        {
            Operation = action.AluOperation,
            A = action.RegisterOperand != RegisterAction.NONE ? 
                Access(action.RegisterOperand).Get() : action.ConstOperand,
            B = cpuBus.Get(),
        });
        
        aluBus.Set(output.Result);

        ushort oldFlags = Access(RegisterAction.PSW).Get();
        
        Access(RegisterAction.PSW).Set((ushort)
            ((oldFlags & (ushort)~action.FlagMask) | (output.Flags & (ushort)action.FlagMask)));
    }
}