namespace pdp11_emulator.Decoding;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public struct Decoded()
{
    public RegisterAction[] Drivers = new RegisterAction[2];

    public AluOperation AluOperation = AluOperation.NONE;
    public AluFlag FlagMask = AluFlag.None;
    public byte StepSize = 2;
    
    public UniBusDriving UniBusDriving = UniBusDriving.NONE;
    public UniBusLatching UniBusLatching = UniBusLatching.NONE;

    public readonly List<MicroCycle> MicroCycles 
        = [MicroCycle.FETCH_MAR, MicroCycle.PC_INC, MicroCycle.FETCH_MDR, MicroCycle.DECODE];
}