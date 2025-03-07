using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public enum Turn
    {
        White,
        Black
    }
  
    public class Indicator 
    {
        public Indicator(int n,Turn p)
        {
            N = n;
            P = p;
        }
    private int  N;
    private Turn P;
        public int No   // property
        {
            get { return N; }   // get method
            set { N = value; }   // set method
        }
        public Turn Plays   // property
        {
            get { return P; }   // get method
            set { P= value; }   // set method
        }

        public override string ToString()
        {
            return (N.ToString()+P.ToString());
        }
    }
}
