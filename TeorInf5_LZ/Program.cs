using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeorInf5_LZ
{
    class Program
    {
        class LZ
        {
            private Dictionary<string, int> symbolCodePair = new Dictionary<string, int>();
            private Dictionary<int, string> codeSymbolPair = new Dictionary<int, string>();

            private List<string> symbols = new List<string>();
            private List<int> codes = new List<int>();

            private List<int> codedLetters = new List<int>();
            private List<string> decodedLetters = new List<string>();

            public void LZCode(string text, char[] alphabet, int size)
            {
                symbols.Clear();
                codes.Clear();

                for (int i = 0; i < size; ++i)
                {
                    symbolCodePair.Add(Convert.ToString(alphabet[i]), i);
                    symbols.Add(Convert.ToString(alphabet[i]));
                    codes.Add(i);
                }

                bool flag = false;
                string s = "";
                int counter = 0;

                while (counter != text.Length)
                {
                    flag = false;
                    char c = text[counter];
                    for (int j = 0; j < symbols.Count; ++j)
                    {
                        if (s + c == symbols[j])
                        {
                            s += c;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        codedLetters.Add(symbolCodePair[s]);
                        symbolCodePair.Add(s + c, symbolCodePair.Count());
                        symbols.Add(s + c);
                        codes.Add(codes.Count());
                        s = Convert.ToString(c);
                    }
                    ++counter;
                }
                codedLetters.Add(symbolCodePair[s]);
            }

            public void LZDecode(List<int> codedLetters, char[] alphabet, int size)
            {
                symbols.Clear();
                codes.Clear();

                for (int i = 0; i < size; ++i)
                {
                    codeSymbolPair.Add(i, Convert.ToString(alphabet[i]));
                    symbols.Add(Convert.ToString(alphabet[i]));
                    codes.Add(i);
                }

                int counter = 0;
                string s = "";
                bool flag = false;

                while (counter != codedLetters.Count)
                {
                    flag = false;
                    int c = codedLetters[counter];
                    decodedLetters.Add(codeSymbolPair[c]);
                    string str = codeSymbolPair[c];
                    for (int j = 0; j < symbols.Count; ++j)
                    {
                        if (s + str[0] == symbols[j])
                        {
                            s += str;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        codeSymbolPair.Add(codeSymbolPair.Count(), s + str[0]);
                        symbols.Add(s + str[0]);
                        codes.Add(codes.Count());
                        s = str;
                    }
                    ++counter;
                }
            }

            public List<int> GetCodedSymbols()
            {
                return codedLetters;
            }

            public List<string> GetDecodedLetters()
            {
                return decodedLetters;
            }

            public Dictionary<string, int> GetSymbolCodePair()
            {
                return symbolCodePair;
            }

            public Dictionary<int, string> GetCodeSymbolPair()
            {
                return codeSymbolPair;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите сообщение");
            string text = Convert.ToString(Console.ReadLine());
            int textLength = text.Length;
            const int N = 73; // мощность алфавита - буквы и .,!?-""()
            char[] alphabet = new char[N];
            int[] quantityOfSymbols = new int[N];
            int pointer = 0;
            bool flag = false;

            for (int i = 0; i < text.Length; ++i)
            {
                for (int j = 0; j < pointer + 1 && j < N; ++j)
                {
                    if (alphabet[j] != text[i])
                    {
                        flag = true;
                    }
                    else
                    {
                        ++quantityOfSymbols[j];
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    alphabet[pointer] = text[i];
                    ++quantityOfSymbols[pointer];
                    ++pointer;
                    flag = false;
                }
            }

            LZ lz = new LZ();

            lz.LZCode(text, alphabet, pointer);
            List<int> codedLetters = new List<int>();
            codedLetters = lz.GetCodedSymbols();
            Console.WriteLine("\nЗакодированное сообщение");
            for (int i = 0; i < codedLetters.Count; ++i)
            {
                Console.Write(codedLetters[i]);
            }
            Dictionary<string, int> symbolCodePair = new Dictionary<string, int>();
            symbolCodePair = lz.GetSymbolCodePair();
            Console.WriteLine();
            foreach (KeyValuePair <string, int> s in symbolCodePair)
            {
                Console.WriteLine(s.Key + " - " + s.Value);
            }

            lz.LZDecode(codedLetters, alphabet, pointer);
            List<string> decodedLetters = new List<string>();
            decodedLetters = lz.GetDecodedLetters();
            Console.WriteLine("\n\nРазкодированное сообщение");
            for (int i = 0; i < codedLetters.Count; ++i)
            {
                Console.Write(decodedLetters[i]);
            }
            Dictionary<int, string> codeSymbolPair = new Dictionary<int, string>();
            codeSymbolPair = lz.GetCodeSymbolPair();
            Console.WriteLine();
            foreach (KeyValuePair<int, string> s in codeSymbolPair)
            {
                Console.WriteLine(s.Key + " - " + s.Value);
            }

            Console.Read();
        }
    }
}

// abdabccabcdab
// 1 01 10 101 1010 011
// abcdabcd
// abaabcabcdabcd
// 00, 01, 10, 11
// всем всем всем и каждому скажу
// 1110100110111001101