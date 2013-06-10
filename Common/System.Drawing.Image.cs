public static partial class Common
{
    public static System.Drawing.Image ConvertToPixelFormat(this System.Drawing.Image Image, System.Drawing.Imaging.PixelFormat PixelFormat)
    {
        var Result = new System.Drawing.Bitmap(Image.Width, Image.Height, PixelFormat);

        using(var Graphics = System.Drawing.Graphics.FromImage(Result))
        {
            Graphics.DrawImage(Image, 0, 0);
        }

        return Result;
    }
}
