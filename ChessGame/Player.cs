using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Player:IPlayer
    {
        private List<Piece> MyPieces;
        private List<Piece> EnemyPieces;
        private Board MyBoard;
        private Color myColor;
        private ScoreSheet Sheet;
        private List<char> files = new List<char> { 'a', 'b', 'c', 'd','e','f','g','h'};
        private MovementIndicator MoveIndicator;
        private static Random m_rnd = new Random();
        public Player(Color color, Board Br, ScoreSheet Sheet)
        {
            MyPieces = new List<Piece>();
            myColor = color;
            UpdateMyPieces();
            MyBoard = (Board)Br.Clone();
            this.Sheet = Sheet;
            MoveIndicator = MovementIndicator.Instance;
            
        }

      
        public List<Piece> GetCapturedPieces()
        {
            throw new NotImplementedException();
        }

        public int GetMobility()
        {
            throw new NotImplementedException();
        }

        private void UpdateMyPieces()
        {
            if (MyBoard is object)
            {
               
                MyPieces = MyBoard.GetPieces(myColor);
                EnemyPieces = MyBoard.GetPieces(myColor);
            }

        }

        public int GetPiecesValue()
        {
            int value=0;
            for (int i = 0; i < MyPieces.Count; i++)
             value+=MyPieces[i].Value;
            return value;
        }

       
        public void SetEnemyPieces(List<Piece> ePieces)
        {
            EnemyPieces= ePieces;
        }

        public List<Pos> GetValidMoves()
        {
            List<Pos> ValidMoves = new List<Pos>();
            List<Pos> v = new List<Pos>();

            for (int i=0; i<MyPieces.Count;i++)
            {
                v=MyPieces[i].GetMovesList();
                for (int j = 0; j < v.Count; j++)
                    ValidMoves.Add(v[j]);
                v.Clear();
            }

            return ValidMoves;
        }

        public void DisplayValidMoves()
        {
            List<Pos> ValidMoves = GetValidMoves();

            
            for (int i = 0; i <ValidMoves.Count; i++)
            {

                    Console.Write(ValidMoves[i] + ", ");
            }
                Console.WriteLine("");
          
        }

        public List<Piece> GetNonCapturedPieces()
        {
            throw new NotImplementedException();
        }


        public bool IsResinged()
        {
            throw new NotImplementedException();
        }

        public Board SelectMove()
        {
            

            Piece p=MyPieces[m_rnd.Next(MyPieces.Count)];
            List<Pos> LP = p.GetMovesList();
            Pos pos=LP[m_rnd.Next(LP.Count)];

            Cell FromCell=MyBoard.GetCell(p.cell.Position.File,p.cell.Position.Rank);
            Cell ToCell = MyBoard.GetCell(pos.File, pos.Rank);
            FromCell.MovePieceTo(ToCell);


            return MyBoard;
        }
    }
}
