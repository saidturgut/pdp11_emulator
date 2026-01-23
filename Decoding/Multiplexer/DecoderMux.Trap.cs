using pdp11_emulator.Executing.Components;

namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected static Decoded TRAP(TrapVector vector, TrapUnit trapUnit)
    {
        trapUnit.Request(vector);
        return new Decoded();
    }
    
    public static Decoded TRAP() => new()
    {
        Registers = [Register.PC, Register.PSW],
        Operation = Operation.SUB,
        MemoryMode = UniBusDriving.WRITE_WORD,
        
        FlagMask = FlagMasks.Table[FlagMask.ALL],
        
        MicroCycles =
        [
            // PUSH PC
            MicroCycle.SP_ALU,
            MicroCycle.REG_TO_TMP,
            MicroCycle.SP_TO_UNI,

            MicroCycle.VEC_TO_UNI,
            MicroCycle.MDR_TO_REG,

            MicroCycle.INDEX_TOGGLE,

            // PUSH PSW
            MicroCycle.SP_ALU,
            MicroCycle.REG_TO_TMP,
            MicroCycle.SP_TO_UNI,

            MicroCycle.VEC_INC,
            MicroCycle.VEC_TO_UNI,
            MicroCycle.MDR_TO_REG
        ]
    };
    
    protected static Decoded RTI(bool rtt) => new()
    {
        Registers = [Register.PC, Register.PSW],
        Operation = Operation.ADD,
        MemoryMode = UniBusDriving.READ_WORD,

        MicroCycles = 
        [
            MicroCycle.SP_TO_UNI,
            MicroCycle.MDR_TO_REG,
            MicroCycle.SP_ALU,
            MicroCycle.INDEX_TOGGLE,
            MicroCycle.SP_TO_UNI,
            MicroCycle.MDR_TO_REG,
            MicroCycle.SP_ALU,
        ],
        
        FlagMask = FlagMasks.Table[FlagMask.ALL],
        
        SuppressTrace = rtt,
    };
}