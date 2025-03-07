using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Pawn : Piece
    {
      
        public Pawn( Color color)
        {
            PieceColor = color;
            IsCaptured = false;
           
            if (color == Color.White)
                Symbol = "wP";
            else
                Symbol = "bP";
            PieceName = "Pawn";
            Value = 1;
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

            int direction = (PieceColor == Color.White) ? 1 : -1;
            int startRank = (PieceColor == Color.White) ? 2 : 7;

            // Single forward move
            if (IsValidPosition(_file, _rank + direction))
                if (direction > 0)
                {
                    Pos p = new Pos(_file, _rank + direction);
                    m.AddUpMove(p,false);
                }
            else
                {
                    Pos p = new Pos(_file, _rank + direction);
                    m.AddUpMove(p,true);
                }

            // Double forward move from starting position
            if (_rank == startRank && IsValidPosition(_file, _rank + 2 * direction))
                if (direction > 0)
                {
                    Pos p = new Pos(_file, _rank + 2*direction);
                    m.AddUpMove(p,false);
                }
                else
                {
                    Pos p = new Pos(_file, _rank + 2*direction);
                    m.AddUpMove(p,true);
                }


            // Capture moves (simplified)
            if (IsValidPosition((char)(_file - 1), _rank + direction))
            {
                Pos p = new Pos((char)(_file - 1), _rank + direction);
                m.AddCaptureMove(p);
            }

            if (IsValidPosition((char)(_file + 1), _rank + direction))
            {
                Pos p = new Pos((char)(_file + 1), _rank + direction);
                m.AddCaptureMove(p);
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
