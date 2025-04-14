using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class King : Piece
    {
        public King(Color color)
        {
            PieceColor = color;
            IsCaptured = false;
            if (color == Color.White)
                Symbol = "wK";
            else
                Symbol = "bK";
            PieceName = "King";
            Value = 4;
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

            //UL  U  UR R   DR  D   DL  L
            int[] fileOffsets = { -1, 0, 1, 1,  1,  0, -1, -1 };
            int[] rankOffsets = { 1,  1, 1, 0, -1, -1, -1,  0 };

            for (int i = 0; i < fileOffsets.Length; i++)
            {
                char newFile = (char)(_file + fileOffsets[i]);
                int newRank = _rank + rankOffsets[i];
                if (IsValidPosition(newFile, newRank))
                {
                    switch (i)
                    {
                        case 0:
                            if (PieceColor == Color.White)
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddUpLeftMove(p,false);
                            }
                            else
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddDownRightMove(p,true);
                            }
                        
                            break;

                        case 1:
                            if (PieceColor == Color.White)
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddUpMove(p,false);
                            }
                            else
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddDownMove(p,true);
                            }
                            break;
                        case 2:
                            if (PieceColor == Color.White)
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddUpRightMove(p,false);
                            }
                            else
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddDownLeftMove(p,true);
                            }
                           break;
                        case 3:
                            if (PieceColor == Color.White)
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddRightMove(p,false);
                            }
                            else
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddLeftMove(p,true);
                            }
                            break;
                        case 4:
                            if (PieceColor == Color.White)
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddDownRightMove(p,false);
                            }
                           
                            else
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddUpLeftMove(p,true);
                            }
                           
                            break;
                        case 5:
                            if (PieceColor == Color.White)
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddDownMove(p,false);
                            }
                             else
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddUpMove(p,true);
                            }
                          
                            break;
                        case 6:
                            if (PieceColor == Color.White)
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddDownLeftMove(p,false);
                            }
                            else
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddUpRightMove(p,true);
                            }
                           
                            break;
                        case 7:
                            if (PieceColor == Color.White)
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddLeftMove(p,false);
                            }
                              else
                            {
                                Pos p = new Pos(newFile, newRank);
                                m.AddRightMove(p,true);
                            }
                            break;
                        default:
                            break;
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
