namespace pdp1120.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected static Decoded ONE_OPERAND(ushort ir)
    {
        // ASSIGN ESSENTIALS
        Decoded decoded = new()
        {
            Registers = [(Register)(ir & 0x7)],
            
            Operation = ((ir >> 6) & 0x3F)  switch
            {
                0x3 => Operation.SWAB,
                0x30 => Operation.ROR,
                0x31 => Operation.ROL,
                0x32 => Operation.ASR,
                0x33 => Operation.ASL,
                0x37 => Operation.SXT,
                _ => SingleOperandTable[(ir >> 6) & 0x7],
            },
            
            ByteMode = ir >> 15 != 0,
            
            MicroCycles = 
            [
                ..AddressEngine[(ir >> 3) & 0x7],
                MicroCycle.EXECUTE_EA
            ],
        };
        
        if (decoded.Operation is not Operation.PASS)
            decoded.MicroCycles.Add(((ir >> 3) & 0x7) == 0 ? MicroCycle.TMP_TO_REG : MicroCycle.TMP_TO_UNI);
        
        // FLAG MASK
        decoded.FlagMask = decoded.Operation switch
        {
            Operation.INC or Operation.DEC => FlagMasks.Table[FlagMask.NZO],
            Operation.PASS or Operation.SWAB => FlagMasks.Table[FlagMask.NZ],
            Operation.SXT => FlagMasks.Table[FlagMask.Z],
            _ => FlagMasks.Table[FlagMask.NZOC]
        };
        
        return decoded;
    }

    private static readonly Operation[] SingleOperandTable =
    [
        Operation.ZERO, Operation.COM, Operation.INC, Operation.DEC,
        Operation.NEG, Operation.ADC, Operation.SBC, Operation.PASS,
        Operation.ASR, Operation.ASL, Operation.ROR, Operation.ROL, Operation.SWAB, Operation.SXT,
    ];
}