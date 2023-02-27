using Petr_RP_Silksong.Properties;
namespace Petr_RP_Silksong;

internal class Terrain
{
    public PictureBox pb;
    static int Count = 1;
    public bool spawn = true;
    public Terrain(int positionX, int positionY, int width, int height, Bitmap image, Panel scene)
    {
        pb = new PictureBox
        {
            Left = positionX,
            Top = positionY,
            Width = width,
            Height = height,
            BackgroundImage = image,
            BackgroundImageLayout = ImageLayout.Stretch,
            Tag = "Terrain",
            Name = "terrain" + Count,
        };
        if (width == 60 && height == 50)
        {
            pb.Name = "spring";
            pb.Tag = "Spring";
        }
        else
            scene.Controls.Add(pb);
            

        Count++;
    }
}
