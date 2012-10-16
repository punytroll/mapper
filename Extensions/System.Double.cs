public static partial class Extensions
{
    public static System.Double GetFraction(this System.Double Double)
    {
        return Double - System.Math.Truncate(Double);
    }

    /// <summary>
    /// Rounds the Double value towards 0 and converts it to an Int32 value.
    /// </summary>
    public static System.Int32 GetTruncatedAsInt32(this System.Double Double)
    {
        return System.Convert.ToInt32(System.Math.Truncate(Double));
    }
}
