public static partial class Extensions
{
    public static System.Double GetFraction(this System.Double Double)
    {
        return Double - System.Math.Truncate(Double);
    }
}
