using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace Parser
{
    internal class Program
    {

        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder("///// Console Parser in C# 2024 by Barbi //////////\n");
            sb.Append("____________________________________________________ \n");
            sb.Append('\n');
            string filepath = @"C:\nolr-work-log-2023.txt";
            bool isFound = (File.Exists(filepath));
            if (isFound)
            {
                sb.Append("   File has been found and is being parsed! \n");
                sb.Append("____________________________________________________ \n");
                sb.Append('\n');
            }
            else { throw new FileNotFoundException("File is not fond."); };
            StreamReader reader = File.OpenText(filepath);

            string redak = null;
            string userstory = string.Empty;
            string datumPLUSus = string.Empty;
            string naDatum = string.Empty;
            int day = 0;
            int month = 0;
            int i = 0;
            List<string> listaUS = new List<string>();

            var brojredaka = File.ReadAllLines(filepath).Length;
            do
            {
                redak = reader.ReadLine();
                i++;
                if (redak == null)
                {
                    if (listaUS.Count() == 1)
                    {
                        datumPLUSus = String.Join("", listaUS).ToString();
                        sb.Append(datumPLUSus + " " + "-there are no entries on the specified date ?! \n");
                        //Console.WriteLine(datumPLUSus + " " + "-there are no entries on the specified date ?!");
                    }
                    if (listaUS.Count() >= 2)
                    {
                        datumPLUSus = String.Join("", listaUS).ToString();
                        sb.Append(datumPLUSus + '\n');
                        //Console.WriteLine(datumPLUSus);
                    }
                    continue;
                }
                if (redak == "" || redak == "\t" || redak.StartsWith("\t"))
                {
                    continue;
                }
                string[] rastavljeniredak = redak.Split(" ");

                if (rastavljeniredak.Length >= 2 && int.TryParse(rastavljeniredak[0], out day) && int.TryParse(rastavljeniredak[1], out month))
                {
                    if (listaUS.Count() == 1)
                    {
                        datumPLUSus = String.Join("", listaUS).ToString();
                        sb.Append(datumPLUSus + " " + "-there are no entries on the specified date ?! \n");
                        //Console.WriteLine(datumPLUSus + " " + "-there are no entries on the specified date ?!");
                        listaUS = new List<string>();
                    }
                    if (listaUS.Count() >= 2)
                    {
                        datumPLUSus = String.Join("", listaUS).ToString();
                        sb.Append(datumPLUSus + '\n');
                        //Console.WriteLine(datumPLUSus);
                    }

                    naDatum = (rastavljeniredak[0].ToString() + " " + rastavljeniredak[1].ToString() + " " + "2023" + " ");
                    listaUS = new List<string>();
                    listaUS.Add(naDatum);
                }

                if (rastavljeniredak.Length >= 2 &&
                    (rastavljeniredak[0].Equals("EH")
                    || rastavljeniredak[0].Equals("MH")
                    || rastavljeniredak[0].Equals("WZ")
                    || rastavljeniredak[0].Equals("IP")
                    || rastavljeniredak[0].Equals("FX3")
                    || rastavljeniredak[0].Equals("MHRZ")))
                {

                    if (rastavljeniredak[1].StartsWith("US")
                        || rastavljeniredak[1].StartsWith("BUG")
                        || rastavljeniredak[1].StartsWith("TK"))
                    {
                        userstory = (rastavljeniredak[1] + " ");
                        if (rastavljeniredak[1].Length <= 3)
                        {
                            userstory = "|" + rastavljeniredak[1] + "-invalid value from input type|";
                        }
                        listaUS.Add(userstory);
                    }
                }
            } while (i < (brojredaka + 1));
            sb.Append("__________________________________________________________________________ \n");
            sb.Append('\n');
            sb.Append("   The file has been parsed! \n");
            sb.Append("__________________________________________________________________________  \n");
            sb.Append('\n');
            Console.WriteLine(sb.ToString());
            File.WriteAllText(@"C:\Parser202x.txt", sb.ToString());
        }
    }
}
