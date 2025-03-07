using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class ApplicationController
    {

		private readonly IMainController mainController;
        public void SetVerbose(bool v)   // property
        {
            mainController.Verbose = v;
        }
        public void SetXMLOutput(bool x)   // property
        {
            mainController.XMLOutput = x;
         
        }
        public ApplicationController()
		{

			this.mainController = new MainController();

		}
		public void DisplayBoard()
        {
			this.mainController.DisplayPlayerMoves();
			this.mainController.DisplayBoard();

        }

        public void NextMove()
        {

        }
        public void Initial()
        {

        }
        public void PreviousMove()
        {

        }
        public void GoToStart()
        {

        }
        public void GoToEnd()
        {

        }
        public void Play()
        {

        }
        public void Load(string filePath)
        {
            mainController.Load(filePath);
        }

        public void Load(StringBuilder sb)
        {
            mainController.Load(sb);
        }

        public List<Board> GetTheGame()
        {
            return mainController.GetTheGame();
        }
    }
}
