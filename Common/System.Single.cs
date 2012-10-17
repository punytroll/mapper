public static partial class Common
{
    /// <summary>
    /// Rounds the Single value towards 0 and converts it to an Int32 value.
    /// </summary>
    public static System.Int32 GetTruncatedAsInt32(this System.Single Single)
    {
        return System.Convert.ToInt32(System.Math.Truncate(Single));
    }

    public static System.Single GetFraction(this System.Single Single)
    {
        return Single - System.Convert.ToSingle(System.Math.Truncate(Single));
    }
}
