using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{

    public enum CellStage
    {
        WhiteOccupied,
        BlackOccupied,
        NonOccupied
    }

    public class Cell : ICloneable
    {

      
        private Pos _pos;
        private Piece mypiece =null;
        private CellStage cstage;
        private Color cellcolor;
        private string csymbol;
        public Cell(char f,int r,Piece p)
        {
            _pos = new Pos(f,r);
            mypiece = p;
            cellcolor = FindColor();

            if (cellcolor == Color.White)
                csymbol = " O";
            else
                csymbol = " X";

            if (p is object)
                SetCellStage(p.PieceColor);
            
            else
                cstage = CellStage.NonOccupied;
            
        }

        private void SetCellStage(Color piececolor)
        {
            switch (piececolor)
            {
                case Color.White:
                    cstage = CellStage.WhiteOccupied;
                    break;
                case Color.Black:
                    cstage = CellStage.BlackOccupied;
                    break;
                
                default:
                    break;
            }
        }

        public void SetPos(Pos p)
        {
            _pos = p;
        }
        //public char File   // property
        //{
        //    get { return file; }   // get method
        //    set { file = value; }   // set method
        //}
        //public int Rank   // property
        //{
        //    get { return rank; }   // get method
        //    set { rank = value; }   // set method
        //}

        public Pos Position   // property
        {
            get { return _pos; }   // get method
            set { _pos = value; }   // set method
        }
        public Pos MovePieceTo(Cell c)
        {
            Pos ps = new Pos();
            if (mypiece is object)
            {
                enPassant enPas = enPassant.Instance;
                if(enPas.IsAvailable && !(c.CellPiece is object) )
                    ps=enPas.CapturePawn(mypiece,c.Position);
               
                
                if (c.CellStage == CellStage.NonOccupied || mypiece.PieceColor != c.CellPiece.PieceColor)
                {
                    c.SetPiece((Piece)mypiece.Clone());
                    c.SetCellStage(mypiece.PieceColor);
                    c.CellPiece.IsMoved = true;
                    c.CellPiece.cell = c;
                    mypiece = null;
                    cstage = CellStage.NonOccupied;
                } 
               
            
            }
            return ps;
        }
       
        public void Promote(Cell c, Piece p)
        {
            Pawn pw=new Pawn(mypiece.PieceColor);
            if (mypiece is object && mypiece.GetType()==pw.GetType()) //check if piece is pawn
            {

                if (c.CellStage == CellStage.NonOccupied || mypiece.PieceColor != c.CellPiece.PieceColor)
                {
                    c.SetPiece( p);
                    c.SetCellStage(mypiece.PieceColor);
                    c.CellPiece.IsMoved = true;
                  
                }

            }

        }

        public bool HasPiece(Piece p)
        {
            if(mypiece is object)
             
            return (mypiece.GetType() == p.GetType()) && (mypiece.PieceColor==p.PieceColor);
            else
                return false;
        }

        public bool HasPiece()
        {
            return mypiece is object;
        }

        public Piece CellPiece  
        {
            get { return mypiece; }
           
        }
        public void SetPiece(Piece p)
        {
          if(p is object)
            {
                mypiece = p;
                if (p.PieceColor == Color.White)
                    cstage = CellStage.WhiteOccupied;
                else
                    cstage = CellStage.BlackOccupied;
                 mypiece.cell = this;

            }
          else
            {
                mypiece = p;
                cstage = CellStage.NonOccupied;
            }
           

        }


        public string Symbol()   // property
        {
            string symbol;
            if (mypiece is object)
                symbol = mypiece.Symbol;
            else
                symbol = csymbol;

            return symbol;
            
        }
        public CellStage CellStage   // property
        {
            get { return cstage; }   // get method
           
        }
       
    
        private Color FindColor()
        {
            Color c;
            List<char> Files = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            if((_pos.Rank+Files.IndexOf(_pos.File))%2==1)
               c = Color.White;
                       else
                c = Color.Black;
            return c;
        }

        public object Clone()
        {
            Cell c=(Cell)this.MemberwiseClone(); // Returns a shallow copy of the current object.
            //continue with deep copy
            Pos p = new Pos(this.Position.File, this.Position.Rank);
            c.SetPos(p);
            if (this.CellPiece is object)
            {
                Piece pc=null;
                switch (this.CellPiece)
                {
                    case King K:
                        pc = new King(this.CellPiece.PieceColor);
                        pc = (King)this.CellPiece.Clone();
                    break;
                    case Queen q:
                        pc = new Queen(this.CellPiece.PieceColor);
                        pc = (Queen)this.CellPiece.Clone();
                        break;
                    case Rook r:
                        pc = new Rook(this.CellPiece.PieceColor);
                        pc = (Rook)this.CellPiece.Clone();
                        break;
                    case Bishop B:
                        pc = new Bishop(this.CellPiece.PieceColor);
                        pc = (Bishop)this.CellPiece.Clone();
                        break;
                    case Knight k:
                        pc = new Knight(this.CellPiece.PieceColor);
                        pc = (Knight)this.CellPiece.Clone();
                        break;
                    case Pawn pn:
                        pc = new Pawn(this.CellPiece.PieceColor);
                        pc = (Pawn)this.CellPiece.Clone();
                        enPassant enPas = enPassant.Instance;
                        Pawn enpasspawn = (Pawn)enPas.DangerPawn;
                         if(enpasspawn is object)
                            {
                            
                            if (enpasspawn.cell.Position.Equals( p))
                                enPas.DangerPawn = pc;
                            }

                        break;
                    default:
                        break;
                }
                Moves m = new Moves(this.CellPiece.GetMoves());
                if (pc is object) pc.SetMoves(m);
                c.SetPiece(pc);
                pc.cell = c;
            }

            return c;
        }
    }
}
