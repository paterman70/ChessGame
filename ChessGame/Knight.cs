using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Knight : Piece
    {
        public Knight(Color color)
        {
            PieceColor = color;
            IsCaptured = false;
            if (color == Color.White)
                Symbol = "wN";
            else
                Symbol = "bN";
            PieceName = "Knight";
            Value= 3;
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

            int[] fileOffsets = { -2, -1, 1, 2, 2, 1, -1, -2 };
            int[] rankOffsets = { 1, 2, 2, 1, -1, -2, -2, -1 };

            for (int i = 0; i < fileOffsets.Length; i++)
            {
                char newFile = (char)(_file + fileOffsets[i]);
                int newRank = _rank + rankOffsets[i];

                if (IsValidPosition(newFile, newRank))
                {
                    Pos p = new Pos(newFile, newRank);
                    m.AddKnightMove(p);
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
