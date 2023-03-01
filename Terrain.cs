namespace Petr_RP_Silksong;

internal class Terrain
{
    public PictureBox pb;
    static int Count = 1;
    public bool spawn = true;
    public Terrain(int positionX, int positionY, int width, int height, string Tag, Bitmap image, Panel scene)
    {
        pb = new PictureBox
        {
            Left = positionX,
            Top = positionY,
            Width = width,
            Height = height,
            BackgroundImage = image,
            BackgroundImageLayout = ImageLayout.Stretch,
            Tag = Tag,
            Name = "terrain" + Count,
        };
        //if (!Tag.Contains("Spring"))
            scene.Controls.Add(pb);


        Count++;
    }
}
