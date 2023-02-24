namespace Petr_RP_Silksong;

internal class Terrain
{
    public PictureBox pb;
    static int Count = 1;
    public Terrain(int positionX, int positionY, int width, int height, Color color, GroupBox scene)
    {
        pb = new PictureBox
        {
            Left = positionX,
            Top = positionY,
            Width = width,
            Height = height,
            BackColor = color,
            Tag = "Terrain",
            Name = "terrain" + Count
        };
        scene.Controls.Add(pb);

        Count++;
    }
}
