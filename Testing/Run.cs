namespace pdp1120.Testing;

internal static class Run
{
    private static readonly Pdp1140 Pdp1140 = new ();

    private static void Main() => Pdp1140.Power();
}