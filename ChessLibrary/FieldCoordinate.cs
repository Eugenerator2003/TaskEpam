using System;

namespace ChessClassLibrary
{
    public struct FieldCoordinate
    {
        public int X { get; }
        public int Y { get; }

        public FieldCoordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static bool operator ==(FieldCoordinate coordinate1, FieldCoordinate coordinate2)
        {
            return (coordinate1.X == coordinate2.X && coordinate1.Y == coordinate2.Y);
        }

        public static bool operator !=(FieldCoordinate coordinate1, FieldCoordinate coordinate2)
        {
            return !(coordinate1 == coordinate2);
        }
        

        public override string ToString()
        {
            char x = ' ';
            switch (X)
            {
                case (1):
                    x = 'A';
                    break;
                case (2):
                    x = 'B';
                    break;
                case (3):
                    x = 'C';
                    break;
                case (4):
                    x = 'D';
                    break;
                case (5):
                    x = 'E';
                    break;
                case (6):
                    x = 'F';
                    break;
                case (7):
                    x = 'G';
                    break;
                case (8):
                    x = 'H';
                    break;
            }
            return $"{x}{Y}";
        }
    }
}
