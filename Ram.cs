namespace pdp1120;
using Signaling;
using Testing;
using Arbitrating;

public class Ram
{
    private readonly byte[] Memory = new byte[0x10000];

    private readonly Dictionary<uint, byte> WriteRequests = new();
    
    private const uint startAddress = 0;
    
    public void Init(byte[] image, bool hexDump)
    {
        for (uint i = 0; i < image.Length; i++)
            Memory[i + startAddress] = image[i];
        
        if (hexDump)
            HexDump.Write(Memory);
    }
    
    public void Respond(UniBus uniBus)
    {
        if(!uniBus.RESPOND_PERMIT)
            return;
        
        switch (uniBus.Operation)
        {
            case UniBusDriving.READ_WORD:
                uniBus.SetData(ReadWord(uniBus.GetAddress())); break;
            case UniBusDriving.READ_BYTE:
                uniBus.SetData(ReadByte(uniBus.GetAddress())); break;
            case UniBusDriving.WRITE_WORD:
                WriteWord(uniBus.GetAddress(), uniBus.GetData()); break;
            case UniBusDriving.WRITE_BYTE:
                WriteByte(uniBus.GetAddress(), (byte)uniBus.GetData()); break;
        } 
    }
    
    private ushort ReadWord(uint address)
    {
        return (ushort)(Memory[address] | (Memory[address + 1] << 8));
    }

    private void WriteWord(uint address, ushort value)
    {
        WriteRequests[address] = (byte)(value & 0xFF);
        WriteRequests[address + 1] = (byte)(value >> 8);
    }

    private byte ReadByte(uint address) 
        => Memory[address];

    private void WriteByte(uint address, byte value)
    {
        WriteRequests[address] = value;
    }

    public void Commit(bool abort)
    {
        if (abort)
        {
            WriteRequests.Clear();
            return;
        }
        
        foreach (uint address in WriteRequests.Keys)
        {
            Console.WriteLine($"MEMORY [{O(address)}] : {O(WriteRequests[address])}");
            Memory[address] = WriteRequests[address];
            WriteRequests.Remove(address);
        }
    }

    private string O(uint input)
        => $"0x{Convert.ToString(input, 16).ToUpper()}";
}