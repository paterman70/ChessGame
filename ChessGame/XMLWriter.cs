using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChessGame
{
    public class XMLWriter
    {
      
        List<List<Cell>> MyBoard;
        ScoreSheet MyScoreSheet;

        public XMLWriter()
        {
          
        }
        public ScoreSheet SSheet  // property
        {
           
            set { MyScoreSheet = value; }   // set method
        }
        private string GetOutputPath()
        {
            string currentPath = Directory.GetCurrentDirectory();
            currentPath = Path.Combine(currentPath, "output");
            if (!Directory.Exists(currentPath))
                Directory.CreateDirectory(currentPath);
            return currentPath;
        }
        public void Write(List<List<Cell>> B)
        {
            MyBoard = B;
         
            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.UTF8;
            MovementIndicator mind = MovementIndicator.Instance;
            string move = mind.GetIndex.ToString() + ".xml";
            string path = GetOutputPath();
            path = Path.Combine(path, move);
            Directory.CreateDirectory(path);
            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                // Opens the document
                writer.WriteStartDocument();


                // Write the root element
                writer.WriteStartElement("chessMove"+ mind.GetIndex.ToString());

              

                // Write a files
                
                Piece _piece;
             
                string str;
                for (int i = 0; i < MyBoard.Count; i++)
                {
                    writer.WriteStartElement("row", i.ToString()); //start row
                    for (int j = 0; j < MyBoard[i].Count; j++)
                    {
                        if (MyBoard[i][j].HasPiece())
                        {
                            _piece = MyBoard[i][j].CellPiece;

                            str = _piece.PieceColor.ToString()+"-"+_piece.PieceName;

                            writer.WriteStartElement("piece", str); //start row
                            writer.WriteEndElement();
                            
                            str = "X=" +j.ToString();
                            writer.WriteStartElement("file", str); //start row
                            writer.WriteEndElement();
                            str = "Y=" + i.ToString();
                            writer.WriteStartElement("rank", str); //start row
                            writer.WriteEndElement();

                            //ls = _piece.GetMovesList();
                            //writer.WriteStartElement("possible_moves"); //start possible moves
                            //for (int k = 0; k < ls.Count; k++)
                            //{
                            //    str = _piece.Symbol + ls[k].File.ToString() + ls[k].Rank.ToString();
                            //    writer.WriteStartElement("possible_move", str); //start possible moves
                            //    writer.WriteEndElement();
                            //}
                            //writer.WriteEndElement();
                        }
                        //else
                        //{
                        //    str = MyBoard[i][j].Position.File.ToString() + MyBoard[i][j].Position.Rank.ToString();

                        //    writer.WriteStartElement("cell", str); //start row
                        //    writer.WriteEndElement();
                        //}

                    }
                    writer.WriteEndElement(); // End row
                }
                writer.WriteEndElement(); // End chessmove



                // End the document
                writer.WriteEndDocument();

            }
        }
        public void Write(ScoreSheet s)
        {
          
            MyScoreSheet = s;
            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.UTF8;
         
            string path = GetOutputPath();
            path = Path.Combine(path, "metadata.xml");
            Directory.CreateDirectory(path);
            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                // Opens the document
                writer.WriteStartDocument();

                // Write metadata
                writer.WriteStartElement("metadata");
                writer.WriteElementString("playerWhite", MyScoreSheet.WhitePlayer);
                writer.WriteElementString("playerBlack", MyScoreSheet.BlackPlayer);
                writer.WriteElementString("date", DateTime.Now.ToString("yyyy-MM-dd"));
                writer.WriteElementString("Turn", MyScoreSheet.NumberOfMoves().ToString());

                writer.WriteEndElement(); // End metadata
                                      
                writer.WriteEndDocument();

            }
        }
        public void Write(List<Board> Game)
        {
           
            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.Encoding = System.Text.Encoding.UTF8;
            string move = "\\Game.xml";
            string path = GetOutputPath();
        
            Directory.CreateDirectory(path);


            using (XmlWriter writer = XmlWriter.Create(path+move, settings))
            {
                // Opens the document
                writer.WriteStartDocument();


                // Write the root element
                writer.WriteStartElement("chessGame");

                // Write metadata
                writer.WriteStartElement("metadata");
                writer.WriteElementString("playerWhite", MyScoreSheet.WhitePlayer);
                writer.WriteElementString("playerBlack", MyScoreSheet.BlackPlayer);
                writer.WriteElementString("date", DateTime.Now.ToString("yyyy-MM-dd"));
                writer.WriteElementString("Turn", MyScoreSheet.NumberOfMoves().ToString());

                writer.WriteEndElement(); // End metadata
                                          // Write moves
               

                // Write a files

                Piece _piece;
              
                string str;
                for (int k = 0; k < Game.Count; k++)
                {
                    if (k == 0) str = "initial_position";
                    else
                    str =(((k+1) % 2==0) ?"White":"Black") +Convert.ToInt32(k / 2+1).ToString() ;
                    MyBoard = Game[k].GetBoard();
                    
                    writer.WriteStartElement(str);//Start move
                    for (int i = 0; i < MyBoard.Count; i++)
                    {
                      
                        for (int j = 0; j < MyBoard[i].Count; j++)
                        {
                            if (MyBoard[i][j].HasPiece())
                            {
                                _piece = MyBoard[i][j].CellPiece;

                                str = _piece.PieceColor.ToString() + "-" + _piece.PieceName+"-"+j.ToString()+ i.ToString();

                                writer.WriteStartElement("piece", str); //start row
                                writer.WriteEndElement();


                                //ls = _piece.GetMovesList();
                                //writer.WriteStartElement("possible_moves"); //start possible moves
                                //for (int k = 0; k < ls.Count; k++)
                                //{
                                //    str = _piece.Symbol + ls[k].File.ToString() + ls[k].Rank.ToString();
                                //    writer.WriteStartElement("possible_move", str); //start possible moves
                                //    writer.WriteEndElement();
                                //}
                                //writer.WriteEndElement();
                            }
                            //else
                            //{
                            //    str = MyBoard[i][j].Position.File.ToString() + MyBoard[i][j].Position.Rank.ToString();

                            //    writer.WriteStartElement("cell", str); //start row
                            //    writer.WriteEndElement();
                            //}

                        }
                      
                    }
                    writer.WriteEndElement(); // End Move
                }
                writer.WriteEndElement(); // End Game

                // End the document
                writer.WriteEndDocument();

            }
        }
    }
}
