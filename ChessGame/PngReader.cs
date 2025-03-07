using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ChessGame
{

 public class PgnReader
    {
        public string Event { get; set; }
        public string Site { get; set; }
        public string Date { get; set; }
        public string Round { get; set; }
        public string White { get; set; }
        public string Black { get; set; }
        public string Result { get; set; }
        public string WhiteElo { get; set; }
        public string BlackElo { get; set; }

        public string WhiteFideId { get; set; }
        public string WhitePlayerId { get; set; }
        public string BlackFideId { get; set; }
        public string BlackPlayerId { get; set; }
        public string TimeControl{ get; set; }
        public string Tournament { get; set; }

        public string UTCDate { get; set; }
        public string UTCTime { get; set; }


        public List<string> Moves { get; private set; }

        public PgnReader()
        {
            Moves = new List<string>();
        }

        public List<Board> ReadFromFile(string filePath)
        {
            List<Board> G=new List<Board>();
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            string pgnContent = File.ReadAllText(filePath);
            ParsePgn(pgnContent);
            return G;
        }

        public ScoreSheet GetScoreSheet(string filePath)
        {
          
            if (!File.Exists(filePath))
            {
                Exception e= new FileNotFoundException($"File not found: {filePath}");
                Logger.LogException(e);
            }
            string pgnContent = File.ReadAllText(filePath);
            return FillScoreSheet(pgnContent);
        }
        public ScoreSheet GetScoreSheet(StringBuilder sb)
        {
          
            return FillScoreSheet(sb.ToString());
        }
        private ScoreSheet FillScoreSheet(string pgnContent)
        {
            ScoreSheet MySheet = new ScoreSheet();
            bool bHasClockTime = pgnContent.Contains("%clk");
            // Extract metadata using regex
            var metadataRegex = new Regex(@"\[(?<key>[A-Za-z]+) ""(?<value>[^""]+)""\]");
            var metadataMatches = metadataRegex.Matches(pgnContent);

            foreach (Match match in metadataMatches)
            {
                var key = match.Groups["key"].Value;
                var value = match.Groups["value"].Value;

                switch (key)
                {
                    case "Event": MySheet.Event = value; break;
                    case "Site": MySheet.Site= value; break;
                    case "Date": MySheet.TournamentDate = DateTime.Parse(value); break;
                    case "Round": MySheet.Round = value; break;
                    case "White": MySheet.WhitePlayer = value; break;
                    case "Black": MySheet.BlackPlayer = value; break;
                    case "Result": MySheet.Score = value; break;
                    case "WhiteElo": break;
                    case "BlackElo": break;
                    case "WhiteFideId": break;
                    case "WhitePlayerId": break;
                    case "BlackFideId": break;
                    case "BlackPlayerId": break;
                    case "TimeControl": break;
                    case "Tournament": break;
                    case "UTCDate": break;
                    case "UTCTime": break;

    }
}
            Regex movesRegex;
            int g1, g2;
            // Extract moves
            if (bHasClockTime)
            {
                movesRegex = new Regex(@"\d+\.\s+(\S+)(?:\s+\{[^}]*\})?\s+(\S+)(?:\s+\{[^}]*\})?\s+(\S+)");

                g1 = 1;g2 = 3;
            }
            else
            {
                movesRegex = new Regex(@"\d+\.\s*([a-zA-Z0-9+#=-]+)\s*([a-zA-Z0-9+#=-]*)");
                g1 = 1;g2 = 2;
            }
          

            var movesMatches = movesRegex.Matches(pgnContent);

            foreach (Match match in movesMatches)
            {
                Move m;
                m = ParseMove(match.Groups[g1].Value);
                m.color = Color.White;
                if(m.Position is object || m.Castle.Length>0 ) MySheet.AddMove(m); // White's move
                if (match.Groups[g2].Success && !string.IsNullOrWhiteSpace(match.Groups[g2].Value))
                {
                    m = ParseMove(match.Groups[g2].Value);
                    m.color = Color.Black;
                    if (m.Position is object || m.Castle.Length > 0) MySheet.AddMove(m);
                  
                }
            }
            return MySheet;
        }

        private Move ParseMove(string move)
        {

            Move m = new Move() ;
            string movePattern = @"(?<castle>O-O(?:-O)?)|(?<piece>[KQRBN]?)(?<originFile>[a-h]?)(?<originRank>[1-8]?)(?<capture>x?)(?<destFile>[a-h])(?<destRank>[1-8])(?<promotion>=?[QRBN])?(?<enPassant>\s*e\.p\.)?(?<check>[+#]?)";
            var match = Regex.Match(move, movePattern);

            if (match.Success)
            {
                if (match.Groups["castle"].Success)
                {
                    m.PieceName = match.Groups["castle"].Value;
                    string castleText = m.PieceName == "O-O" ? "KingSideCastle" : m.PieceName == "O-O-O" ? "QueenSideCastle" : ""; 
                    m.Castle = castleText;
                }
                else
                {
                    string piece = match.Groups["piece"].Value;
                    string originFile = match.Groups["originFile"].Value;
                    string originRank = match.Groups["originRank"].Value;
                    string capture = match.Groups["capture"].Value;
                    string destFile = match.Groups["destFile"].Value;
                    string destRank = match.Groups["destRank"].Value;
                    string promotion = match.Groups["promotion"].Value;
                    string enPassant = match.Groups["enPassant"].Value;
                    string check = match.Groups["check"].Value;
                    string castle = match.Groups["castle"].Value;

                    // Replace this with the switch statement above
                    string pieceName;
                    switch (piece)
                    {
                        case "K":
                            pieceName = "King";
                            break;
                        case "Q":
                            pieceName = "Queen";
                            break;
                        case "R":
                            pieceName = "Rook";
                            break;
                        case "B":
                            pieceName = "Bishop";
                            break;
                        case "N":
                            pieceName = "Knight";
                            break;
                        default:
                            pieceName = "Pawn";
                            break;
                    }

                    string disambiguation = !string.IsNullOrEmpty(originFile) || !string.IsNullOrEmpty(originRank)
                        ? $"from {(originFile + originRank)}"
                        : "";

                    string action = capture == "x" ? "captures" : "moves to";
                    string promotionText = !string.IsNullOrEmpty(promotion) ? $" and promotes to {promotion[1]}" : "";
                    string enPassantText = !string.IsNullOrEmpty(enPassant) ? " via en passant" : "";
                    string checkText = check == "+" ? " and puts the opponent in check" : check == "#" ? " and checkmates" : "";
                   
                    m.PieceName = pieceName;
                    m.Disambiguation= disambiguation;
                    Pos p = new Pos(Convert.ToChar(destFile), Int32.Parse(destRank));
                    m.Position = p;
                    m.Promotion = promotionText;
                    m.EnPassant = enPassantText;
                    m.Check = checkText;
                    

                    // Console.WriteLine($"{pieceName}{disambiguation} {action} {destFile}{destRank}{promotionText}{enPassantText}{checkText}");            
                }
            }
            return m;
        }
        private void ParsePgn(string pgnContent)
        {
            // Extract metadata using regex
            var metadataRegex = new Regex(@"\[(?<key>[A-Za-z]+) ""(?<value>[^""]+)""\]");
            var metadataMatches = metadataRegex.Matches(pgnContent);

            foreach (Match match in metadataMatches)
            {
                var key = match.Groups["key"].Value;
                var value = match.Groups["value"].Value;

                switch (key)
                {
                    case "Event": Event = value; break;
                    case "Site": Site = value; break;
                    case "Date": Date = value; break;
                    case "Round": Round = value; break;
                    case "White": White = value; break;
                    case "Black": Black = value; break;
                    case "Result": Result = value; break;
                }
            }

            // Extract moves
            var movesRegex = new Regex(@"\d+\.\s*([a-zA-Z0-9+#=]+)\s*([a-zA-Z0-9+#=]*)");
            var movesMatches = movesRegex.Matches(pgnContent);

            foreach (Match match in movesMatches)
            {
                Moves.Add(match.Groups[1].Value); // White's move
                if (match.Groups[2].Success && !string.IsNullOrWhiteSpace(match.Groups[2].Value))
                {
                    Moves.Add(match.Groups[2].Value); // Black's move
                }
            }
        }

        public void PrintGameInfo()
        {
            Console.WriteLine($"Event: {Event}");
            Console.WriteLine($"Site: {Site}");
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Round: {Round}");
            Console.WriteLine($"White: {White}");
            Console.WriteLine($"Black: {Black}");
            Console.WriteLine($"Result: {Result}");
            Console.WriteLine("Moves:");
            for (int i = 0; i < Moves.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Moves[i]}");
            }
        }
    }

  

}
