namespace pdp11_emulator.Executing;
using Signaling;
using Computing;
using Components;

public partial class DataPath
{
    private readonly Alu Alu = new();
    
    public void AluAction(TriStateBus cpuBus, TriStateBus aluBus, TrapUnit trapUnit)
    {
        Cw.SetFlags(Access(Register.PSW).Get());
        
        if(signals.AluAction is null)
            return;
        
        AluAction action = signals.AluAction.Value;
        
        AluOutput output = Alu.Compute(new AluInput
        {
            Operation = action.Operation,
            
            A = InputMask(cpuBus.Get()),
            
            B = InputMask(action.RegisterOperand != Register.NONE
                ? Access(action.RegisterOperand).Get() 
                : action.StepSize),
            
            ByteMode = signals.CycleMode == CycleMode.BYTE_MODE,
            
            Cw = Cw,
        });

        // OVERFLOW TRAP
        if(Cw.Trace && ((output.Flags & (ushort)PswFlag.Overflow) != 0))
            trapUnit.Request(TrapVector.OVERFLOW, true);
        
        // SET ALU BUS
        aluBus.Set(output.Result);
        
        // SET FLAGS
        if(action.StepSize == 0) 
            zeroLatch = (output.Flags & (ushort)PswFlag.Zero) != 0;
        
        SetFlags(output.Flags, signals.FlagMask);
    }

    private ushort InputMask(ushort target)
        => signals.CycleMode != CycleMode.BYTE_MODE ? target : (byte)(target & 0x00FF);
}