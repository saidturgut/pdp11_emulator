namespace pdp11_emulator.Core.Executing;
using Signaling.Cycles;
using Signaling;
using Components;

public partial class DataPath
{
    public void UniBusLatch(UniBus uniBus)
    {
        if(signals.UniBusLatching is UniBusLatching.NONE)
            return;

        if (uniBus.requesters[(ushort)requesterType] != null)
        {
            STALL = true;
            return;
        }

        STALL = false;
        Access(RegisterAction.MDR).Set(uniBus.GetData());
    }
    
    public void UniBusDrive(UniBus uniBus)
    {
        if(signals.UniBusDriving is UniBusDriving.NONE)
            return;

        uniBus.Request(new Request
        {
            Requester = (byte)requesterType,
            Address = Access(RegisterAction.MAR).Get(),
            Data = Access(RegisterAction.TMP).Get(),
            Operation = signals.UniBusDriving,
        });
    }
}