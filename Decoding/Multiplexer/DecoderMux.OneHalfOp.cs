namespace pdp1120.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    /*protected static Decoded ONE_HALF_OPERAND(ushort ir) => new()
    {
        Registers = [(Register)((ir >> 6) & 0x7), (Register)(ir & 0x7)],
        Operation = OneHalfOperandTable[(ushort)(OneHalfOperandType)((ir >> 9) - 56)],
        FlagMask = FlagMasks.Table[FlagMask.NZOC],
        MicroCycles =
        [
            ..AddressEngine[0],

            MicroCycle.INDEX_TOGGLE,

            ..AddressEngine[(ir >> 3) & 0x7],

            MicroCycle.EXECUTE_EA
        ],
    };*/

    protected static Decoded ONE_HALF_OPERAND(ushort ir)
    {
        Decoded decode = new Decoded()
        {
            Registers = [(Register)((ir >> 6) & 0x7), (Register)(ir & 0x7)],
            Operation = OneHalfOperandTable[(ushort)(OneHalfOperandType)((ir >> 9) - 56)],
            FlagMask = FlagMasks.Table[FlagMask.NZOC],
            MicroCycles =
            [
                ..AddressEngine[0],

                MicroCycle.INDEX_TOGGLE,

                ..AddressEngine[(ir >> 3) & 0x7],

                MicroCycle.EXECUTE_EA,
                
                ((ir >> 3) & 0x7) == 0 ? MicroCycle.TMP_TO_REG : MicroCycle.TMP_TO_UNI
            ],
        };
        
        return decode;
    }
    

    private enum OneHalfOperandType
    {
        MUL , DIV, ASH, ASHC, XOR,
    }

    public static readonly Operation[] OneHalfOperandTable =
    [
        Operation.MUL, Operation.DIV, 
        Operation.ASH, Operation.ASHC, 
        Operation.XOR, 
    ];
}