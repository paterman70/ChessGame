﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Rook : Piece
    {
        public Rook(Color color)
        {
            PieceColor = color;
            IsCaptured = false;
            if (color == Color.White)
                Symbol = "wR";
            else
                Symbol = "bR";
            PieceName = "Rook";
            Value = 5;
        }
        public Rook()
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
