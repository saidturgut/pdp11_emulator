namespace pdp11_emulator.Core.Decoding;
using Signaling.Cycles;

public partial class DecoderMUX
{
    public MicroCycle[] FetchEngine =
    [
        MicroCycle.FETCH_MAR, MicroCycle.PC_INC, MicroCycle.FETCH_MDR, MicroCycle.DECODE,
    ];

    private readonly MicroCycle[][] EAEngine =
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
}