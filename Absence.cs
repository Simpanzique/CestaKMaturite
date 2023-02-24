namespace Petr_RP_Silksong;
internal class Absence
{
    public PictureBox pb;
    public bool move = true;
    public int from;
    public int to;
    public int help;
    public int positionX;
    public int positionY;
    public int boundsX;
    public int boundsY;
    public int cooldown;
    public bool spawn = false;
    public bool spawned = false;
    public bool plusCoordinates;
    public bool changeDirection;
    public int indexDirection = 0;
    public int movementSpeed;
    public string type;
    static int Count = 1;


    public Absence(int _positionX, int _positionY, Color color, int _cooldown, string _type,
        bool _plusCoordinates, bool _changeDirection, int _from, int _to, int _movementSpeed)
    {
        positionX = _positionX;
        positionY = _positionY;
        boundsX = positionX;
        boundsY = positionY;
        type = _type;
        plusCoordinates = _plusCoordinates;
        changeDirection = _changeDirection;
        cooldown = _cooldown;
        movementSpeed = _movementSpeed;
        from = _from;
        to = _to;
        pb = new PictureBox
        {
            Left = _positionX,
            Top = _positionY,
            Width = 30,
            Height = 30,
            BackColor = color,
            Tag = "Absence",
            Name = "absence" + Count
        };

        Count++;
    }
}
