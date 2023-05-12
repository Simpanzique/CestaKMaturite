namespace Petr_RP_CestaKMaturite;
internal class Nugget {

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
}
