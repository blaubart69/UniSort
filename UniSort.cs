using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace UniSort
{
    class UniSort
    {
        static void CheckParams(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("usage: UniSort.exe {Inputfile UTF-8 encoded} {Outputfile, will be UTF-8 with BOM} {Start pos sort column. starting with 1} {length}");
                throw new Exception("bad parameters");
            }
        }

        static void Main(string[] args)
        {
            CheckParams(args);

            using ( TextReader tr = new StreamReader(args[0], true) )
            using ( TextWriter tw = new StreamWriter(args[1], false, Encoding.UTF8) )
            {
                Sort(tr, tw, Convert.ToInt32(args[2]), Convert.ToInt32(args[3]));
            }
            Console.Error.WriteLine("memory used:\t{0}",GC.GetTotalMemory(true));
        }
        static int Sort(TextReader FileIn, TextWriter FileOut, int posStart, int length)
        {
            List<string> list = new List<string>();

            string line;
            while ((line = FileIn.ReadLine()) != null)
            {
                list.Add(line);
            }

            list.Sort((a, b) =>
            {
                string Sub_a = a.Substring(posStart - 1, length);
                string Sub_b = b.Substring(posStart - 1, length);
                return Sub_a.CompareTo(Sub_b);
            });

            Console.Error.WriteLine("sorted lines:\t{0}", list.Count);

            foreach (string s in list)
            {
                FileOut.WriteLine(s);
            }

            return 0;
        }
    }
}
