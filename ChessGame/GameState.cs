using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{

   public  enum GState
    {
        WhiteWon,
        BlackWon,
        Draw,
        InProgress
    }

    public class GameState
    {
        private static GameState _instance;
        private static readonly object _lock = new object();

        private GState st=GState.InProgress;
        private int WMobility=0;
        private int BMobility=0;
        private int WValue=0;
        private int BValue=0;

        public static GameState Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new GameState();
                        }
                    }
                }
                return _instance;
            }
        }

        public GState State   // property
        {
            get { return st; }   // get method
            set { st = value; }   // set method
        }
      
        public int WhiteMobility   // property
        {
            get { return WMobility; }   // get method
        }
        public int BlackMobility   // property
        {
            get { return BMobility; }   // get method
        }
        public int WhiteValue   // property
        {
            get { return WValue; }   // get method
        }
        public int BlackValue   // property
        {
            get { return BValue; }   // get method
        }

        public void Update(List<Piece> B)
        {
            
            Piece p;
            WMobility = 0;
            BMobility = 0;
            WValue = 0;
            BValue = 0;
            for (int i = 0; i < B.Count; i++)
            {             
                p = B[i];
                if (p is object)
                {
                    if (p.PieceColor ==Color.White )
                    {
                        WMobility += p.Mobility();
                        WValue += p.Value;
                    }
                    else
                    {
                        BMobility += p.Mobility();
                        BValue += p.Value;
                    }

                }

            }
           
        }
    }
}
