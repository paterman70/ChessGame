using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Move 
    {
        public string PieceName { get; set; }
        public Pos Position { get; set; }
        public string Annotation { get; set; }
        public string Capture { get; set; }
        public string Action { get; set; }
        public string Promotion { get; set; }
        public string EnPassant { get; set; }
        public string Check { get; set; }
        public string Castle { get; set; }
        public string Disambiguation { get; set; }
        public Color color { get; set; }
       
        //https://www.geeksforgeeks.org/c-sharp-inheritance-in-constructors/
     
    
    }
}
