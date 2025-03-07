using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Bishop : Piece
    {
        public Bishop(Color color)
        {
            PieceColor = color;
            IsCaptured = false;
            if (color == Color.White)
                Symbol = "wB";
            else
                Symbol = "bB";
            PieceName = "Bishop";
            Value = 3;
        }
        public Bishop()
        {

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
