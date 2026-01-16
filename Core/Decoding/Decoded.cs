namespace pdp11_emulator.Core.Decoding;
using Signaling.Cycles;

public class Decoded
{
    public RegisterAction[] Registers = [];
    
    public List<MicroCycle> MicroCycles 
        = [MicroCycle.FETCH_MAR, MicroCycle.PC_INC, MicroCycle.FETCH_MDR];
}