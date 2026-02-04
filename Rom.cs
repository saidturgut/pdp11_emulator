namespace pdp1120;
using Testing;

public class Rom
{
    public void Boot(Ram ram)
    {
        Assembler.Run();
        
        ram.Init(File.ReadAllBytes("test.bin"), false);
    }
}