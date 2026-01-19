namespace pdp11_emulator;
using Utility;

public class Rom
{
    public void Boot(Ram ram)
    {
        Assembler.Run();

        ram.LoadImage(File.ReadAllBytes("test.bin"), false);
    }
}