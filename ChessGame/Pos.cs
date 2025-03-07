using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Pos:IEquatable<Pos>
    {
        private List<char> Files = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        private char file;
        private int rank;

        public Pos (char f, int r)
        {
            file = f;
            rank = r;
        }

        public Pos()
        {
            file = ' ';
            rank = 0;
        }
        public int Rank   // property
        {
            get { return rank; }   // get method
            set { rank = value; }   // set method
        }
        public char File   // property
        {
            get { return file; }   // get method
            set { file = value; }   // set method
        }

        public int FileIndex   // property
        {
            get { return Files.IndexOf(file); }   // get method
         
        }
        public bool Equals(Pos other)
        {
            if (other == null) return false;
            return ((file==other.File) && (rank==other.Rank));
        }
    }
}
