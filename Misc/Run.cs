namespace pdp11_emulator.Misc;

internal static class Run
{
    private static readonly Pdp11 Pdp11 = new ();

    private static void Main() => Pdp11.Power();
}