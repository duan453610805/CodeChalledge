﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Threading;
using System.Reflection;

namespace SEGCodeChallenge
{
    public class CodeChallenge
    {
        public void Week3_1()
        {
            //加一个由‘.’组成得正方形边框，这样就可以不用考虑边界问题
            //将特殊字符替换为一个统一得字符，减少处理次数
            string[] buf, buf1;
            string str = null;
            bool Flag_symbol = false;
            int result = 0;
            buf = File.ReadAllLines("W3_PuzzleInput.txt");
            int symbolNum = 0;
            buf1 = new string[buf.Length + 2];
            for (int line = 0; line < buf.Length; line++)
            {
                string sss = null;
                str += '.';
                sss += '.';
                for (int row = 0; row < buf[line].Length; row++)
                {
                    if (((buf[line][row] < '0') || (buf[line][row] > '9')) && (buf[line][row] != '.'))
                    { str += '*'; }
                    else
                        str += buf[line][row];
                    sss += '.';
                }
                str += '.';
                sss += '.';
                buf1[line + 1] = str;
                if (line == 0)
                    buf1[line] = sss;
                if (line == buf.Length - 1)
                    buf1[line + 2] = sss;
                str = null;
                sss = null;
            }
            for (int line = 1; line < buf1.Length - 1; line++)
            {
                for (int row = 1; row < buf1[line].Length - 1; row++)
                {
                    if ((buf1[line][row] >= '0') && (buf1[line][row] <= '9'))
                    {
                        symbolNum = symbolNum * 10 + buf1[line][row] - 0x30;
                        if ((buf1[line - 1][row - 1] == '*') || (buf1[line - 1][row] == '*') || (buf1[line - 1][row + 1] == '*') ||       //up line
                         (buf1[line][row - 1] == '*') || (buf1[line][row + 1] == '*') ||                                                  //this line
                         (buf1[line + 1][row - 1] == '*') || (buf1[line + 1][row] == '*') || (buf1[line + 1][row + 1] == '*'))             //down line
                            Flag_symbol = true;
                    }
                    else if (Flag_symbol)
                    {
                        result += symbolNum;
                        Console.WriteLine(symbolNum);
                        Console.WriteLine(result);
                        Flag_symbol = false;
                        symbolNum = 0;
                    }
                    else
                        symbolNum = 0;
                }
            }
        }

        public void Week3_2()
        {
            //根据‘*’的位置，标记周围的数字，
            //根据标记的数字的位置算出数字做乘法
            string[] buf;
            int result = 0;
            buf = File.ReadAllLines("W3_PuzzleInput.txt");
            int symbolNum1 = 0;
            int symbolNum2 = 0;
            List<List<int>> arr = new List<List<int>>();

            for (int line = 1; line < buf.Length - 1; line++)
            {//第一行和最后一行没有特殊字符
                for (int row = 1; row < buf[line].Length - 1; row++)
                {//第一列和最后一列没有特殊字符
                    if (buf[line][row] == '*')
                    {
                        List<int> temp = new List<int>();
                        if ((buf[line - 1][row] >= '0') && (buf[line - 1][row] <= '9'))
                        {//正上   如果正上是我们想要的字符中的某一位，那左上，右上一定不会存在另外一个
                            temp.Add(line - 1); temp.Add(row);
                        }
                        else
                        {
                            if ((buf[line - 1][row - 1] >= '0') && (buf[line - 1][row - 1] <= '9'))
                            {//左上
                                temp.Add(line - 1); temp.Add(row - 1);
                            }
                            if ((buf[line - 1][row + 1] >= '0') && (buf[line - 1][row + 1] <= '9'))
                            {//右上
                                temp.Add(line - 1); temp.Add(row + 1);
                            }
                        }
                        if ((buf[line][row - 1] >= '0') && (buf[line][row - 1] <= '9'))
                        {//左
                            temp.Add(line); temp.Add(row - 1);
                        }
                        if ((buf[line][row + 1] >= '0') && (buf[line][row + 1] <= '9'))
                        {//右
                            temp.Add(line); temp.Add(row + 1);
                        }
                        if ((buf[line + 1][row] >= '0') && (buf[line + 1][row] <= '9'))
                        {//正下
                            temp.Add(line + 1); temp.Add(row);
                        }
                        else
                        {
                            if ((buf[line + 1][row - 1] >= '0') && (buf[line + 1][row - 1] <= '9'))
                            {//左下
                                temp.Add(line + 1); temp.Add(row - 1);
                            }
                            if ((buf[line + 1][row + 1] >= '0') && (buf[line + 1][row + 1] <= '9'))
                            {//右下
                                temp.Add(line + 1); temp.Add(row + 1);
                            }
                        }
                        arr.Add(temp);
                    }
                }
            }
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i].Count == 4)
                {
                    symbolNum1 = 0;
                    symbolNum2 = 0;
                    while ((buf[arr[i][0]][arr[i][1]] >= '0') && (buf[arr[i][0]][arr[i][1]] <= '9'))
                    {
                        arr[i][1]--;
                    }
                    arr[i][1]++;
                    while ((buf[arr[i][0]][arr[i][1]] >= '0') && (buf[arr[i][0]][arr[i][1]] <= '9'))
                    {
                        symbolNum1 = symbolNum1 * 10 + buf[arr[i][0]][arr[i][1]] - 0x30;
                        arr[i][1]++;
                    }
                    while ((buf[arr[i][2]][arr[i][3]] >= '0') && (buf[arr[i][2]][arr[i][3]] <= '9'))
                    {
                        arr[i][3]--;
                        if (arr[i][3] == -1)
                            break;
                    }
                    arr[i][3]++;
                    while ((buf[arr[i][2]][arr[i][3]] >= '0') && (buf[arr[i][2]][arr[i][3]] <= '9'))
                    {
                        symbolNum2 = symbolNum2 * 10 + buf[arr[i][2]][arr[i][3]] - 0x30;
                        arr[i][3]++;
                    }
                    result += symbolNum1 * symbolNum2;
                    Console.WriteLine(symbolNum1);
                    Console.WriteLine(symbolNum2);
                    Console.WriteLine(result);
                }
            }
        }

        public void Week4_1()
        {
            string[] buf, temp, winNumber;
            int result = 0;
            int point;
            buf = File.ReadAllLines("W4_PuzzleInput.txt");
            for (int line = 0; line < buf.Length; line++)
            {
                temp = buf[line].Split(':');
                temp = temp[1].Split('|');
                winNumber = temp[0].Split(' ');
                temp[1] += " ";
                point = 1;
                for (int i = 1; i < winNumber.Length - 1; i++)
                {
                    if ((winNumber[i] != "") && (temp[1].Contains(" " + winNumber[i] + " ")))
                    {
                        point *= 2;
                    }
                }
                result += point / 2;
                Console.WriteLine(line + 1);
                Console.WriteLine(point / 2);
                Console.WriteLine(result);
            }
        }

        public void Week4_2()
        {
            string[] buf, temp, winNumber;
            List<int> point = new List<int>();
            List<List<int>> point1 = new List<List<int>>();
            int result = 0;
            buf = File.ReadAllLines("W4_PuzzleInput.txt");
            int pointnum = 0;
            for (int line = 0; line < buf.Length; line++)
            {//先计算每次中奖数字的个数
                temp = buf[line].Split(':');
                temp = temp[1].Split('|');
                winNumber = temp[0].Split(' ');
                temp[1] += " ";
                pointnum = 0;
                for (int i = 1; i < winNumber.Length - 1; i++)
                {
                    if ((winNumber[i] != "") && (temp[1].Contains(" " + winNumber[i] + " ")))
                    {
                        pointnum++;
                    }
                }
                if (line >= point1.Count())
                {
                    List<int> num = new List<int>();
                    num.Add(1);
                    point1.Add(num);
                }
                int num1 = point1[line].Sum();
                for (int row = 1; row <= pointnum; row++)
                {
                    if (line + row >= point1.Count())
                    {
                        List<int> num = new List<int>();
                        num.Add(1);
                        point1.Add(num);
                    }
                    point1[line + row].Add(num1);
                }
                result += point1[line].Sum();
                Console.WriteLine(point1[line].Sum());
                Console.WriteLine(result);
            }
        }

        public void Week5_1()
        {
            List<long> seeds = new List<long>();
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W5_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");
            List<List<List<long>>> Maps = new List<List<List<long>>>();
            List<List<long>> map = new List<List<long>>();
            foreach (string line in PuzzleInput)
            {
                if (line.Contains("seeds"))
                {
                    for (int num = 1; num < (line.Split(':')[1]).Split(' ').Length; num++)
                    {
                        seeds.Add(Convert.ToInt64((line.Split(':')[1]).Split(' ')[num]));
                    }
                }
                else if (line.Contains("map"))
                {
                    if (map.LongCount() > 0)
                        Maps.Add(map);
                    map = new List<List<long>>();
                }
                else if (line.LongCount() > 0)
                {
                    List<long> row = new List<long>();
                    row.Add(Convert.ToInt64(line.Split(' ')[0]));
                    row.Add(Convert.ToInt64(line.Split(' ')[1]));
                    row.Add(Convert.ToInt64(line.Split(' ')[2]));
                    map.Add(row);
                }
            }
            Maps.Add(map);
            //List<ValueType> map = new List<ValueType>();
            long result = 0x7fffffff;
            foreach (long seed in seeds)
            {
                long location = seed;
                Console.WriteLine(location);
                for (int i = 0; i < Maps.LongCount(); i++)
                {
                    for (int k = 0; k < Maps[i].LongCount(); k++)
                    {
                        if ((location < (Maps[i][k][1] + Maps[i][k][2])) && (location >= Maps[i][k][1]))
                        {
                            location = location - Maps[i][k][1] + Maps[i][k][0];
                            break;
                        }
                    }
                    Console.WriteLine(location);
                }
                if (location < result)
                    result = location;
                Console.WriteLine(result);
            }
        }

        public void Week5_2()
        {
            List<List<long>> seeds = new List<List<long>>();
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W5_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");
            List<List<List<long>>> Maps = new List<List<List<long>>>();
            List<List<long>> map = new List<List<long>>();
            foreach (string line in PuzzleInput)
            {
                if (line.Contains("seeds"))
                {
                    for (int num = 1; num < (line.Split(':')[1]).Split(' ').Length; num += 2)
                    {
                        List<long> row = new List<long>();
                        row.Add(Convert.ToInt64((line.Split(':')[1]).Split(' ')[num]));
                        row.Add(Convert.ToInt64((line.Split(':')[1]).Split(' ')[num + 1]));
                        seeds.Add(row);
                    }
                    seeds.Sort((List<long> x, List<long> y) => { return x[0].CompareTo(y[0]); });
                }
                else if (line.Contains("map"))
                {
                    if (map.LongCount() > 0)
                        Maps.Add(map);
                    map = new List<List<long>>();
                }
                else if (line.LongCount() > 0)
                {
                    List<long> row = new List<long>();
                    row.Add(Convert.ToInt64(line.Split(' ')[0]));
                    row.Add(Convert.ToInt64(line.Split(' ')[1]));
                    row.Add(Convert.ToInt64(line.Split(' ')[2]));
                    map.Add(row);
                }
            }
            Maps.Add(map);

            //5-2分情况讨论
            //location   :            #---------------------#
            //seed case1 :   *---*
            //seed case2 :   *---------------------*
            //seed case3 :   *-----------------------------------------*
            //seed case4 :                *--------*
            //seed case5 :                *----------------------------*
            //seed case6 :                                       *-----*
            for (int i = 0; i < Maps.LongCount(); i++)
            {
                List<List<long>> newseeds = new List<List<long>>();
                Maps[i].Sort((List<long> x, List<long> y) => { return x[1].CompareTo(y[1]); });//排序
                for (int sed = 0; sed < seeds.LongCount(); sed++)
                {
                    List<long> seed = new List<long>();
                    seed.Add(0); seed.Add(0);
                    //for (k = 0; k < Maps[i].LongCount(); k++)
                    while (Maps[i].LongCount() > 0)
                    {
                        if (seeds[sed][0] < Maps[i][0][1])
                        {
                            if (seeds[sed][0] + seeds[sed][1] < Maps[i][0][1])
                            {
                                //case1
                                seed = new List<long>();
                                seed = seeds[sed];
                                newseeds.Add(seed);
                                break;
                            }
                            else if (seeds[sed][0] + seeds[sed][1] <= Maps[i][0][1] + Maps[i][0][2])
                            {
                                //case2
                                seed = new List<long>();
                                seed.Add(seeds[sed][0]);
                                seed.Add(Maps[i][0][1] - seeds[sed][0]);
                                newseeds.Add(seed);

                                seed = new List<long>();
                                seed.Add(Maps[i][0][0]);
                                seed.Add(seeds[sed][0] + seeds[sed][1] - Maps[i][0][1]);
                                newseeds.Add(seed);
                                break;
                            }
                            else
                            {
                                //case3
                                seed = new List<long>();
                                seed.Add(seeds[sed][0]);
                                seed.Add(Maps[i][0][1] - seeds[sed][0]);
                                newseeds.Add(seed);

                                seed = new List<long>();
                                seed.Add(Maps[i][0][0]);
                                seed.Add(Maps[i][0][2]);
                                newseeds.Add(seed);

                                seeds[sed][1] = seeds[sed][0] + seeds[sed][1] - (Maps[i][0][1] + Maps[i][0][2]);
                                seeds[sed][0] = Maps[i][0][1] + Maps[i][0][2];
                                Maps[i].RemoveAt(0);
                            }
                        }
                        else if (seeds[sed][0] < Maps[i][0][1] + Maps[i][0][2])
                        {
                            if (seeds[sed][0] + seeds[sed][1] <= Maps[i][0][1] + Maps[i][0][2])
                            {
                                //case4
                                seed = new List<long>();
                                seed.Add(Maps[i][0][0] + seeds[sed][0] - Maps[i][0][1]);
                                seed.Add(seeds[sed][1]);
                                newseeds.Add(seed);
                                break;
                            }
                            else
                            {//case5
                                seed = new List<long>();
                                seed.Add(Maps[i][0][0] + seeds[sed][0] - Maps[i][0][1]);
                                seed.Add(Maps[i][0][1] + Maps[i][0][2] - seeds[sed][0]);
                                newseeds.Add(seed);

                                seeds[sed][1] = (seeds[sed][0] + seeds[sed][1]) - (Maps[i][0][1] + Maps[i][0][2]);
                                seeds[sed][0] = Maps[i][0][1] + Maps[i][0][2];
                                Maps[i].RemoveAt(0);
                            }
                        }
                        else
                        {
                            Maps[i].RemoveAt(0);
                        }
                    }
                    if (((seed[0] == 0) && (seed[1] == 0)) || (Maps[i].LongCount() == 0))
                        newseeds.Add(seeds[sed]);
                }
                seeds = newseeds;
                seeds.Sort((List<long> x, List<long> y) => { return x[0].CompareTo(y[0]); });
            }
            Console.WriteLine(seeds[0][0]);
        }

        public void Week6_1()
        {
            List<int> time = new List<int>();
            List<int> distance = new List<int>();
            long result = 1;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W6_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");
            foreach (string str in PuzzleInput[0].Split(':')[1].Split(' '))
            {
                if (str != "")
                    time.Add(Convert.ToInt32(str));
            }
            foreach (string str in PuzzleInput[1].Split(':')[1].Split(' '))
            {
                if (str != "")
                    distance.Add(Convert.ToInt32(str));
            }

            for (int i = 0; i < time.Count; i++)
            {
                int succ = 0;
                for (int k = 0; k < time[i]; k++)
                {
                    if (k * (time[i] - k) > distance[i])
                        succ++;
                }
                result *= succ;
            }
            Console.WriteLine(result);
        }

        public void Week6_2()
        {
            List<int> time = new List<int>();
            List<long> distance = new List<long>();
            long result = 1;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W6_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");
            string str1 = PuzzleInput[0].Split(':')[1];
            time.Add(Convert.ToInt32(str1.Replace(" ","")));
            str1 = PuzzleInput[1].Split(':')[1];
            distance.Add(Convert.ToInt64(str1.Replace(" ", "")));

            for (int i = 0; i < time.Count; i++)
            {
                int succ = 0;
                for (long k = 0; k < time[i]; k++)
                {
                    if (k * (time[i] - k) > distance[i])
                        succ++;
                }
                result *= succ;
            }
            Console.WriteLine(result);
        }

        class cardstype
        {
            public int id;
            public List<int> type = new List<int>();
            public string name;
            public int value;
        }

        public void Week7_1()
        {
            List<cardstype> Cards = new List<cardstype>();
            long result = 0;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W7_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");

            for (int i = 0; i < PuzzleInput.Length; i++)
            {
                cardstype hands = new cardstype();

                hands.name = PuzzleInput[i].Split(' ')[0];
                hands.name = hands.name.Replace('A', 'E');
                hands.name = hands.name.Replace('K', 'D');
                hands.name = hands.name.Replace('Q', 'C');
                hands.name = hands.name.Replace('J', 'B');
                hands.name = hands.name.Replace('T', 'A');
                hands.value = Convert.ToInt32(PuzzleInput[i].Split(' ')[1]);
                string str = hands.name;
                for (int j = 0; j < str.Length; j++)
                {
                    hands.type.Insert(0, 1);
                    for (int k = j + 1; k < str.Length;)
                    {
                        if (str[j] == str[k])
                        {
                            hands.type[0]++;
                            str = str.Remove(k, 1);
                        }
                        else
                            k++;
                    }
                }
                hands.type.Sort((x, y) => { return y.CompareTo(x); });
                switch (hands.type[0])
                {
                    case 1:  //ABCDE
                        hands.id = 0;
                        break;
                    case 2:  //AABCD
                        if (hands.type[1] == 1)
                            hands.id = 1;
                        else//AABBC
                            hands.id = 2;
                        break;  
                    case 3: //AAABC
                        if (hands.type[1] == 1)
                            hands.id = 3;
                        else//AAABB
                            hands.id = 4;
                        break;
                    case 4://AAAAB
                        hands.id = 5;
                        break;
                    case 5://AAAAA
                        hands.id = 6;
                        break;
                    default:
                        break;
                }
                Cards.Add(hands);
            }
            //排序
            Cards.Sort(delegate (cardstype x, cardstype y)
            {  
                if(x.id == y.id)
                    if (x.name[0] == y.name[0])
                        if (x.name[1] == y.name[1])
                            if (x.name[2] == y.name[2])
                                if (x.name[3] == y.name[3])
                                    return x.name[4].CompareTo(y.name[4]);
                                else
                                    return x.name[3].CompareTo(y.name[3]);
                            else
                                return x.name[2].CompareTo(y.name[2]);
                        else
                            return x.name[1].CompareTo(y.name[1]);
                    else
                        return  x.name[0].CompareTo(y.name[0]);
                else
                    return x.id.CompareTo(y.id);
            });
            for (int i = 0; i < Cards.Count; i++)
                Console.WriteLine(Cards[i].name);
            for (int i = 0; i < Cards.Count; i++)
                result += (i + 1) * Cards[i].value;

            Console.WriteLine(result);
        }

        public void Week7_2()
        {
            List<cardstype> Cards = new List<cardstype>();
            long result = 0;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W7_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");

            for (int i = 0; i < PuzzleInput.Length; i++)
            {
                cardstype hands = new cardstype();

                hands.name = PuzzleInput[i].Split(' ')[0];
                hands.name = hands.name.Replace('A', 'E');
                hands.name = hands.name.Replace('K', 'D');
                hands.name = hands.name.Replace('Q', 'C');
                hands.name = hands.name.Replace('J', '1');
                hands.name = hands.name.Replace('T', 'A');
                hands.value = Convert.ToInt32(PuzzleInput[i].Split(' ')[1]);
                string str = hands.name;
                int Count_J = 0;
                for (int j = 0; j < str.Length; j++)
                {
                    hands.type.Insert(0, 1);
                    for (int k = j + 1; k < str.Length;)
                    {
                        if (str[j] == str[k])
                        {
                            hands.type[0]++;
                            str = str.Remove(k, 1);
                        }
                        else
                            k++;
                    }
                    if ((str[j] == '1') && hands.type[0] != 5)
                    {
                        Count_J = hands.type[0];
                        hands.type[0] = 0;
                    }
                }
                hands.type.Sort((x, y) => { return y.CompareTo(x); });
                hands.type[0] += Count_J;
                switch (hands.type[0])
                {
                    case 1:  //ABCDE
                        hands.id = 0;
                        break;
                    case 2:  //AABCD
                        if (hands.type[1] == 1)
                            hands.id = 1;
                        else//AABBC
                            hands.id = 2;
                        break;
                    case 3: //AAABC
                        if (hands.type[1] == 1)
                            hands.id = 3;
                        else//AAABB
                            hands.id = 4;
                        break;
                    case 4://AAAAB
                        hands.id = 5;
                        break;
                    case 5://AAAAA
                        hands.id = 6;
                        break;
                    default:
                        break;
                }
                Cards.Add(hands);
            }
            //排序
            Cards.Sort(delegate (cardstype x, cardstype y)
            {
                if (x.id == y.id)
                    if (x.name[0] == y.name[0])
                        if (x.name[1] == y.name[1])
                            if (x.name[2] == y.name[2])
                                if (x.name[3] == y.name[3])
                                    return x.name[4].CompareTo(y.name[4]);
                                else
                                    return x.name[3].CompareTo(y.name[3]);
                            else
                                return x.name[2].CompareTo(y.name[2]);
                        else
                            return x.name[1].CompareTo(y.name[1]);
                    else
                        return x.name[0].CompareTo(y.name[0]);
                else
                    return x.id.CompareTo(y.id);
            });
            for (int i = 0; i < Cards.Count; i++)
                Console.WriteLine(Cards[i].name);
            for (int i = 0; i < Cards.Count; i++)
                result += (i + 1) * Cards[i].value;

            Console.WriteLine(result);
        }
        class output
        {
            public string left;
            public string right;
        }
        public void Week8_1()
        {
            long result = 0;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W8_PuzzleInput.txt");
            // = File.ReadAllLines("test.txt");
            Dictionary<string, output> PuzzleDictionary = new Dictionary<string, output>();

            string ditect = PuzzleInput[0];
            string buf = "";
            for (int i = 2; i < PuzzleInput.Length; i++)
            {
                output Myoutput = new output();
                buf = PuzzleInput[i].Split(' ')[0];
                Myoutput.left = PuzzleInput[i].Split(' ')[2].Replace("(","").Replace(",", "");
                Myoutput.right = PuzzleInput[i].Split(' ')[3].Replace(")", "");
                PuzzleDictionary.Add(buf, Myoutput);
            }
            buf = "AAA";
            int index = 0;
            while (true)
            {
                output newMyoutput = PuzzleDictionary[buf];

                if (ditect[index] == 'L')
                    buf = newMyoutput.left;
                else
                    buf = newMyoutput.right;
                result++;
                index++;
                if (index == ditect.Length) index = 0;
                if(buf == "ZZZ")
                    break;
            }
            Console.WriteLine(result);
        }
        public void Week8_2()
        {
            //把所有的A-Z都找到，求最小公倍数
            long result = 0;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W8_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");
            Dictionary<string, output> PuzzleDictionary = new Dictionary<string, output>();

            string ditect = PuzzleInput[0];
            string buf = "";
            List<string> nodes = new List<string>();
            List<long> output = new List<long>();
            for (int i = 2; i < PuzzleInput.Length; i++)
            {
                output Myoutput = new output();
                buf = PuzzleInput[i].Split(' ')[0];
                Myoutput.left = PuzzleInput[i].Split(' ')[2].Replace("(", "").Replace(",", "");
                Myoutput.right = PuzzleInput[i].Split(' ')[3].Replace(")", "");
                PuzzleDictionary.Add(buf, Myoutput);
                if (buf[2] == 'A')
                    nodes.Add(buf);
            }
            int index = 0;
            for (int i = 0; i < nodes.Count; i++)
            {
                buf = nodes[i];
                result = 0;
                while (true)
                {
                    output newMyoutput = PuzzleDictionary[buf];

                    if (ditect[index] == 'L')
                        buf = newMyoutput.left;
                    else
                        buf = newMyoutput.right;
                    result++;
                    index++;
                    if (index == ditect.Length) index = 0;
                    if (buf[2] == 'Z')
                        break;
                }
                output.Add(result);
            }
            int k = 2;
            result = 1;
            while (true)
            {
                bool flag = false;
                for (int i = 0; i < output.Count; i++)
                {
                    if (output[i] % k != 0)
                        break;
                    if (i == output.Count - 1)
                        flag = true;
                }
                if (flag == true)
                {
                    for (int i = 0; i < output.Count; i++)
                        output[i] /= k;
                    result *= k;
                    k = 2;
                }
                if (k > output.Max())
                    break;
                k++;
            }
            Console.WriteLine("最大公约数："+result);
            for (int i = 0; i < output.Count; i++)
                result *= output[i];
            Console.WriteLine(result);
        }

        public void Week9_1()
        {
            long result = 0;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W9_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");

            List<List<long>> inputArray = new List<List<long>>();
            for (int i = 0; i < PuzzleInput.Length; i++)
            {
                List<long> input = new List<long>();
                string[] buf = PuzzleInput[i].Split(' ');
                for (int j = 0;j< buf.Length; j++)
                    input.Add(long.Parse(buf[j]));
                inputArray.Add(input);
            }
            for (int i = 0; i < inputArray.Count; i++)
            {
                List<List<long>> input = new List<List<long>>();
                input.Add(inputArray[i]);
                int j = 0;
                while (!input[j].All(x => x == 0))
                {
                    List<long> output = new List<long>();
                    for (int k = 1; k < input[j].Count; k++)
                        output.Add(input[j][k] - input[j][k - 1]);
                    input.Add(output);
                    j++;
                }
                for (int k = input.Count-2; k >=0; k--)
                    input[k].Add(input[k + 1][input[k + 1].Count - 1] + input[k][input[k].Count - 1]);
                result += input[0][input[0].Count - 1];
            }
            Console.WriteLine(result);
        }

        public void Week9_2()
        {
            long result = 0;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W9_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");
            List<List<long>> inputArray = new List<List<long>>();
            for (int i = 0; i < PuzzleInput.Length; i++)
            {
                List<long> input = new List<long>();
                string[] buf = PuzzleInput[i].Split(' ');
                for (int j = 0; j < buf.Length; j++)
                    input.Add(long.Parse(buf[j]));
                inputArray.Add(input);
            }
            for (int i = 0; i < inputArray.Count; i++)
            {
                List<List<long>> input = new List<List<long>>();
                input.Add(inputArray[i]);
                int j = 0;
                while (!input[j].All(x => x == 0))
                {
                    List<long> output = new List<long>();
                    for (int k = 1; k < input[j].Count; k++)
                        output.Add(input[j][k] - input[j][k - 1]);
                    input.Add(output);
                    j++;
                }
                for (int k = input.Count - 2; k >= 0; k--)
                    input[k].Insert(0, input[k][0] - input[k + 1][0]);

                result += input[0][0];
            }
            Console.WriteLine(result);
        }

        public void Week10_1()
        {
            long result = 0;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W10_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");
            List<List<string>> inputArray = new List<List<string>>();
            int[] P_X = { 0, 0, 0, 0 }; int[] P_Y = { 0, 0, 0, 0 };
            int[] P_dir = { 0, 1, 2, 3 };  // 0---right   1---left    2---down    3---up
            int[] buf = { 0, 0, 0, 0 };
            string[] key = { "0","1","2","3"};
            for (int i = 0; i < PuzzleInput.Length; i++)
            {
                List<string> arr = new List<string>();
                for (int j = 0; j < PuzzleInput[i].Length; j++)
                    arr.Add(PuzzleInput[i][j].ToString());
                if (PuzzleInput[i].Contains("S"))
                {
                    P_Y[0] = i; P_Y[1] = i; P_Y[2] = i+1; P_Y[3] = i-1;
                    P_X[3] = PuzzleInput[i].IndexOf("S", StringComparison.Ordinal);
                    P_X[2] = P_X[3]; P_X[1] = P_X[3]-1; P_X[0] = P_X[3]+1;
                }
                inputArray.Add(arr);
            }
            int x = 0;int y = 0;int dir = 0; int flag = 0;
            for (int i = 0; i < 4; i++)
            {
                dir = P_dir[i];   flag = 0;
                while ((x >= 0) && (x < inputArray[y].Count) && (y >= 0) && (y < inputArray.Count))
                {
                    if (flag == 1)
                    {
                        if (buf[i] > result)
                            result = buf[i];
                        break;
                    }
                    string puzzle = inputArray[P_Y[i]][P_X[i]];
                    x = P_X[i]; y = P_Y[i];
                    //inputArray[P_Y[i]][P_X[i]] = buf[i].ToString();
                    switch (puzzle)
                    {
                        case "F":
                            if (P_dir[i] == 3) { P_X[i]++; P_dir[i] = 0; }
                            else if (P_dir[i] == 1) { P_Y[i]++; P_dir[i] = 2; }
                            else { flag = 1; }
                            break;
                        case "J":
                            if (P_dir[i] == 2) { P_X[i]--; P_dir[i] = 1; }
                            else if (P_dir[i] == 0) { P_Y[i]--; P_dir[i] = 3; }
                            else { flag = 1; }
                            break;
                        case "L":
                            if (P_dir[i] == 2) { P_X[i]++; P_dir[i] = 0; }
                            else if (P_dir[i] == 1) { P_Y[i]--; P_dir[i] = 3; }
                            else { flag = 1; }
                            break;
                        case "7":
                            if (P_dir[i] == 3) { P_X[i]--; P_dir[i] = 1; }
                            else if (P_dir[i] == 0) { P_Y[i]++; P_dir[i] = 2; }
                            else { flag = 1; }
                            break;
                        case "|":
                            if (P_dir[i] == 3) { P_Y[i]--; P_dir[i] = 3; }
                            else if (P_dir[i] == 2) { P_Y[i]++; P_dir[i] = 2; }
                            else { flag = 1; }
                            break;
                        case "-":
                            if (P_dir[i] == 1) { P_X[i]--; P_dir[i] = 1; }
                            else if (P_dir[i] == 0) { P_X[i]++; P_dir[i] = 0; }
                            else { flag = 1; }
                            break;
                        case "S":
                            flag = 1;
                            break;
                        default:
                            flag = 1;
                            break;
                    }
                    if (flag == 1)
                        continue;
                    inputArray[y][x] = (buf[i] * 10).ToString();
                    buf[i]++;
                }
            }
            //for (int i = 0; i < inputArray.Count; i++)
            //{
            //    for (int j = 0; j < inputArray[i].Count; j++)
            //    {
            //        Console.Write(inputArray[i][j] + " ");
            //    }
            //    Console.WriteLine();
            //}
            Console.WriteLine((result + 1) / 2);
        }

        public void Week10_2()
        {
            //思路：如何区分是不是在环内：一个左必定对应一个右，一个上必定对应一个下
            //如果在环内“一侧的上下或左右的数量一定不相等。相等则不在环内
            long result = 0;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W10_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");
            List<List<string>> inputArray = new List<List<string>>();
            int[] P_X = { 0, 0, 0, 0 }; int[] P_Y = { 0, 0, 0, 0 };
            int[] P_dir = { 0, 1, 2, 3 };  // 0---right   1---left    2---down    3---up
            int[] buf = { 0, 0, 0, 0 };
            string[] key = { "→", "←", "↓", "↑" };
            for (int i = 0; i < PuzzleInput.Length; i++)
            {
                List<string> arr = new List<string>();
                for (int j = 0; j < PuzzleInput[i].Length; j++)
                    arr.Add(PuzzleInput[i][j].ToString());
                if (PuzzleInput[i].Contains("S"))
                {
                    P_Y[0] = i; P_Y[1] = i; P_Y[2] = i + 1; P_Y[3] = i - 1;
                    P_X[3] = PuzzleInput[i].IndexOf("S", StringComparison.Ordinal);
                    P_X[2] = P_X[3]; P_X[1] = P_X[3] - 1; P_X[0] = P_X[3] + 1;
                }
                inputArray.Add(arr);
            }
            //先找到环，环的终点是“S”，并把环处理成箭头
            int x = 0; int y = 0; string dir = ""; int flag = 0;
            for (int i = 0; i < 4; i++)
            {
                flag = 0;
                while ((y >= 0) && (x >= 0) && (x < inputArray[y].Count) &&  (y < inputArray.Count))
                {
                    if (flag == 1)
                    {
                        if (buf[i] > result)
                            result = buf[i];
                        break;
                    }
                    string puzzle = inputArray[P_Y[i]][P_X[i]];
                    x = P_X[i]; y = P_Y[i];
                    switch (puzzle)
                    {
                        case "F":
                            if (P_dir[i] == 3) { P_X[i]++; P_dir[i] = 0; dir = "↑→"; }
                            else if (P_dir[i] == 1) { P_Y[i]++; P_dir[i] = 2; dir = "↓←"; }
                            else { flag = 1; }
                            break;
                        case "J":
                            if (P_dir[i] == 2) { P_X[i]--; P_dir[i] = 1; dir = "←↓"; }
                            else if (P_dir[i] == 0) { P_Y[i]--; P_dir[i] = 3; dir = "→↑"; }
                            else { flag = 1; }
                            break;
                        case "L":
                            if (P_dir[i] == 2) { P_X[i]++; P_dir[i] = 0; dir = "↓→"; }
                            else if (P_dir[i] == 1) { P_Y[i]--; P_dir[i] = 3; dir = "↑←"; }
                            else { flag = 1; }
                            break;
                        case "7":
                            if (P_dir[i] == 3) { P_X[i]--; P_dir[i] = 1; dir = "←↑"; }
                            else if (P_dir[i] == 0) { P_Y[i]++; P_dir[i] = 2; dir = "→↓"; }
                            else { flag = 1; }
                            break;
                        case "|":
                            if (P_dir[i] == 3) { P_Y[i]--; P_dir[i] = 3; dir = "↑↑"; }
                            else if (P_dir[i] == 2) { P_Y[i]++; P_dir[i] = 2; dir = "↓↓"; }
                            else { flag = 1; }
                            break;
                        case "-":
                            if (P_dir[i] == 1) { P_X[i]--; P_dir[i] = 1; dir = "←←"; }
                            else if (P_dir[i] == 0) { P_X[i]++; P_dir[i] = 0; dir = "→→"; }
                            else { flag = 1; }
                            break;
                        case "S":
                            switch (i)
                            {
                                case 0:
                                    if (P_dir[i] == 0) dir = "→→";
                                    else if (P_dir[i] == 2) dir = "↓→";
                                    else if (P_dir[i] == 3) dir = "↑→";
                                    else dir = "S";
                                    break;
                                case 1:
                                    if (P_dir[i] == 1) dir = "←←";
                                    else if (P_dir[i] == 2) dir = "↓←";
                                    else if (P_dir[i] == 3) dir = "↑←";
                                    else dir = "S";
                                    break;
                                case 2:
                                    if (P_dir[i] == 0) dir = "→↓";
                                    else if (P_dir[i] == 1) dir = "←↓";
                                    else if (P_dir[i] == 2) dir = "↓↓";
                                    else dir = "S";
                                    break;
                                case 3:
                                    if (P_dir[i] == 0) dir = "→↑";
                                    else if (P_dir[i] == 1) dir = "←↑";
                                    else if (P_dir[i] == 2) dir = "↑↑";
                                    else dir = "S";
                                    break;
                                default: break;
                            }
                            buf[i]++;
                            inputArray[y][x] = dir;
                            flag = 1;
                            break;
                        default:
                            flag = 1;
                            break;
                    }
                    if (flag == 1)
                        continue;
                    buf[i]++;
                    inputArray[y][x] = dir;
                }
            }
            //确定字符在环内还是环外：只需要确认上方和左侧就可以了
            int buf_result1 = 0; int buf_result2 = 0; result = 0;
            for (int i = 0; i < inputArray.Count; i++)
            {
                for (int j = 0; j < inputArray[i].Count; j++)
                {
                    buf_result1 = 0;  buf_result2 = 0;
                    if ((i == 0) || (j == 0)) { inputArray[i][j] = "o"; continue; }
                    if (inputArray[i][j].Length > 1) continue;
                    if ((inputArray[i - 1][j] == "o") || (inputArray[i][j - 1] == "o")) { inputArray[i][j] = "o"; continue; }
                    for (int k = i - 1; k >= 0; k--)
                    {//up
                        if (inputArray[k][j].Contains("←"))
                            buf_result1++;
                        if (inputArray[k][j].Contains("←←"))
                            buf_result1++;
                        if (inputArray[k][j].Contains("→"))
                            buf_result2++;
                        if (inputArray[k][j].Contains("→→"))
                            buf_result2++;
                    }
                    if (buf_result1 != buf_result2)
                    {
                        result++;
                        inputArray[i][j] = "i";
                        continue;
                    }
                    for (int k = j - 1; k >= 0; k--)
                    {//left
                        if (inputArray[i][k].Contains("↑"))
                            buf_result1++;
                        if (inputArray[i][k].Contains("↑↑"))
                            buf_result1++;
                        if (inputArray[i][k].Contains("↓"))
                            buf_result2++;
                        if (inputArray[i][k].Contains("↓↓"))
                            buf_result2++;
                    }
                    if (buf_result1 != buf_result2)
                    {
                        result++;
                        inputArray[i][j] = "i";
                        continue;
                    }
                    inputArray[i][j] = "o";
                }
            }
            //for (int i = 0; i < inputArray.Count; i++)
            //{
            //    for (int j = 0; j < inputArray[i].Count; j++)
            //    {
            //        Console.Write(inputArray[i][j] + ";");
            //    }
            //    Console.WriteLine();
            //}
            Console.WriteLine((result));
        }

        public void Week11_1()
        {
            //横向压扁和纵向压扁
            long result = 0;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W11_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");
            List<List<string>> inputArray = new List<List<string>>();
            int[] inputLine = new int[PuzzleInput.Length];
            int[] inputRow = new int[PuzzleInput[0].Length];
            for (int i = 0; i < PuzzleInput.Length; i++)
            {
                List<string> arr = new List<string>();
                for (int j = 0; j < PuzzleInput[i].Length; j++)
                {
                    arr.Add(PuzzleInput[i][j].ToString());
                    if (PuzzleInput[i][j] == '#')
                    {
                        inputLine[i]++;inputRow[j]++;
                    }
                }
                inputArray.Add(arr);
            }
            int flag_Nop = 0;
            result = 0;
            for (int i = 0; i < inputLine.Length-1; i++)
            {
                flag_Nop=0;
                if (inputLine[i] == 0) continue;
                for (int j = i + 1; j < inputLine.Length; j++)
                {
                    if (inputLine[j] == 0) { flag_Nop++; }
                    result += (j - i + flag_Nop) * inputLine[i] * inputLine[j];
                }
            }
            for (int i = 0; i < inputRow.Length-1; i++)
            {
                flag_Nop = 0;
                if (inputRow[i] == 0) continue;
                for (int j = i + 1; j < inputRow.Length; j++)
                {
                    if (inputRow[j] == 0) { flag_Nop++; }
                    result += (j - i + flag_Nop ) * inputRow[i] * inputRow[j];
                }
            }
            Console.WriteLine((result));
        }

        public void Week11_2()
        {
            //横向压扁和纵向压扁
            long result = 0;
            string[] PuzzleInput;
            PuzzleInput = File.ReadAllLines("W11_PuzzleInput.txt");
            //PuzzleInput = File.ReadAllLines("test.txt");
            List<List<string>> inputArray = new List<List<string>>();
            int[] inputLine = new int[PuzzleInput.Length];
            int[] inputRow = new int[PuzzleInput[0].Length];
            for (int i = 0; i < PuzzleInput.Length; i++)
            {
                List<string> arr = new List<string>();
                for (int j = 0; j < PuzzleInput[i].Length; j++)
                {
                    arr.Add(PuzzleInput[i][j].ToString());
                    if (PuzzleInput[i][j] == '#')
                    {
                        inputLine[i]++; inputRow[j]++;
                    }
                }
                inputArray.Add(arr);
            }
            int flag_Nop = 0;
            result = 0;
            for (int i = 0; i < inputLine.Length - 1; i++)
            {
                flag_Nop = 0;
                if (inputLine[i] == 0) continue;
                for (int j = i + 1; j < inputLine.Length; j++)
                {
                    if (inputLine[j] == 0) { flag_Nop++; }
                    result += (j - i + flag_Nop * 999999) * inputLine[i] * inputLine[j];
                }
            }
            for (int i = 0; i < inputRow.Length - 1; i++)
            {
                flag_Nop = 0;
                if (inputRow[i] == 0) continue;
                for (int j = i + 1; j < inputRow.Length; j++)
                {
                    if (inputRow[j] == 0) { flag_Nop++; }
                    result += (j - i + flag_Nop * 999999) * inputRow[i] * inputRow[j];
                }
            }
            Console.WriteLine((result));
        }
    }
}

