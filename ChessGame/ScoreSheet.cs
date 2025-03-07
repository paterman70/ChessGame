using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ScoreSheet
    {
        List<Move> Sheet;
        string score;
        string site;
        string Player1;
        string Player2;
        string tournament;
        string round;
        string timecontrol;
        int Player1Elo;
        int Player2Elo;
        DateTime date;
        DateTime Player1EndingTime;
        DateTime Player2EndingTime;
        private MovementIndicator MoveIndicator;
        

        public ScoreSheet()
        {
            Sheet = new List<Move>();
            score = "";
            Player1 = "";
            Player2 = "";
            tournament = "";
            site = "";
            round = "";
            timecontrol = "";
            Player1Elo = 0;
            Player1Elo = 0;
            MoveIndicator = MovementIndicator.Instance;

        }

        public int NumberOfMoves()
        {
            return Sheet.Count / 2 + Sheet.Count % 2;
        }
        public List<Move> MoveRange(int from, int to)
        {
            List<Move> ls = new List<Move>();
            if ((from>0)&&(from<=to)&& (to<=NumberOfMoves()))
               {
                for (int i = from - 1; i < to * 2 + 1; i++)
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
        public string Event   // property
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

        public DateTime TournamentDate   // property
        {
            get { return date; }   // get method
            set { date = value; }   // set method
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
