using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Queen : Piece
    {
        public Queen(Color color)
        {
            PieceColor = color;
            IsCaptured = false;
            if (color == Color.White)
                Symbol = "wQ";
            else
                Symbol = "bQ";
            PieceName = "Queen";
            Value = 9;
        }
        public override List<string> GetCaptures()
        {
            throw new NotImplementedException();
        }

        public override Moves GetPossibleMoves(Pos p1)
        {
            Moves m = new Moves();
            char _file = p1.File;
            int _rank = p1.Rank;
            // Horizontal moves
            for (char f = 'a'; f <= 'h'; f++)
            {
                if (f < _file)
                {
                    if (PieceColor == Color.White)
                    {
                        Pos p = new Pos(f, _rank);
                        m.AddLeftMove(p, true);
                    }

                    else
                    {
                        Pos p = new Pos(f, _rank);
                        m.AddRightMove(p, true);
                    }
                }
                else if (f > _file)
                {
                    if (PieceColor == Color.White)
                    {
                        Pos p = new Pos(f, _rank);
                        m.AddRightMove(p, false);
                    }
                    else
                    {
                        Pos p = new Pos(f, _rank);
                        m.AddLeftMove(p, false);
                    }
                }

            }

            // Vertical moves
            for (int r = 1; r <= 8; r++)
            {
                if (r < _rank)
                {
                    if (PieceColor == Color.White)
                    {
                        Pos p = new Pos(_file, r);
                        m.AddDownMove(p, true);
                    }
                    else
                    {
                        Pos p = new Pos(_file, r);
                        m.AddUpMove(p, true);
                    }
                }
                else if (r > _rank)
                {
                    if (PieceColor == Color.White)
                    {
                        Pos p = new Pos(_file, r);
                        m.AddUpMove(p, false);
                    }
                    else
                    {
                        Pos p = new Pos(_file, r);
                        m.AddDownMove(p, false);
                    }
                }
            }

            // Diagonals
            for (int offset = 1; offset < 8; offset++)
            {
                if (IsValidPosition((char)(_file + offset), _rank + offset))
                    if (PieceColor == Color.White)
                    {
                        Pos p = new Pos((char)(_file + offset), _rank + offset);
                        m.AddUpRightMove(p, false);
                    }
                    else
                    {
                        Pos p = new Pos((char)(_file + offset), _rank + offset);
                        m.AddDownLeftMove(p, false);
                    }

                if (IsValidPosition((char)(_file - offset), _rank + offset))
                    if (PieceColor == Color.White)
                    {
                        Pos p = new Pos((char)(_file - offset), _rank + offset);
                        m.AddUpLeftMove(p, false);
                    }
                    else
                    {
                        Pos p = new Pos((char)(_file - offset), _rank + offset);
                        m.AddDownRightMove(p, false);
                    }

                if (IsValidPosition((char)(_file + offset), _rank - offset))
                    if (PieceColor == Color.White)
                    {
                        Pos p = new Pos((char)(_file + offset), _rank - offset);
                        m.AddDownRightMove(p, false);
                    }
                    else
                    {
                        Pos p = new Pos((char)(_file + offset), _rank - offset);
                        m.AddUpLeftMove(p, false);
                    }

                if (IsValidPosition((char)(_file - offset), _rank - offset))
                    if (PieceColor == Color.White)
                    {
                        Pos p = new Pos((char)(_file - offset), _rank - offset);
                        m.AddDownLeftMove(p, false);
                    }
                    else
                    {
                        Pos p = new Pos((char)(_file - offset), _rank - offset);
                        m.AddUpRightMove(p, false);
                    }
            }

            return m;
        }


        public override List<string> GetSuporters()
        {
            throw new NotImplementedException();
        }

        public override List<string> GetThreats()
        {
            throw new NotImplementedException();
        }

    }
}
