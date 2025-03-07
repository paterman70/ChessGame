using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Move 
    {
        string pieceName = "";
        string disambiguation = "";
        string action = "";
        string promotionText = "";
        string enPassantText = "";
        string checkText = "";
        string castleText="";
        Color _color;
        string annotation = "";
        Pos position;
        //https://www.geeksforgeeks.org/c-sharp-inheritance-in-constructors/
       

        public string PieceName   // property
        {
            get { return pieceName; }   // get method
            set { pieceName = value; }   // set method
        }
        public Pos Position   // property
        {
            get { return position; }   // get method
            set { position = value; }   // set method
        }
        public string Annotation   // property
        {
            get { return annotation; }   // get method
            set { annotation = value; }   // set method
        }
        public Color color   // property
        {
            get { return _color; }   // get method
            set { _color = value; }   // set method
        }
        public string Action   // property
        {
            get { return action; }   // get method
            set { action = value; }   // set method
        }
        public string Disambiguation   // property
        {
            get { return disambiguation; }   // get method
            set { disambiguation = value; }   // set method
        }
        public string Promotion   // property
        {
            get { return promotionText; }   // get method
            set { promotionText = value; }   // set method
        }
        public string EnPassant   // property
        {
            get { return enPassantText; }   // get method
            set { enPassantText = value; }   // set method
        }
        public string Check   // property
        {
            get { return checkText; }   // get method
            set { checkText = value; }   // set method
        }
        public string Castle   // property
        {
            get { return castleText; }   // get method
            set { castleText = value; }   // set method
        }
    }
}
