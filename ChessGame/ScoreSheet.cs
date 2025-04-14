using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ScoreSheet
    {
        List<Move> Sheet= new List<Move>();
        string score = "";
        string site = "";
        string Player1 = "";
        string Player2 = "";
        string tournament = "";
        string round = "";
        string timecontrol = "";
        int Player1Elo=0;
        int Player2Elo=0;
      

        DateTime Player1EndingTime;
        DateTime Player2EndingTime;
        private MovementIndicator MoveIndicator =MovementIndicator.Instance;
        private static ScoreSheet _instance;
        private static readonly object _lock = new object();

        private List<int> WMobility = new List<int>();
        private List<int> BMobility = new List<int>();
        private List<int> WValue = new List<int>();
        private List<int> BValue = new List<int>();

        //Singleton pattern
        public static ScoreSheet Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ScoreSheet();
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
        public List<List<string>> GetMovesAnnotation()
        {
            List<List<string>> M = new List<List<string>>();
            List<string> L;
            for (int i=0;i<Sheet.Count;i+=2)
            {
                L = new List<string>();
                L.Add(Sheet[i].Annotation);
                if ((i+1) < Sheet.Count)
                    L.Add(Sheet[i+1].Annotation);
                else
                    L.Add("");
                M.Add(L);
            }
            return M;
        }

        public int NumberOfMoves()
        {
            return (Sheet.Count+1) / 2 + Sheet.Count % 2;
        }
        public List<Move> MoveRange(int from, int to)
        {
            List<Move> ls = new List<Move>();
            if ((from>0)&&(from<=to)&& (to<=NumberOfMoves()))
               {
                for (int i = from - 1; i < to * 2 + 1; i++)
                    if(i<Sheet.Count)
                      ls.Add(Sheet[i]);
               }

            return ls;

        }
        public void AddMove(Move m)
        {
            Sheet.Add(m);
        }

        public Move GetMove(Color c, int n)
        {
            Move m = null;
          if (2 * n < Sheet.Count && c == Color.White)
            {
                m = new Move();
                m = Sheet[2 * n];
            }
            else if (2 * n +1< Sheet.Count)
            {
                m = new Move();
                m = Sheet[2 * n + 1];
            }
          return m;
        }

        public List<int> WhiteMobility()
        {
            return WMobility;
        }
        public List<int> BlackMobility()
        {
            return BMobility;
        }
        public List<int> WhiteValue()
        {
            return WValue;
        }
        public List<int> BlackValue()
        {
            return BValue;
        }

        public int WhiteMobility(Indicator ind)
        {
            return GetValue(WMobility, ind, Turn.White);
        }
        public int BlackMobility(Indicator ind)
        {
            return GetValue(BMobility, ind, Turn.Black);
        }
        public int WhiteValue(Indicator ind)
        {
            return GetValue(WValue, ind, Turn.White);
        }
        public int BlackValue(Indicator ind)
        {
            return GetValue(BValue, ind, Turn.Black);
        }
        public string WhiteFideId { get; set; }
        public string WhitePlayerId { get; set; }
        public string BlackFideId { get; set; }
        public string BlackPlayerId { get; set; }

        public DateTime UTCDate { get; set; }
        public DateTime UTCDateTime { get; set; }

        //Does not work!!! Ind gives the number of move but ls has the double number of move
        private int GetValue(List<int> ls, Indicator ind, Turn t)
        {
            if (ind.Plays == t)
            {
                if (ind.No - 1 < ls.Count)
                    return ls[ind.No - 1];
                else
                    return -1;
            }
            else
            {
                if (ind.No < ls.Count)
                    return ls[ind.No];
                else
                    return -1;
            }
        }
        public void Update(List<Piece> B)
        {
            Piece p;

            try
            {

                WMobility.Add(0);
                  WValue.Add(0);
               BMobility.Add(0);
                  BValue.Add(0);
               

                for (int i = 0; i < B.Count; i++)
                {
                    p = B[i];
                    if (p is object)
                    {
                        if (p.PieceColor == Color.White)
                        {
                            WMobility[WMobility.Count-1] += p.Mobility();
                            WValue[WValue.Count-1] += p.Value;
                        }
                        else
                        {
                            BMobility[BMobility.Count-1] += p.Mobility();
                            BValue[BValue.Count-1] += p.Value;
                        }
                    }
                  
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public string Site   // property
        {
            get { return site; }   // get method
            set { site = value; }   // set method
        }
        public string Score   // property
        {
            get { return score; }   // get method
            set { score = value; }   // set method
        }
        public string WhitePlayer   // property
        {
            get { return Player1; }   // get method
            set { Player1 = value; }   // set method
        }
        public string BlackPlayer   // property
        {
            get { return Player2; }   // get method
            set { Player2 = value; }   // set method
        }
        public string Tournament  // property
        {
            get { return tournament; }   // get method
            set { tournament = value; }   // set method
        }
        public string Round   // property
        {
            get { return round; }   // get method
            set { round = value; }   // set method
        }

        public string TimeControl   // property
        {
            get { return timecontrol; }   // get method
            set { timecontrol = value; }   // set method
        }
        public int WhitePlayerELO   // property
        {
            get { return Player1Elo; }   // get method
            set { Player1Elo = value; }   // set method
        }
        public int BlackPlayerELO   // property
        {
            get { return Player2Elo; }   // get method
            set { Player2Elo = value; }   // set method
        }

       
        public DateTime WhitePlayerTime   // property
        {
            get { return Player1EndingTime; }   // get method
            set { Player1EndingTime = value; }   // set method
        }
        public DateTime BlackPlayerTime   // property
        {
            get { return Player2EndingTime; }   // get method
            set { Player2EndingTime = value; }   // set method
        }
    }
}
