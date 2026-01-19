namespace pdp11_emulator.Executing;
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
        
        ushort A = action.RegisterOperand != RegisterAction.NONE
            ? Access(action.RegisterOperand).Get() : action.ConstOperand;
        
        AluOutput output = Alu.Compute(new AluInput
        {
            Operation = action.AluOperation,
            A = !signals.ByteMode ? A : (byte)(A & 0xFF),
            B = !signals.ByteMode ? cpuBus.Get() : (byte)(cpuBus.Get() & 0xFF),
            C = (Access(RegisterAction.PSW).Get() & (ushort)AluFlag.Carry) != 0,
            ByteMode = signals.ByteMode,
        });

        aluBus.Set(output.Result);

        ushort oldFlags = Access(RegisterAction.PSW).Get();
        
        Access(RegisterAction.PSW).Set((ushort)
            ((oldFlags & (ushort)~action.FlagMask) | (output.Flags & (ushort)action.FlagMask)));
    }
}