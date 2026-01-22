namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded TRAP(TrapVector vector, TrapUnit trapUnit)
    {
        trapUnit.Request(vector);
        return new Decoded();
    }
    
    public Decoded TRAP() => new()
    {
        Registers = [Register.PC, Register.PSW],
        Operation = Operation.SUB,
        MemoryMode = UniBusDriving.WRITE_WORD,
        
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
}