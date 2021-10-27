using System;

namespace ChessLibrary
{
    public struct FieldCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int isOnLine(FieldCoordinate coordinate)
        {
            int lineNum = 0;
            if (this.X == coordinate.X && this.Y != coordinate.Y)
            {
                if (this.Y < coordinate.Y)
                    lineNum = 4;
                else
                    lineNum = 2;
            }
            else if (this.Y == coordinate.Y && this.X != coordinate.X)
            {
                if (this.X < coordinate.X)
                    lineNum = 1;
                else
                    lineNum = 3;
            }
            return lineNum;
        }

        public int isOnDiagonal(FieldCoordinate coordinate)
        {
            int diagonalNum = 0;

            for (int i = 0; i < 4; i++)
            {
                bool found = false;
                int x_step = 0, y_step = 0;
                FieldCoordinate search = this;
                switch (i)
                {
                    case (0):
                        x_step = 1;
                        y_step = 1;
                        break;
                    case (1):
                        x_step = 1;
                        y_step = -1;
                        break;
                    case (2):
                        x_step = -1;
                        y_step = -1;
                        break;
                    case (3):
                        x_step = -1;
                        y_step = 1;
                        break;
                }
                while (search.X >= 1 && search.X <= 8 && search.Y >= 1 && search.Y <= 8)
                {
                    if (search == coordinate)
                    {
                        found = true;
                        break;
                    }
                    else
                    {
                        search.X += x_step;
                        search.Y += y_step;
                    }
                }

                if (found)
                {
                    switch (i)
                    {
                        case (0):
                            diagonalNum = 1;
                            break;
                        case (1):
                            diagonalNum = 2;
                            break;
                        case (2):
                            diagonalNum = 3;
                            break;
                        case (3):
                            diagonalNum = 4;
                            break;
                    }
                    break;
                }
            }

            return diagonalNum;
        }

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
                    x = 'a';
                    break;
                case (2):
                    x = 'b';
                    break;
                case (3):
                    x = 'c';
                    break;
                case (4):
                    x = 'd';
                    break;
                case (5):
                    x = 'e';
                    break;
                case (6):
                    x = 'f';
                    break;
                case (7):
                    x = 'g';
                    break;
                case (8):
                    x = 'h';
                    break;
            }
            return $"{x}{Y}";
        }
    }
}
