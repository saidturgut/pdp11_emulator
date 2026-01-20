namespace pdp11_emulator;
using Executing.Components;
using Signaling;
using Utility;

public class Ram
{
    private readonly byte[] Memory = new byte[0x10000];

    private const ushort startAddress = 0;
    
    public void LoadImage(byte[] image, bool hexDump)
    {
        for (int i = 0; i < image.Length; i++)
            Memory[i + startAddress] = image[i];

        Memory[0x10] = 0xAA;
        Memory[0x11] = 0xAA;
        
        Memory[0x100] = 0x20;
        Memory[0x101] = 0x22;
        Memory[0x102] = 0x22;
        Memory[0x103] = 0x22;
        
        Memory[0x1FE] = 0x34;
        Memory[0x1FF] = 0x33;

        Memory[0x300] = 0x44;
        Memory[0x301] = 0x44;

        Memory[0x404] = 0x55;
        Memory[0x405] = 0x55;
        
        Memory[0x500] = 0x00;
        Memory[0x501] = 0x06;
        
        Memory[0x600] = 0x78;
        Memory[0x601] = 0x77;

        Memory[0x2002] = 0x00;
        Memory[0x2003] = 0x08;
        
        Memory[0x0800] = 0x96;
        Memory[0x0801] = 0x99;
        
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
        Memory[address + 1] = (byte)(value >> 8);

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
        => $"0x{Convert.ToString(input, 16).ToUpper()}";
}