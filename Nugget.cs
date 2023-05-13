namespace Petr_RP_CestaKMaturite;
internal class Nugget : IDisposable{

    public PictureBox pb;
    public int healthAdd;

    public Nugget(int positionX, int positionY, int _healthAdd, Bitmap nugeta, Panel panel) {
        healthAdd = _healthAdd;
        pb = new PictureBox {
            Left = positionX,
            Top = positionY,
            Width = 65,
            Height = 70,
            Image = nugeta,
        };
        panel.Controls.Add(pb);
    }

    public void Dispose() {
        pb.Parent?.Controls.Remove(pb);
        pb.Dispose();
        pb.Bounds = Rectangle.Empty;
    }
}
