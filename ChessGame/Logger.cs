using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Logger
    {
        public static void LogException(Exception ex)
        {
            try
            {

                using (StreamWriter sr = File.AppendText("log.txt"))
                {

                    sr.WriteLine("=>" + DateTime.Now + " " + " An Error occurred: " + ex.StackTrace +
                        " Message: " + ex.Message + "\n\n");
                    sr.Flush();
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void LogState(string str)
        {
            try
            {

                using (StreamWriter sr = File.AppendText("log.txt"))
                {

                    sr.WriteLine("=>" + DateTime.Now + " " + " State: " + str + "\n\n");
                    sr.Flush();
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
