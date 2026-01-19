namespace pdp11_emulator.Decoding;
using Signaling.Cycles;
using Executing.Computing;

public class DecoderRom
{
    protected MicroCycle[] FetchEngine =
    [
        MicroCycle.FETCH_MAR, MicroCycle.PC_INC, MicroCycle.FETCH_MDR, MicroCycle.DECODE,
    ];

    protected readonly MicroCycle[][] EaEngine =
    [
        /*0*/[MicroCycle.EA_REG],
        /*1*/[MicroCycle.EA_REG_MAR, MicroCycle.EA_RAM_MAR, MicroCycle.EA_RAM_MDR],
        /*2*/[MicroCycle.EA_REG_MAR, MicroCycle.EA_INC, MicroCycle.EA_RAM_MDR],
        /*3*/[MicroCycle.EA_REG_MAR, MicroCycle.EA_INC, MicroCycle.EA_RAM_MAR, MicroCycle.EA_RAM_MDR],
        /*4*/[MicroCycle.EA_DEC, MicroCycle.EA_REG_MAR, MicroCycle.EA_RAM_MDR],
        /*5*/[MicroCycle.EA_DEC, MicroCycle.EA_REG_MAR, MicroCycle.EA_RAM_MAR, MicroCycle.EA_RAM_MDR],
        /*6*/[MicroCycle.EA_INDEX_MAR, MicroCycle.PC_INC, MicroCycle.EA_INDEX_MDR, MicroCycle.EA_RAM_MDR],
        /*7*/[MicroCycle.EA_INDEX_MAR, MicroCycle.PC_INC, MicroCycle.EA_INDEX_MDR, MicroCycle.EA_RAM_MAR, MicroCycle.EA_RAM_MDR],
    ];

    protected readonly Dictionary<FlagMask, AluFlag> FlagMasks = new()
    {
        { FlagMask.NZOC, AluFlag.Negative | AluFlag.Zero | AluFlag.Overflow | AluFlag.Carry },
        { FlagMask.NZO, AluFlag.Negative | AluFlag.Zero | AluFlag.Overflow },
        { FlagMask.NZ, AluFlag.Negative | AluFlag.Zero },
    };
    
    protected enum FlagMask
    {
        NZOC, NZO, NZ,
    }
}