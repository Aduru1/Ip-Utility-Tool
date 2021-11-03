using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{
    class Program
    {
        public static List<string> FileToList(string fileName)
        {
            List<string> lines = new List<string>();
            using (var sr = new StreamReader(fileName))
            {
                while (sr.Peek() >= 0)
                    lines.Add(sr.ReadLine());
            }
            return lines;
        }
        static void ReplaceIpInFiles(List<string> files, List<string> newips ,List <string> oldips)
        {
       
            List<string> fileList = new List<string>();
            
            foreach (string file in files)
            {
                string oldip, newip;
                fileList = FileToList(file);
                for (int j=0;j<oldips.Count;j++)
                {
                    oldip = oldips[j];
                    newip = newips[j];
                    for (int i = 0; i < fileList.Count; i++)
                    {

                        fileList[i] = fileList[i].Replace(oldip, newip);
                    }
                }
                File.WriteAllLines(file, fileList);
            }
            Console.WriteLine("replacement done succesfully ");
        }
        public static List<string> ListToConsole(string folderName , string type)
        {
            Console.WriteLine("listing all files having selected extention");
            Console.WriteLine("==========================================");

            string[] dir = Directory.GetFiles(folderName, type, SearchOption.AllDirectories);
            List<string> files = new List<string>();
            files = dir.ToList();
            for (var i = 0; i < files.Count; i++)
            {
                Console.WriteLine(dir[i]);

            }
            return files;
        }
        static void Main(string[] args)
        { 
            
            string f1 = ConfigurationManager.AppSettings["FolderName"];
            string v1 = ConfigurationManager.AppSettings["oldIp"];
            string v2 = ConfigurationManager.AppSettings["NewIp"];

            string type = ConfigurationManager.AppSettings["FileType"];
            

            List<string> initialList = new List<string>();
            initialList = ListToConsole(f1, type);
            string[] stringarr1 = new string[1];
            string[] stringarr2 = new string[1];

            string[] result = new string[1];
            stringarr1 = v1.Split(new char[] { ',' });
            stringarr2 = v2.Split(new char[] { ',' });

            Console.WriteLine("  OldIp                     NewIp");
            Console.WriteLine("=====================================================");
            for (int i = 0; i < stringarr1.Length; i++)
            {
                Console.WriteLine(stringarr1[i] + "                " + stringarr2[i]);
            }

            List<string> str1 = new List<string>();
            str1 = stringarr1.ToList();
            List<string> str2 = new List<string>();
            str2 = stringarr2.ToList();
            ReplaceIpInFiles(initialList, str2, str1);
            Console.WriteLine("successfully Replaced . ");
            Console.WriteLine("press enter to exit the console App");
            Console.ReadLine();

        }

    }
}
