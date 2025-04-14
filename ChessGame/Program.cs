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
#if DEBUG
             string str ="Start ";
            for (int i = 0; i < args.Length; i++)
                str += " " + args[i];
            Logger.LogState(str);
#endif
           
           
            if (args.Length>0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {

                        case "-N":
                            app.NextMove();
                            break;
                        case "-I":
                            app.Initial();
                            break;
                        case "-P":
                            app.PreviousMove();
                            break;
                        case "-S":
                            app.GoToStart();
                            break;
                        case "-E":
                            app.GoToEnd();
                            break;
                        case "-PL":
                            app.Play();
                            break;
                        case "-L":
                            if (args.Length > 1)
                            {
                                app.Load(args[++i]);
                                Logger.LogState("Program Load");
                            }
                            break;
                        case "-V":
                            app.SetVerbose(true);
                            break;
                        case "-X":
                            app.SetXMLOutput(true);
                            break;
#if DEBUG
                        default:
                            app.Load("test1.pgn");
                            break;
#endif
                    }
                }
            }
#if DEBUG
            else //for test purpose
                app.Load("test1.pgn");
#endif

        }
    }
}
