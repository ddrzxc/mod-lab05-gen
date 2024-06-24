using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace generator
{
    public class CharGenerator 
    {
        private string syms = "абвгдеёжзийклмнопрстуфхцчшщьыъэюя"; 
        private char[] data;
        private int size;
        private Random random = new Random();
        public CharGenerator() 
        {
           size = syms.Length;
           data = syms.ToCharArray(); 
        }
        public char getSym() 
        {
           return data[random.Next(0, size)]; 
        }
    }
    public class BigrGenerator
    {
        private Dictionary<string, int> map;
        private int max;
        public BigrGenerator()
        {
            map = new Dictionary<string, int>();
            max = 0;
        }
        public void Load(string fileName)
        {
            List<string> lines = File.ReadAllLines(fileName).ToList();
            Dictionary<string, int> newmap = new Dictionary<string, int>();
            foreach (string line in lines)
            {
                string[] words = line.Split('\t', StringSplitOptions.TrimEntries);
                max += Convert.ToInt32(words[2]);
                newmap.Add(words[1], max);
            }
            Load(newmap);
        }
        public void Load(Dictionary<string, int> load)
        {
            map = load;
        }
        public string GetSym()
        {
            Random rand = new Random();
            long num = rand.NextInt64(max);
            foreach (var pair in map)
            {
                if (num <= pair.Value) return pair.Key;
            }
            return map.Keys.ToList().Last();
        }
        public string GetString(int count)
        {
            string res = "";
            for (int i = 0; i < count; i++)
            {
                res += GetSym();
            }
            return res;
        }
    }
    public class WordGenerator
    {
        private Dictionary<string, int> map;
        private int max;
        public WordGenerator()
        {
            map = new Dictionary<string, int>();
            max = 0;
        }
        public void Load(string fileName)
        {
            List<string> lines = File.ReadAllLines(fileName).ToList();
            Dictionary<string, int> newmap = new Dictionary<string, int>();
            foreach (string line in lines)
            {
                string[] words = line.Split('\t', StringSplitOptions.RemoveEmptyEntries);
                max += Convert.ToInt32(words[2]);
                newmap.Add(words[1], max);
            }
            Load(newmap);
        }
        public void Load(Dictionary<string, int> load)
        {
            map = load;
        }
        public string GetWord()
        {
            Random rand = new Random();
            long num = rand.NextInt64(max);
            foreach (var pair in map)
            {
                if (num <= pair.Value) return pair.Key;
            }
            return map.Keys.ToList().Last();
        }
        public string GetString(int count)
        {
            string res = "";
            for (int i = 0; i < count; i++)
            {
                res += GetWord() + " ";
            }
            return res;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CharGenerator gen = new CharGenerator();
            SortedDictionary<char, int> stat = new SortedDictionary<char, int>();
            for(int i = 0; i < 1000; i++) 
            {
               char ch = gen.getSym(); 
               if (stat.ContainsKey(ch))
                  stat[ch]++;
               else
                  stat.Add(ch, 1); Console.Write(ch);
            }
            Console.Write('\n');
            foreach (KeyValuePair<char, int> entry in stat) 
            {
                 Console.WriteLine("{0} - {1}",entry.Key,entry.Value/1000.0); 
            }

            BigrGenerator bgen = new BigrGenerator();
            bgen.Load("bigrams.txt");
            Console.WriteLine(bgen.GetString(10));
            File.WriteAllText("gen-1.txt", bgen.GetString(1000));

            WordGenerator wgen = new WordGenerator();
            wgen.Load("words.txt");
            Console.WriteLine(wgen.GetString(10));
            File.WriteAllText("gen-2.txt", wgen.GetString(1000));
        }
    }
}

