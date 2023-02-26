namespace Petr_RP_Silksong;
internal class Nugget
{
    public PictureBox pb;
    public int healthAdd;
    static int Count = 1;
    public Nugget(int positionX, int positionY, int _healthAdd, Color color, Panel panel)
    {
        healthAdd = _healthAdd;
        pb = new PictureBox
        {
            Left = positionX,
            Top = positionY,
            Width = 65,
            Height = 70,
            BackColor = color,
            Tag = "Nugget",
            Name = "nugget" + Count
        };
        panel.Controls.Add(pb);

        Count++;
    }
}
