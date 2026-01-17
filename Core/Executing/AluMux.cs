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
            A = cpuBus.Get(),
            B = action.RegisterOperand != RegisterAction.NONE ? 
                Access(action.RegisterOperand).Get() : action.ConstOperand,
        });

        aluBus.Set(output.Result);
    }
}