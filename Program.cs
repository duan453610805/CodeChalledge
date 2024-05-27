using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SEGCodeChallenge;

namespace MyCodeChallenge
{
    static class Program
    {
        static void Main(string[] args)
        {
            CodeChallenge Code = new CodeChallenge();
            bool flag = true;
            Code.Week7_1();
            while (flag)
            {
                Console.WriteLine("Please Input your Puzzle Number(1-25),and press enter");
                Console.WriteLine("If you want quit,please enter space and enter");
                string str = Console.ReadLine();
                switch (str)
                {
                    case "3":
                        Code.Week3_1();
                        Code.Week3_2();
                        break;
                    case "4":
                        Code.Week4_1();
                        Code.Week4_2();
                        break;
                    case "5":
                        Code.Week5_1();
                        Code.Week5_2();
                        break;
                    case "6":
                        Code.Week6_1();
                        Code.Week6_2();
                        break;
                    case "7":
                        Code.Week7_1();
                        Code.Week7_2();
                        break;
                    case " ":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Please Input the right Puzzle Number(1-25),and Press enter\r\n");
                        break;
                }
            }
        }
    }
}
