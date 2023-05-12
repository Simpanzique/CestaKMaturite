using Petr_RP_CestaKMaturite.Properties;

namespace Petr_RP_CestaKMaturite;
internal class Absence {

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

    public System.Windows.Forms.Timer absenceTimer = new();
    private int index = 0;


    public Absence(int _positionX, int _positionY, int _cooldown, string _type,
        bool _plusCoordinates, bool _changeDirection, int _from, int _to, int _movementSpeed) {
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
        pb = new PictureBox {
            Left = _positionX,
            Top = _positionY,
            Width = 30,
            Height = 30,
            Image = Resources.Absence,
            SizeMode = PictureBoxSizeMode.StretchImage,
        };

        absenceTimer.Interval = 1;
        absenceTimer.Tick += AbsenceTimer_Tick;
        absenceTimer.Start();
    }

    private void AbsenceTimer_Tick(object? sender, EventArgs e) {
        if (index == 0) {
            if (changeDirection) {
                if (indexDirection != 0) {
                    if (type == "X") {
                        if (positionX == from)
                            positionX = to;
                        else
                            positionX = from;
                    }
                    if (type == "Y") {
                        if (positionY == from)
                            positionY = to;
                        else
                            positionY = from;
                    }

                    plusCoordinates = !plusCoordinates;

                    help = from;
                    from = to;
                    to = help;

                    boundsX = positionX;
                    boundsY = positionY;
                }

                indexDirection++;
            }

            spawned = false;
            spawn = true;
            absenceTimer.Interval = cooldown;

            index++;
        }else if (index == 1) {
            spawn = false;
            absenceTimer.Interval = 1;

            index = 0;
        }
    }
}
