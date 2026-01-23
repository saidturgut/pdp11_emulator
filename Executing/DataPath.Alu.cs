namespace pdp11_emulator.Executing;
using Signaling;
using Computing;
using Components;

public partial class DataPath
{
    private readonly Alu Alu = new();
    
    public void AluAction(TriStateBus cpuBus, TriStateBus aluBus)
    {
        if(Signals.AluAction is null)
            return;
        
        AluAction action = Signals.AluAction.Value;
        
        AluOutput output = Alu.Compute(new AluInput
        {
            Operation = action.Operation,
            
            A = InputMask(cpuBus.Get()),
            
            B = InputMask(action.RegisterOperand != Register.NONE
                ? Access(action.RegisterOperand).Get() 
                : action.StepSize),
            
            ByteMode = Signals.CycleMode == CycleMode.BYTE_MODE,
            
            Cw = Cw,
        });
        
        // SET ALU BUS
        aluBus.Set(output.Result);
        
        // SET FLAGS
        if(action.StepSize == 0) 
            zeroLatch = (output.Flags & (ushort)PswFlag.ZERO) != 0;
        
        PswSet(output.Flags, Signals.FlagMask);
    }

    private ushort InputMask(ushort target)
        => Signals.CycleMode != CycleMode.BYTE_MODE ? target : (byte)(target & 0x00FF);
}