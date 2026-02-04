namespace pdp1120.Executing;
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
            
            ByteMode = Signals.UseByteMode,
            
            Cw = Psw,
        });
        
        // SET ALU BUS
        aluBus.Set(output.Result);
        
        // SET FLAGS
        if(action.StepSize == 0) Psw.ZERO_LATCH = (output.Flags & (ushort)PswFlag.ZERO) != 0;
        
        Psw.Set(output.Flags, Signals.FlagMask);
    }

    private ushort InputMask(ushort target)
        => !Signals.UseByteMode ? target : (byte)(target & 0x00FF);
}