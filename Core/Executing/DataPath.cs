using pdp11_emulator.Core.Signaling;

namespace pdp11_emulator.Core.Executing;
using Components;

public partial class DataPath
{
    private readonly Register[] Registers =
    [
        new (), // R0
        new (), // R1
        new (), // R2
        new (), // R3
        new (), // R4
        new (), // R5
        new (), // Sp
        new () // Pc
    ];

    private readonly Register Mdr = new ();
    private readonly Register Ir = new ();
    private readonly Register Mar = new ();

    public void Init()
    {
    }
}