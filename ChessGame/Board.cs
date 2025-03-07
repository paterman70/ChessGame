using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
   
    public class Board:IBoard
    {
        List<char> Files = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        List<Piece> WhitePieces;
        List<Piece> BlackPieces;

        List<List<Cell>> MyBoard;
        bool bBlackIsChecked;
        bool bWhiteIsChecked;
        private MovementIndicator MoveIndicator;

        public Board()
        {
            MyBoard = new List<List<Cell>>();
            WhitePieces = new List<Piece>();
            BlackPieces = new List<Piece>();
            MoveIndicator = MovementIndicator.Instance;

            bBlackIsChecked = false;
            bWhiteIsChecked=false;

            InitBoard();
            UpdateMoves();
           
        }

        private void InitBoard()
        {
            Cell c;
             for (int i=1;i<=8;i++)
            {
                List<Cell> f = new List<Cell>();
                for (int j = 0; j < 8; j++)
                {
                    c = new Cell(Files[j],i,null);
                    f.Add(c);
                }
                MyBoard.Add(f);
            }

            InitialPieces(Color.White);
            InitialPieces(Color.Black);
        }

        private void InitialPieces(Color color)
        {
          
            int rank;
            InitialPawns(color);
            if (color == Color.White)
                rank = 1;
            else
                rank = 8;

            InitialPieces(rank, color);

        }

        private void InitialPawns(Color color)
        {
        
            Pawn p;

            int rank,f;
            if (color == Color.White)
                rank = 2;
            else
                rank = 7;

            for (int i = 0; i < Files.Count; i++)
            {
                p = new Pawn(color);

                f = Files.IndexOf(Files[i]);
                MyBoard[rank - 1][f].SetPiece(p);
            }
           
        }

        private void InitialPieces(int rank, Color color)
        {
            Rook r;
            Knight k;
            Bishop B;
            Queen Q;
            King K;
            int f;

            r = new Rook(color);
            f = Files.IndexOf('a');
            MyBoard[rank - 1][f].SetPiece(r);
           

            r = new Rook(color);
            f = Files.IndexOf('h');
            MyBoard[rank - 1][f].SetPiece(r);
            /////////////////////////////////
            k = new Knight(color);
            f = Files.IndexOf('b');
            MyBoard[rank - 1][f].SetPiece(k);

            k = new Knight(color);
            f = Files.IndexOf('g');
            MyBoard[rank - 1][f].SetPiece(k);
            /////////////////////////////////
            B = new Bishop(color);
            f = Files.IndexOf('c');
            MyBoard[rank - 1][f].SetPiece(B); 
            B = new Bishop(color);
            f = Files.IndexOf('f');
            MyBoard[rank - 1][f].SetPiece(B);
            /////////////////////////////////
            Q = new Queen(color);
            f = Files.IndexOf('d');
            MyBoard[rank - 1][f].SetPiece(Q);
            /////////////////////////////////
            K = new King(color);
            f = Files.IndexOf('e');
            MyBoard[rank - 1][f].SetPiece(K) ;
        }
       
        public List<Piece> GetPieces(Color c)
        {
            List<Piece> lP = new List<Piece>();
            Piece p;
           
            for (int i = 0; i < 8; i++)
            {             
                for (int j = 0; j < 8; j++)
                {
                   p= MyBoard[i][j].CellPiece;
                  
                    if (p is object && p.PieceColor==c)
                        lP.Add(p);                
                }
            }
            return lP;
        }

        public List<Piece> GetPieces()
        {
            List<Piece> lP = new List<Piece>();
            Piece p;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    p = MyBoard[i][j].CellPiece;

                    if (p is object )
                        lP.Add(p);
                }
            }
            return lP;
        }
        public Piece PieceOfMove(Move m)
        {
            List<Piece> lP = GetPieces(m.color);
            Piece p = null;
            

            for (int i=0;i<lP.Count;i++)
            {
              
               
                if (lP[i].IsPosInMoves(m.Position)&& lP[i].PieceName==m.PieceName)
                {
                    if (m.Disambiguation.Length == 0) //no Disambiguation
                    {
                        p = lP[i];
                     
                        break;
                    }
                    else
                    {
                        string st = m.Disambiguation.Substring(m.Disambiguation.IndexOf(" "));
                        st = st.Trim();
                        if(int.TryParse(st, out int n)) //Disambiguation in Rank
                        {
                            if (lP[i].cell.Position.Rank==n)
                            {
                                p = lP[i];
                                break;
                            }
                        }
                        else //Disambiguation in file
                        {
                            char ch = Convert.ToChar(st);
                            if (lP[i].cell.Position.File == ch)
                            {
                                p = lP[i];
                                break;
                            }

                        }
                    }
                }

            }
            return p;
        }

        public List<Cell> Castle(Color c,string castle)
        {
            List<Cell> ls = new List<Cell>();
           

            if (c==Color.White)
            {

                King Ke1 = (King)MyBoard[0][Files.IndexOf('e')].CellPiece;


                switch (castle)
                {
                
                    case "KingSideCastle":
                        Rook Rh1 = (Rook)MyBoard[0][Files.IndexOf('h')].CellPiece;
                        Cell c1 = MyBoard[0][Files.IndexOf('f')];
                        Cell c2 = MyBoard[0][Files.IndexOf('g')];
                      
                        if (Rh1 is object && !Rh1.IsMoved && Ke1 is object && !Ke1.IsMoved)                     
                            if(c1.CellStage==CellStage.NonOccupied && c2.CellStage == CellStage.NonOccupied)                           
                                if(PiecesInPath(Ke1.cell,Color.Black).Count==0)
                                    if (PiecesInPath(c1, Color.Black).Count == 0)
                                        if (PiecesInPath(c2, Color.Black).Count == 0)
                                        {
                                            ls.Add(Ke1.cell); ls.Add(c2);
                                            ls.Add(Rh1.cell); ls.Add(c1);

                                        }

                        break;
                    case "QueenSideCastle":
                        Rook Ra1 = (Rook)MyBoard[0][Files.IndexOf('a')].CellPiece;
                          c1 = MyBoard[0][Files.IndexOf('c')];
                          c2 = MyBoard[0][Files.IndexOf('d')];
                       

                        if (Ra1 is object && !Ra1.IsMoved && Ke1 is object && !Ke1.IsMoved)
                            if (c1.CellStage == CellStage.NonOccupied && c2.CellStage == CellStage.NonOccupied)
                                if (PiecesInPath(Ke1.cell, Color.Black).Count == 0)
                                    if (PiecesInPath(c1, Color.Black).Count == 0)
                                        if (PiecesInPath(c2, Color.Black).Count == 0)
                                        {
                                            ls.Add(Ke1.cell); ls.Add(c1);
                                            ls.Add(Ra1.cell); ls.Add(c2);
                                        }

                        break;
                   
                    default:
                        
                        break;

                }

            }
            else
            {
                King Ke8 = (King)MyBoard[7][Files.IndexOf('e')].CellPiece;


                switch (castle)
                {

                    case "KingSideCastle":
                        Rook Rh8 = (Rook)MyBoard[7][Files.IndexOf('h')].CellPiece;
                        Cell c1 = MyBoard[7][Files.IndexOf('f')];
                        Cell c2 = MyBoard[7][Files.IndexOf('g')];

                        if (Rh8 is object && !Rh8.IsMoved && Ke8 is object && !Ke8.IsMoved)
                            if (c1.CellStage == CellStage.NonOccupied && c2.CellStage == CellStage.NonOccupied)
                                if (PiecesInPath(Ke8.cell, Color.White).Count == 0)
                                    if (PiecesInPath(c1, Color.White).Count == 0)
                                        if (PiecesInPath(c2, Color.White).Count == 0)
                                        {
                                            ls.Add(Ke8.cell); ls.Add(c2);
                                            ls.Add(Rh8.cell); ls.Add(c1);
                                        }

                        break;
                    case "QueenSideCastle":
                        Rook Ra8 = (Rook)MyBoard[7][Files.IndexOf('a')].CellPiece;
                        c1 = MyBoard[7][Files.IndexOf('c')];
                        c2 = MyBoard[7][Files.IndexOf('d')];


                        if (Ra8 is object && !Ra8.IsMoved && Ke8 is object && !Ke8.IsMoved)
                            if (c1.CellStage == CellStage.NonOccupied && c2.CellStage == CellStage.NonOccupied)
                                if (PiecesInPath(Ke8.cell, Color.White).Count == 0)
                                    if (PiecesInPath(c1, Color.White).Count == 0)
                                        if (PiecesInPath(c2, Color.White).Count == 0)
                                        {
                                            ls.Add(Ke8.cell); ls.Add(c1);
                                            ls.Add(Ra8.cell); ls.Add(c2);
                                        }

                        break;

                    default:

                        break;

                }
            }

            return ls;
        }

        public void UpdateMoves()
        {
            UpdateWithoutThreads();
            CheckForThreats(Color.White);
            CheckForThreats(Color.Black);


        }

        private void UpdateWithoutThreads()
        {
            Piece p;
            Moves m;
            Cell _cell;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _cell = MyBoard[i][j];
                    p = _cell.CellPiece;
                    if (p is object)
                    {
                        m = p.GetPossibleMoves(_cell.Position);

                        ValidateMoves(ref m, _cell);
                        p.SetMoves(m);
                    }
                }
            }
        }
        private void CheckForThreats(Color c)
        {
            List<Piece> ls = PiecesThreatingKing(c);
            if (ls.Count == 0)
            {
                Castling(c);
                switch(c)
                {
                    case Color.White:
                        bWhiteIsChecked = false;
                        break;
                    case Color.Black:
                        bBlackIsChecked = false;
                        break;
                }
                
            }
            else
            {
                switch (c)
                {
                    case Color.White:
                        bWhiteIsChecked = true;
                        break;
                    case Color.Black:
                        bBlackIsChecked = true;
                        break;

                }
                AvoidCheck(c);
            }
        }

        public void AvoidCheck(Color c)
        {
            Piece p;
            Cell _cell,newcell;
            List<Pos> ls;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _cell = MyBoard[i][j];
                    
                    p = _cell.CellPiece;
                    if (p is object && p.PieceColor==c)
                    {
                        newcell = (Cell)_cell.Clone();
                        ls =p.GetMovesList();
                      
                        List<List<Cell>> TestBoard = CopyBoard(MyBoard);
                        for(int k=0;k<ls.Count;k++)
                        {
                            _cell.MovePieceTo(MyBoard[ls[k].Rank-1][Files.IndexOf(ls[k].File)]);
                            UpdateWithoutThreads();
                            if (PiecesThreatingKing(c).Count > 0)
                            {
                                MyBoard = CopyBoard(TestBoard);
                                
                                newcell.CellPiece.RemoveMove(ls[k]);
                            }
                            else
                                MyBoard = CopyBoard(TestBoard);
                            
                            _cell = MyBoard[i][j];
                        }
                        MyBoard[i][j] = newcell;
                    }
                }
            }
        }
        private List<Piece> PiecesThreatingKing(Color c)
        {
            Cell KingCell;
            
            List<Piece> lp=null;
            King K = new King(c);
           
            
            Color ec;
            if (c == Color.White)
                ec = Color.Black;
            else
                ec = Color.White;
            List<Cell> ls = FindPiece(K);
           
            if (ls.Count > 0)
            {
                KingCell = ls[0];
                lp = PiecesInPath(KingCell, ec);
                lp = FilterCaptureMoves(KingCell.Position, lp);
             
            }
                return lp;
        }
        private List<Piece> FilterCaptureMoves(Pos KingPos,List<Piece> ls)
        {
            List<Piece> lcm = new List<Piece>();
            Pawn pn;
            Piece p;
            //bool bOnlyCaptures;
            for (int i=0;i<ls.Count;i++)
            {
                p = ls[i];
                pn=new Pawn(p.PieceColor);
                if (p.GetType() == pn.GetType())
                {
                    if (p.IsPosInCaptureMoves(KingPos))
                        lcm.Add(p);
                }
                else
                    lcm.Add(p);

            }
            return lcm;
        }
        private List<List<Cell>> CopyBoard(List<List<Cell>> source)
        {
            List<List<Cell>> dest=null;
            List<Cell> element;
            if (source.Count > 0)
            {
                dest = new List<List<Cell>>();
                for (int i=0; i<source.Count;i++)
                {
                    element = new List<Cell>();
                    for (int j = 0; j < source[i].Count; j++)
                    {
                        element.Add((Cell)source[i][j].Clone());
                    }

                    dest.Add(element);
                }
             }
            return dest;
        }

       private void ValidateMoves(ref Moves m,Cell _cell)
        {
            Color mycolor = _cell.CellPiece.PieceColor;
            List<Pos> CaptureMoves = new List<Pos>();
            _cell.CellPiece.ClearCaptureMoves();
            switch(_cell.CellPiece)
            {
                case Pawn p:
                    {
                        m.SetUpMoves(FilterMoves(m.GetUpMoves(), ref CaptureMoves, mycolor, true));
                        m.SetCaptureMoves(FilterCaptureMoves(m.GetCaptureMoves(), ref CaptureMoves, mycolor));
                        break;
                    }
                case Rook r:
                    {
                         m.SetUpMoves(FilterMoves(m.GetUpMoves(), ref CaptureMoves, mycolor,  true));
                         m.SetDownMoves(FilterMoves(m.GetDownMoves(), ref CaptureMoves, mycolor, true));
                         m.SetLeftMoves(FilterMoves(m.GetLeftMoves(), ref CaptureMoves, mycolor, true));
                         m.SetRightMoves(FilterMoves(m.GetRightMoves(), ref CaptureMoves, mycolor, true));
                        break;
                    }
                case Knight k:
                    {
                        m.SetKnightMoves(FilterMoves(m.GetKnightMoves(), ref CaptureMoves, mycolor, false));
                        break;
                    }
                case Bishop B:
                    {
                        m.SetUpRightMoves(FilterMoves(m.GetUpRightMoves(), ref CaptureMoves, mycolor, true));
                        m.SetDownRightMoves(FilterMoves(m.GetDownRightMoves(), ref CaptureMoves, mycolor, true));
                        m.SetUpLeftMoves(FilterMoves(m.GetUpLeftMoves(), ref CaptureMoves, mycolor, true));
                        m.SetDownRightMoves(FilterMoves(m.GetDownRightMoves(), ref CaptureMoves, mycolor, true));
                        break;
                    }
                case Queen Q:
                case King K:
                    {
                        m.SetUpMoves(FilterMoves(m.GetUpMoves(), ref CaptureMoves, mycolor, true));
                        m.SetDownMoves(FilterMoves(m.GetDownMoves(), ref CaptureMoves, mycolor, true));
                        m.SetLeftMoves(FilterMoves(m.GetLeftMoves(), ref CaptureMoves, mycolor, true));
                        m.SetRightMoves(FilterMoves(m.GetRightMoves(), ref CaptureMoves, mycolor, true));
                        m.SetUpRightMoves(FilterMoves(m.GetUpRightMoves(), ref CaptureMoves, mycolor, true));
                        m.SetDownRightMoves(FilterMoves(m.GetDownRightMoves(), ref CaptureMoves, mycolor, true));
                        m.SetUpLeftMoves(FilterMoves(m.GetUpLeftMoves(), ref CaptureMoves, mycolor, true));
                        m.SetDownRightMoves(FilterMoves(m.GetDownRightMoves(), ref CaptureMoves, mycolor, true));
                        break;
                    }
            }
           
            
            for (int i = 0; i < CaptureMoves.Count; i++)
                _cell.CellPiece.AddCaptureMove(CaptureMoves[i]);
        }

        private List<Pos> FilterMoves(List<Pos> PossibleMoves, ref List<Pos> cm, Color mycolor,bool serial)
        {
            List<Pos> ls =new List<Pos>();
            Cell c;

            for (int i = 0; i < PossibleMoves.Count; i++)                                                           
            {
                c = GetCell(PossibleMoves[i].File, PossibleMoves[i].Rank);
                if (c.CellPiece is object)
                {
                    if (c.CellPiece.PieceColor == mycolor)
                    {
                        if (serial)
                            break;
                    }
                    else 
                    {
                        ls.Add(PossibleMoves[i]);
                        cm.Add(PossibleMoves[i]);
                        if (serial)
                            break;
                    }
                }
                else
                    ls.Add(PossibleMoves[i]);
            }

            return ls;

        }

        private List<Pos> FilterCaptureMoves(List<Pos> PossibleMoves, ref List<Pos> cm, Color mycolor)
        {
            List<Pos> ls = new List<Pos>();
            Cell c;
            enPassant en = enPassant.Instance;

            for (int i = 0; i < PossibleMoves.Count; i++)
            {
                c = GetCell(PossibleMoves[i].File, PossibleMoves[i].Rank);
                if (c.CellPiece is object )
                {
                    
                        ls.Add(PossibleMoves[i]);
                        cm.Add(PossibleMoves[i]);

                }
                else if (en.IsAvailable)
                {
                    if(PossibleMoves[i].Equals(en.enPassantPos))
                    {
                        ls.Add(PossibleMoves[i]);
                        cm.Add(PossibleMoves[i]);
                    }
                }
               
            }

            return ls;

        }

        private bool Castling(Color c)
        {
            Cell KingCell;
          
            Color ec;
            if (c == Color.White)
                ec = Color.Black;
            else
                ec = Color.White;

            King K = new King(c);
            Rook r = new Rook(c);
            List<Cell> ls= FindPiece(K);
     
            if (ls.Count > 0)
            {
                KingCell = ls[0];
                 MyBoard[KingCell.Position.Rank-1][Files.IndexOf(KingCell.Position.File)].CellPiece.KSCastle = false;
                MyBoard[KingCell.Position.Rank-1][Files.IndexOf(KingCell.Position.File)].CellPiece.QSCastle = false;


                if (!KingCell.CellPiece.IsMoved)
                { 
                    ls = FindPiece(r);
                    for (int i=0;i<ls.Count;i++)
                    {
                        if(!ls[i].CellPiece.IsMoved)
                        {
                            if(Math.Abs(ls[i].Position.File-KingCell.Position.File)<4) //KingSideCastling
                            {
                               
                                if((MyBoard[KingCell.Position.Rank-1][Files.IndexOf('f')].CellStage==CellStage.NonOccupied)&&
                                    (MyBoard[KingCell.Position.Rank-1][Files.IndexOf('g')].CellStage == CellStage.NonOccupied)&&
                                    (PiecesInPath(KingCell,ec).Count==0) &&
                                    (PiecesInPath(MyBoard[KingCell.Position.Rank-1][Files.IndexOf('f')], ec).Count == 0) &&
                                    (PiecesInPath(MyBoard[KingCell.Position.Rank-1][Files.IndexOf('g')], ec).Count == 0))
                                {
                                    MyBoard[KingCell.Position.Rank-1][Files.IndexOf('e')].CellPiece.KSCastle = true;
                           
                                }

                            }
                            else
                            {
                                if ((MyBoard[KingCell.Position.Rank-1][Files.IndexOf('d')].CellStage == CellStage.NonOccupied) &&
                                  (MyBoard[KingCell.Position.Rank-1][Files.IndexOf('c')].CellStage == CellStage.NonOccupied) &&
                                  (MyBoard[KingCell.Position.Rank -1][Files.IndexOf('b')].CellStage == CellStage.NonOccupied) &&
                                  (PiecesInPath(KingCell, ec).Count == 0) &&
                                  (PiecesInPath(MyBoard[KingCell.Position.Rank-1][Files.IndexOf('d')], ec).Count == 0) &&
                                  (PiecesInPath(MyBoard[KingCell.Position.Rank -1][Files.IndexOf('c')], ec).Count == 0))
                                {
                                    MyBoard[KingCell.Position.Rank-1][Files.IndexOf('e')].CellPiece.QSCastle = true;

                                }
                            }
                        }
                    }
                }

        }

            return false;
        }

        public List<Piece> PiecesInPath(Cell cl, Color cr)
        {
            List<Piece> ls = new List<Piece>();
            Piece p;
            //bool bOnlyCaptures ;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //bOnlyCaptures = false;
                    p = MyBoard[i][j].CellPiece;
                    if ( p is object && p.PieceColor==cr)
                    {
                        //Pawn pn = new Pawn(cr);
                        //if (p.GetType() == pn.GetType()) bOnlyCaptures = true;

                        if (p.IsPosInMoves(cl.Position))
                            ls.Add(p);
                    }
                }
            }


            return ls;
        }

        public List<Cell> FindPiece(Piece p)
        {
            List<Cell> ls = new List<Cell>();
            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if( MyBoard[i][j].HasPiece(p))                  
                        ls.Add(MyBoard[i][j]);
                }
            }

            return ls;
        }
        public Cell GetCell(char f, int rank)
        {
            int file = Files.IndexOf(f);
            return MyBoard[rank-1][file];
        }

        public CellStage GetCellStage(char f,int rank)
        {
            int file = Files.IndexOf(f);
            return MyBoard[rank - 1][file].CellStage;
        }
        public void Display()
        {
            GameState GS= GameState.Instance;
            MovementIndicator In = MovementIndicator.Instance;
            for (int i=7; i >-1; i--)
            {
                
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(MyBoard[i][j].Symbol()+' ');
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
            Console.WriteLine("Move: {0}  WM:{1}   BM:{2}   WV:{3}   BV:{4}", 
                               In.GetIndex.No.ToString(), GS.WhiteMobility,   GS.BlackMobility,     GS.WhiteValue,      GS.BlackValue);
            Console.WriteLine("");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public object Clone()
        {
            Board B = (Board)this.MemberwiseClone();// Returns a shallow copy of the current object.
            for(int i=0;i < this.WhitePieces.Count;i++)
            {
                B.WhitePieces[i] = (Piece)this.WhitePieces[i].Clone();
            }
            for (int i = 0; i < this.BlackPieces.Count; i++)
            {
                B.BlackPieces[i] = (Piece)this.BlackPieces[i].Clone();
            }
            B.MyBoard =new  List<List<Cell>>();
            B.InitBoard();
            for (int i=0;i<this.MyBoard.Count;i++)
            {
                for (int j = 0; j < this.MyBoard[i].Count; j++)
                {
                    B.MyBoard[i][j] = (Cell)this.MyBoard[i][j].Clone();
                }

            }
            return B; //returns Deep copy
        }
        public List<List<Cell>> GetBoard()
        {
            return MyBoard;
        }
    }
}
