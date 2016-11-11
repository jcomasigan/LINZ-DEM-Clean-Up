using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LINZ_DEM_Clean_Up
{
    public enum CleanResult
    {
        SUCCESS,
        FAILED
    }

    class CleanDEM
    {
        public static void Clean(string dir, double maxHeight)
        {
            string[] files = System.IO.Directory.GetFiles(dir, "*.asc");
            int fileNo = 0;
            if (files.Count() > 0)
            {
                foreach (string file in files)
                {
                    fileNo++;
                    List<string> updatedLines = new List<string>();
                    int lineCount = 0;
                    StreamReader asc = new StreamReader(file);
                    string line;
                    while ((line = asc.ReadLine()) != null)
                    {
                        if(lineCount > 6)
                        {
                            line = ProcessLine(line, maxHeight);
                        }
                        updatedLines.Add(line + "\r\n");
                        lineCount++;
                    }
                    using (var writer = new StreamWriter(@"\\mesa\users\jmasigan\textelav\" + fileNo.ToString() + ".asc"))
                    {
                        foreach (string updatedLine in updatedLines)
                        {
                            writer.Write(updatedLine);
                        }
                    }
                }


            }
        }

        private static string ProcessLine(string line, double tolerance)
        {
            string[] elav = line.Split(' ');
            double previousElev = 0;
            double currElev = 0;
            double nextElev = 0;
            for (int i = 0; i < (elav.Count() - 1) ; i++)
            {
                if (i > 0)
                {                 
                    double.TryParse(elav[i - 1], out previousElev);
                    double.TryParse(elav[i], out currElev);
                    double.TryParse(elav[i + 1], out nextElev);
                    if (Math.Abs(currElev  - previousElev) < tolerance)
                    {
                        currElev = ((previousElev + nextElev / 2));
                        if (Math.Abs(currElev - previousElev) < tolerance)
                        {
                            currElev = previousElev;
                        }
                        elav[i] = currElev.ToString();
                    }                
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach(string z in elav)
            {
                sb.Append(z).Append(" ");
            }
            return sb.ToString();

        }
    }
}
