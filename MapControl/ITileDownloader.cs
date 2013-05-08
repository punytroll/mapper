namespace System
{
    public interface ITileDownloader
    {
        System.Boolean SupportsTile(System.Windows.Forms.MapTile Tile);
        void FetchTile(System.Windows.Forms.MapTile Tile);
        System.String GetSetIdentifier();
        System.Int32 GetTileSize();
    }
}
