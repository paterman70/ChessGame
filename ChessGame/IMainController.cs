using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    interface IMainController
    {
        bool Verbose { get; set; }
        bool XMLOutput { get; set; }

        void DisplayBoard();
        void DisplayPlayerMoves();
        void Load(string filePath);
        void Load(StringBuilder sb);
        List<Board> GetTheGame();
        ScoreSheet GetScoreSheet();
    }
}
