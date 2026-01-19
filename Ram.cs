namespace pdp11_emulator;
using Executing.Components;
using Signaling;
using Utility;

public class Ram
{
    private readonly byte[] Memory = new byte[0x10000];

    public void LoadImage(byte[] image, bool hexDump)
    {
        for (int i = 0; i < image.Length; i++)
            Memory[i] = image[i];
        
        if (hexDump)
            HexDump.Write(Memory);
    }
    
    public void Respond(UniBus uniBus)
    {
        if(uniBus.GetAddress() >= 0x8000 || !uniBus.respondPermit)
            return;

        switch (uniBus.operation)
        {
            case UniBusDriving.READ_WORD:
                uniBus.SetData(ReadWord(uniBus.GetAddress())); break;
            case UniBusDriving.READ_BYTE:
                uniBus.SetData(ReadByte(uniBus.GetAddress())); break;
            case UniBusDriving.WRITE_WORD:
                WriteWord(uniBus.GetAddress(), uniBus.GetData()); break;
            case UniBusDriving.WRITE_BYTE:
                WriteByte(uniBus.GetAddress(), (byte)uniBus.GetData()); break;
            default:
                throw new Exception("UNKNOWN OPERATION!");
        } 
    }
    
    private ushort ReadWord(ushort address)
    {
        if (address % 2 != 0)
        {
            throw new Exception("ODD ADDRESSES ARE ILLEGAL!!");
        }
        
        return (ushort)(Memory[address] | (Memory[address + 1] << 8));
    }

    private void WriteWord(ushort address, ushort value)
    {
        if (address % 2 != 0)
        {
            throw new Exception("ODD ADDRESSES ARE ILLEGAL!!");
        }

        Memory[address] = (byte)(value & 0xFF);
        Memory[address + 1] = (byte)(value >> 6);

        Console.WriteLine($"MEMORY [{O(address)}] : {O(value)}");
    }

    private byte ReadByte(ushort address) 
        => Memory[address];

    private void WriteByte(ushort address, byte value)
    {
        Memory[address] = value;
        
        Console.WriteLine($"MEMORY [{O(address)}] : {O(value)}");
    }

    private string O(int input)
        => $"{Convert.ToString(input, 8)}";
}