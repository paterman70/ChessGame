﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{


   public  class MainController : IMainController
    {
        private List<Board> Game;
        private Player WhitePlayer;
        private Player BlackPlayer;
        private MovementIndicator MoveIndicator;
        private XMLWriter Writer;
        private ScoreSheet MySheet;
        private GameState MyGameState;
        private bool bverbose = false;
        private bool bxmloutput = false;

        public MainController()
        {

            Board B = new Board();
            Game = new List<Board>();
            Game.Add(B);
            MoveIndicator = MovementIndicator.Instance;
            MySheet =ScoreSheet.Instance;
            Color c = Color.White;
            WhitePlayer = new Player(c, Game[Game.Count - 1], MySheet);

            c = Color.Black;
            BlackPlayer = new Player(c, Game[Game.Count - 1], MySheet);
            Writer = new XMLWriter();

            MyGameState = GameState.Instance;
            MyGameState.Update(B.GetPieces());
        }

        public bool Verbose   // property
        {
            get { return bverbose; }   // get method
            set { bverbose = value; }   // set method
        }
        public bool XMLOutput   // property
        {
            get { return bxmloutput; }   // get method
            set { bxmloutput = value; }   // set method
        }
        public GameState Play()
        {

            while (MyGameState.State == GState.InProgress)
            {
                if (!WhitePlayer.IsResinged())
                {
                    Game.Add(WhitePlayer.SelectMove());
                    MoveIndicator.NextMove();

                }
                else
                    MyGameState.State = GState.BlackWon;

                if (!BlackPlayer.IsResinged())
                {
                    Game.Add(BlackPlayer.SelectMove());
                    MoveIndicator.NextMove();

                }
                else
                    MyGameState.State = GState.WhiteWon;

            }
            return MyGameState;
        }


        public void DisplayBoard()
        {
            if (bverbose)
            {
                if (Game.Count > 0)
                    Game[Game.Count - 1].Display();
            }
        }
        public void DisplayPlayerMoves()
        {
            WhitePlayer.DisplayValidMoves();
            BlackPlayer.DisplayValidMoves();

        }

        public void Load(StringBuilder sb)
        {
            ScoreSheet.Reset();
            enPassant.Reset();
            GameState.Reset();
            MovementIndicator.Reset();
            var pgnReader = new PgnReader();
            MySheet = pgnReader.GetScoreSheet(sb);
            int NumMoves = MySheet.NumberOfMoves();
            MySheet.Update(Game[Game.Count() - 1].GetPieces());
            Color c;
            Board B;
            for (int i = 0; i < NumMoves; i++)
            {
                c = Color.White;
                B = SetMove(c, i);

                if (B is object)
                {
                    Game.Add(B);
                    if (bverbose)
                    Game[Game.Count - 1].Display();
                }

                c = Color.Black;
                B = SetMove(c, i);

                if (B is object)
                {
                    Game.Add(B);
                    if (bverbose)
                        Game[Game.Count - 1].Display();
                }
            }
            if (bxmloutput)
            {
                Writer.SSheet = MySheet;
                Writer.Write(Game);
            }
        }

        public void Load(string filePath)
        {
            var pgnReader = new PgnReader();
            MySheet = pgnReader.GetScoreSheet(filePath);
            int NumMoves = MySheet.NumberOfMoves();
            MySheet.Update(Game[Game.Count()-1].GetPieces());
            Color c;
            Board B;
            for (int i = 0; i < NumMoves; i++)
            {
                c = Color.White;
                B = SetMove(c, i);

                if (B is object)
                {
                    Game.Add(B);
                    if (bverbose)
                        Game[Game.Count - 1].Display();
                }

                c = Color.Black;
                B = SetMove(c, i);

                if (B is object)
                {
                    Game.Add(B);
                    if (bverbose)
                        Game[Game.Count - 1].Display();
                }
            }
            if (bxmloutput)
            {
                Writer.SSheet = MySheet;
                Writer.Write(Game);
            }
        }


        private Board SetMove(Color c, int i)
        {
            Piece p;
            Board B = null;
            Move m;
            enPassant enPas = enPassant.Instance;
            B = (Board)Game[Game.Count - 1].Clone();
            m = MySheet.GetMove(c, i);
         
            if (m is object)
            {

                if (m.Castle is null)
                {

                    p = B.PieceOfMove(m);
                    if (p is object)
                    {
                        Pos ps;                 
                        Cell newcell = B.GetCell(m.Position.File, m.Position.Rank);
                        ps=p.cell.MovePieceTo(newcell);
                        enPas.UpdateState(p, m, B);

                         if (ps.Rank >0)
                        {
                            Cell enPassantCapturedCell = B.GetCell(ps.File, ps.Rank);
                            if (enPassantCapturedCell.CellPiece is object)
                            {
                                enPassantCapturedCell.SetPiece(null);
                                

                            }

                        }
                        p = newcell.CellPiece;

                        if (enPas.IsAvailable) enPas.DangerPawn=p;

                        B.UpdateMoves(); 
                       MoveIndicator.NextMove();
                        MySheet.Update(B.GetPieces());
                        
                    }
                  
                }
                else
                {
                    List<Cell> ls = B.Castle(c, m.Castle);
                    for (int j = 0; j < ls.Count; j += 2)
                    {
                        ls[j].MovePieceTo(ls[j + 1]);
                    }
                    B.UpdateMoves();
                     MoveIndicator.NextMove();
                    MySheet.Update(B.GetPieces());
                   
                }
            }
            return B;
        }

        public List<Board> GetTheGame()
        {
            return Game;
        }

        public ScoreSheet GetScoreSheet()
        {
            return MySheet;
        }
    }
}
