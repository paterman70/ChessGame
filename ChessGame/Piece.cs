using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{

    public enum Color
    {
        White,
        Black
    }

    public abstract class Piece:ICloneable
    {
        private Color _color; // field
        private bool bIsCaptured; // field
        private int iValue;
        //private string file;
        //private int rank;
        private string symbol;
        private List<Pos> Capture=new List<Pos>();
        private Moves PieceMoves = new Moves();
        private bool isMoved=false;
        private Cell MyCell=null;
        private string pieceName;

        
        public Color EnemyColor()
        {
            if (_color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }
       public string PieceName   // property
        {
            get { return pieceName; }   // get method
            set { pieceName = value; }   // set method
        }
        public Color PieceColor   // property
        {
            get { return _color; }   // get method
            set { _color = value; }   // set method
        }
        public Cell cell   // property
        {
            get { return MyCell; }   // get method
            set { MyCell = value; }   // set method
        }
        public bool IsMoved   // property
        {
            get { return this.isMoved; }   // get method
            set { this.isMoved = value; }   // set method
        }
        public bool IsCaptured   // property
        {
            get { return bIsCaptured; }   // get method
            set { bIsCaptured = value; }   // set method
        }

        public bool IsPosInMoves(Pos p)
        {
            return PieceMoves.IsPosInMoves(p);
        }
        public bool IsPosInCaptureMoves(Pos p)
        {
            return PieceMoves.IsPosInCaptureMoves(p);
        }
        public int Value   // property
        {
            get { return iValue; }   // get method
            set { iValue = value; }   // set method
        }
        public int Mobility()
        {
            return PieceMoves.NumOfMoves();
        }

        public bool IsSamePiece(Piece p)
        {
            if (p is object)

                return (this.GetType() == p.GetType()) && (this.PieceColor == p.PieceColor);
            else
                return false;
        }
      public void AddCaptureMove(Pos p)
        {
            Capture.Add(p);
        }
        public void ClearCaptureMoves()
        {
            Capture.Clear();
        }

        public void RemoveMove(Pos p)
        {
            PieceMoves.Remove(p);
        }

        public string Symbol   // property
        {
            get { return symbol; }   // get method
            set { symbol = value; }   // set method
        }
        public abstract Moves GetPossibleMoves(Pos p);
        public void SetMoves(Moves m)
        {
            PieceMoves = new Moves(m);
        }
        public Moves GetMoves()
        {
            return PieceMoves;
        }

        public List<Pos> GetMovesList()
        {
            return PieceMoves.GetMoves();
        }

        public abstract List<string> GetThreats();
        public abstract List<string> GetCaptures();
        public abstract List<string> GetSuporters();


        public bool IsValidPosition(char file, int rank)
        {
            return file >= 'a' && file <= 'h' && rank >= 1 && rank <= 8;
        }

        public object Clone()
        {
            return this.MemberwiseClone(); // Returns a shallow copy of the current object.
        }
        public bool KSCastle   // property
        {
            get { return PieceMoves.KSCastle; }   // get method
            set { PieceMoves.KSCastle = value; }   // set method
        }
        public bool QSCastle   // property
        {
            get { return PieceMoves.QSCastle; }   // get method
            set { PieceMoves.QSCastle = value; }   // set method
        }

       public void DiplayMoves()
        {
           
            Console.WriteLine(PieceName+" "+_color);
            PieceMoves.Diplay();
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

    }
}
