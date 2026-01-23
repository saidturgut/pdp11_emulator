namespace pdp11_emulator.Signaling;
using Decoding;
using Cycles;

public class TrapUnit : TrapUnitRom
{
    private TrapVector?[] TrapRequests = new  TrapVector?[6];
    
    public ushort VECTOR { get; private set; }
    public bool ABORT { get; private set; }
    public bool TRAP { get; private set; }
    
    public void Request(TrapVector vector)
    {
        Console.WriteLine($"TRAP REQUESTED: \"{vector}\" ");

        Trap request = TrapTable[vector];

        TrapRequests[request.Priority] = vector;
    }
    
    public void Arbitrate()
    {
        for (byte i = 0; i < TrapRequests.Length; i++)
        {
            if (TrapRequests[i] != null)
            {
                Trap request = TrapTable[TrapRequests[i]!.Value];

                VECTOR = request.Address;
                ABORT = request.Abort;
                TRAP = true;
                return;
            }
        }
        Clear();
    }

    public void Clear()
    {
        TrapRequests =  new TrapVector?[5];
        VECTOR = 0;
        ABORT = false;
        TRAP = false;
    }
}
