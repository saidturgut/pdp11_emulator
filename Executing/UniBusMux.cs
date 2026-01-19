using System.Xml.Schema;

namespace pdp11_emulator.Executing;
using Signaling.Cycles;
using Signaling;
using Components;

public partial class DataPath
{
    private const Requester requesterType = Requester.CPU;
    
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

        UniBusDriving masked = signals.UniBusDriving;

        if (signals.ByteMode)
        {
            switch (signals.UniBusDriving)
            {
                case UniBusDriving.READ_WORD:
                    masked  = UniBusDriving.READ_BYTE;
                    break;
                case  UniBusDriving.WRITE_WORD:
                    masked = UniBusDriving.WRITE_BYTE;
                    break;
            }
        }
        
        Console.WriteLine(masked);
        
        uniBus.Request(new Request
        {
            Requester = (byte)requesterType,
            Address = Access(RegisterAction.MAR).Get(),
            Data = Access(RegisterAction.TMP).Get(),
            Operation = masked,
        });
    }
}