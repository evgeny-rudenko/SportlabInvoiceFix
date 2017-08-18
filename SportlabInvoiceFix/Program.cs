using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SportlabInvoiceFix
{
    class Program
    {
        static void Main(string[] args)
        {
            String path = Directory.GetCurrentDirectory();
            String DestinationFile;
            string DocumentNumber;
            string s;
            foreach (string file in Directory.EnumerateFiles(path, "*.txt", SearchOption.AllDirectories))
            {
                Console.WriteLine(file);
                DestinationFile = file.Replace(".txt", ".sst");
                DocumentNumber = Path.GetFileName(file).Replace(".txt","");
                StringBuilder sb = new StringBuilder();
                StreamReader sr1 = new StreamReader(file,Encoding.GetEncoding(1251));//  File.OpenText(file);

                if (File.Exists(DestinationFile))
                    {
                    File.Delete(DestinationFile);
                    }

                string HeadeLine = "!documentnumber!;!documentdate!;0;ПОСТАВКА;;;РУБЛЬ;1;;;;;;;Аптека 27 ООО; ; ; ";
                HeadeLine =  HeadeLine.Replace("!documentnumber!",DocumentNumber);
                HeadeLine =  HeadeLine.Replace("!documentdate!",DateTime.Today.ToString("dd.MM.yyyy"));
                sb.AppendLine("[Header]");
                sb.AppendLine(HeadeLine);
                sb.AppendLine("[Body]");

                while ((s = sr1.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                    sb.AppendLine(s);
                }

                sr1.Close();
                Console.WriteLine();
                StreamWriter sw1 =new StreamWriter(DestinationFile, true,Encoding.GetEncoding(1251)) ; //File.AppendText(DestinationFile);
                sw1.Write(sb);
                sw1.Close();

            }



        }
        
      
    }
}
