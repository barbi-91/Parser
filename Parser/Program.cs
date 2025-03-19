using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Parser
{
    internal class Program
    {

        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder("///// Console App Parser in C# 2024 by Barbi //////////\n");
            sb.Append("____________________________________________________ \n");
            sb.Append('\n');
            // get file path
            string filepath = ConfigurationManager.AppSettings["FilePath"] ?? string.Empty;
            // get year from file path
            string filename = Path.GetFileName(filepath);
            string yearFromFilename = Regex.Match(filename, @"\d{4}").Value;

            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
            {
                Console.WriteLine($"Invalid file path specified in the configuration. File Path: {filepath} ");
                throw new FileNotFoundException($"Invalid file path or file not found: {filepath}");
            }
            sb.Append("   File has been found and is being parsed! \n");
            sb.Append($"   File path: {filepath} \n");
            sb.Append("____________________________________________________ \n");
            sb.Append('\n');
            using (StreamReader reader = File.OpenText(filepath))
            {

                string? fileLine = null;
                string userstory = string.Empty;
                string datePLUSus = string.Empty;
                string specificDate = string.Empty;
                int day = 0;
                int month = 0;
                int i = 0;
                List<string> listaUS = new List<string>();

                var numberOfLines = File.ReadAllLines(filepath).Length;
                do
                {
                    fileLine = reader.ReadLine();
                    i++;
                    if (fileLine == null)
                    {
                        if (listaUS.Count() == 1)
                        {
                            datePLUSus = String.Join("", listaUS);
                            sb.Append(datePLUSus + " " + "-there are no entries on the specified date ?! \n");
                        }
                        if (listaUS.Count() >= 2)
                        {
                            datePLUSus = String.Join("", listaUS);
                            sb.Append(datePLUSus + '\n');
                        }
                        continue;
                    }
                    if (fileLine == "" || fileLine == "\t" || fileLine.StartsWith("\t"))
                    {
                        continue;
                    }
                    string[] splitedLine = fileLine.Split(" ");

                    if (splitedLine.Length >= 2 && int.TryParse(splitedLine[0], out day) && int.TryParse(splitedLine[1], out month))
                    {
                        if (listaUS.Count() == 1)
                        {
                            datePLUSus = String.Join("", listaUS).ToString();
                            sb.Append(datePLUSus + " " + "-there are no entries on the specified date ?! \n");

                            listaUS = new List<string>();
                        }
                        if (listaUS.Count() >= 2)
                        {
                            // Check if list is ok, and add a date with userstory
                            datePLUSus = String.Join("", listaUS).ToString();
                            sb.Append(datePLUSus + '\n');
                        }
                        // In splited line is a new date plus check fore year from file name 
                        //  If the year is successfully extracted from the file name
                        int year = 0;
                        if (int.TryParse(yearFromFilename, out year))
                        {
                            specificDate = (splitedLine[0] + " " + splitedLine[1] + " " + year + " ");
                        }
                        else
                        {
                            specificDate = (splitedLine[0] + " " + splitedLine[1] + " " + DateTime.Today.Year + " ");
                        }
                        // Readed new date from splitedline, clear old list, up in code old list is append, add new date to list
                        listaUS.Clear();
                        listaUS.Add(specificDate);
                    }

                    if (splitedLine.Length >= 2 &&
                        (splitedLine[0].Equals("EH")
                        || splitedLine[0].Equals("MH")
                        || splitedLine[0].Equals("WZ")
                        || splitedLine[0].Equals("IP")
                        || splitedLine[0].Equals("FX3")
                        || splitedLine[0].Equals("MHRZ")
                        || splitedLine[0].Equals("MHWS")
                        ))
                    {

                        if (splitedLine[1].StartsWith("US")
                            || splitedLine[1].StartsWith("BUG")
                            || splitedLine[1].StartsWith("TK"))
                        {
                            userstory = (splitedLine[1] + " ");
                            if (splitedLine[1].Length <= 3)
                            {
                                userstory = "|" + splitedLine[1] + "-invalid value from input type|";
                            }
                            listaUS.Add(userstory);
                        }
                    }
                } while (i < (numberOfLines + 1));
            };
            sb.Append("__________________________________________________________________________ \n");
            sb.Append('\n');
            sb.Append("   The file has been parsed! \n");
            sb.Append("__________________________________________________________________________  \n");
            sb.Append('\n');
            Console.WriteLine(sb.ToString());
            // This code was used to save data to a text file (txt),
            // but it's now commented out as we are saving data to a PDF file instead.
            //File.WriteAllText(@"C:\src-barbi-91\Parser\result.txt", sb.ToString());

            // Here we are calling the method that saves the data to a PDF file
            SaveDataAsPdf(@"C:\src-barbi-91\Parser\result.pdf", sb);



        }
        public static void SaveDataAsPdf(string filePath, StringBuilder sb)
        {
            Document document = new Document(PageSize.A4);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();
                document.Add(new Paragraph(sb.ToString()));
                document.Close();
            }
            SaveDataAsPdf(@"C:\src-barbi-91\Parser\result.pdf", sb);
        }
    }
}
