namespace pdp11_emulator;
using pdp11_emulator.Misc;

public class Rom
{
    public void Boot(Ram ram)
    {
        Assembler.RunAssembler();

        ram.LoadImage(File.ReadAllBytes("test.bin"), false);
    }
}