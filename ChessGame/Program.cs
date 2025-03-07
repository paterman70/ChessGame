using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Program
    {
       
        static void Main(string[] args)
        {   
            ApplicationController app = new ApplicationController();
            string str="Start ";
            for (int i = 0; i < args.Length; i++)
                str += " " + args[i];
            Logger.LogState(str);
           
            if (args.Length>0)
            {
                switch (args[0])
                {

                    case "Next":
                        app.NextMove();
                        break;
                    case "Initialization":
                        app.Initial();
                        break;
                    case "Previous":
                        app.PreviousMove();
                        break;
                    case "GoToStart":
                        app.GoToStart();
                        break;
                    case "GoToEnd":
                        app.GoToEnd();
                        break;
                    case "Play":
                        app.Play();
                        break;
                    case "Load":
                        if (args.Length > 1)
                        { 
                                app.Load(args[1]);
                            Logger.LogState("Program Load");
                        }
                        break;


                    default:
                        break;
                }
            }
          
            
        }
    }
}
