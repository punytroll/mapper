namespace Test
{
    internal static class Application
    {
        [System.STAThread]
        internal static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.Run(new MainWindow());
        }
    }
}
