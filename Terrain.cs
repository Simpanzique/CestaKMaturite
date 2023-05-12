namespace Petr_RP_CestaKMaturite;

internal class Terrain {

    public PictureBox pb;
    public bool spawn = true;

    public Terrain(int positionX, int positionY, int width, int height, string Tag, Bitmap image, Panel scene) {
        pb = new PictureBox {
            Left = positionX,
            Top = positionY,
            Width = width,
            Height = height,
            Image = image,
            SizeMode = PictureBoxSizeMode.StretchImage,
            Tag = Tag,
        };
        if (!Tag.Contains("Spring"))
            scene.Controls.Add(pb);
    }
}
