namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded ONE_OPERAND(ushort ir)
    {
        // ASSIGN ESSENTIALS
        Decoded decoded = new()
        {
            Drivers = [(Register)(ir & 0x7)],
            
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
        };
        if (ir >> 15 != 0)
            decoded.CycleMode = CycleMode.BYTE_MODE;
        
        // FLAG MASK
        decoded.FlagMask = decoded.Operation switch
        {
            Operation.INC or Operation.DEC => FlagMasks.Table[FlagMask.NZO],
            Operation.PASS or Operation.SWAB => FlagMasks.Table[FlagMask.NZ],
            Operation.SXT => FlagMasks.Table[FlagMask.Z],
            _ => FlagMasks.Table[FlagMask.NZOC]
        };
        
        // EA AND EXE ENGINES
        decoded.MicroCycles.AddRange(AddressEngine[(ir >> 3) & 0x7]);
        decoded.MicroCycles.Add(MicroCycle.EXECUTE_LATCH);

        // COMMIT ENGINE
        if (decoded.Operation is not Operation.PASS)
            decoded.MicroCycles.Add(
                ((ir >> 3) & 0x7) == 0 ? MicroCycle.COMMIT_ONE : MicroCycle.COMMIT_RAM);
        
        return decoded;
    }

    private readonly Operation[] SingleOperandTable =
    [
        Operation.ZERO, Operation.COM, Operation.INC, Operation.DEC,
        Operation.NEG, Operation.ADC, Operation.SBC, Operation.PASS,
        Operation.ASR, Operation.ASL, Operation.ROR, Operation.ROL, Operation.SWAB, Operation.SXT,
    ];
}