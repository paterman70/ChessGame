using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{

  
    public class MovementIndicator : IEquatable<Indicator>
    {
        private static MovementIndicator _instance;
        private static readonly object _lock = new object();

        private Indicator Ind;

        public Indicator GetIndex
        {
            get { return Ind; }   // get method
        }
        public void SetIndex(Indicator i)
        {
            Ind.Plays = i.Plays;
            Ind.No = i.No;
        }
        private MovementIndicator()
        {
            Ind = new Indicator(0,Turn.White);
           
        }
        public void NextMove()
        {
            switch (Ind.Plays)
            {
                case Turn.White:
                    Ind.Plays = Turn.Black;
                    Ind.No++;
                    break;
                case Turn.Black:
                    Ind.Plays = Turn.White;
                    break;
                default:
                    break;
            }
        }

        public void PrevMove()
        {
            switch (Ind.Plays)
            {
                case Turn.White:
                    Ind.Plays = Turn.Black;
                    if (Ind.No>1) Ind.No--;
                    break;
                case Turn.Black:
                    Ind.Plays = Turn.White;
                    break;
                default:
                    break;
            }
        }
        public static MovementIndicator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MovementIndicator();
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
        public bool Equals(Indicator other)
        {
            if (other == null) return false;
            return ((Ind.No == other.No) && (Ind.Plays== other.Plays));
        }
    }
}
