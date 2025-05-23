﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class enPassant
    {
        private static enPassant _instance;
        private static readonly object _lock = new object();
        private bool bAvailable=false;
        private Pos enPos=new Pos();
        private Pawn InDangerPawn;
        private Indicator m_Ind;
        private MovementIndicator M_Indicator = MovementIndicator.Instance;
        public static enPassant Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new enPassant();

                        }
                    }
                }
                return _instance;
            }
        }
        public static void Reset()
        {
            lock (_lock)
            {
                _instance = null;
            }
        }
        public void UpdateState(Piece p, Move m,Board B) //Update state before move
        {
            Pawn pn = new Pawn(p.PieceColor);
            if (p.GetType() == pn.GetType())
            {
                if (Math.Abs(p.cell.Position.Rank - m.Position.Rank) > 1)
                {
                    Cell c;
                    for (int j=-1;j<=1;j+=2)
                        {
                            c= B.GetCell(m.Position.FileIndex + j, m.Position.Rank);
                            if (c is object)
                            {
                                if ((c.CellStage == CellStage.BlackOccupied && p.PieceColor==Color.White)||
                                (c.CellStage == CellStage.WhiteOccupied && p.PieceColor == Color.Black))
                                {
                                    m_Ind = M_Indicator.GetIndex;
                                    enPos.File = m.Position.File;
                                    if (p.PieceColor == Color.White)
                                            enPos.Rank = m.Position.Rank-1;
                                    else
                                        enPos.Rank = m.Position.Rank + 1;
                                    
                                    bAvailable = true;
                                    break;
                                }

                            }
                        }
                }
            }
            else
                bAvailable = false;
        }


        public bool IsAvailable   // property
        {
            get { return bAvailable; }   // get method
            set { bAvailable = value; }   // set method
        }
        public Pos CapturePawn(Piece p, Pos position)
        {
            Pos ps = new Pos();
      
            Pawn pn = new Pawn(p.PieceColor);
            if (p.GetType() == pn.GetType())
                if (m_Ind.Equals(M_Indicator.GetIndex))
                    if (bAvailable)
                        if (position.Equals(enPos))
                        {
                            ps.File = InDangerPawn.cell.Position.File;
                            ps.Rank = InDangerPawn.cell.Position.Rank;
                            Cell c =InDangerPawn.cell;
                            c.SetPiece(null);
                            
                            return ps;
                        }
          
            return ps;
        }

      
        public Piece DangerPawn   // property
        {
            get { return InDangerPawn; }   // get method
            set { InDangerPawn = (Pawn)value; }   // set method
        }
        public Pos enPassantPos   // property
        {
            get { return enPos; }   // get method 
        }
      
    }
}
