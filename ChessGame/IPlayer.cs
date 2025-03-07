using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    interface IPlayer
    {
        List<Piece> GetNonCapturedPieces();

        Board SelectMove();
        List<Pos> GetValidMoves();
        void SetEnemyPieces(List<Piece> ePieces);
        int GetPiecesValue();
        int GetMobility();
       
         bool IsResinged();
    }
}
