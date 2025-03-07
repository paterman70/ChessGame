using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    interface IBoard
    {
       void UpdateMoves();
        List<Piece> GetPieces(Color c);
        List<Piece> PiecesInPath(Cell cl, Color cr);
        List<Cell> FindPiece(Piece p);
        CellStage GetCellStage(char f, int rank);
        void Display();
    }
   
}
