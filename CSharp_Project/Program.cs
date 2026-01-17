using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Program
{
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
    static StreamReader sr = new StreamReader(Console.OpenStandardInput());
    static StringBuilder sb = new StringBuilder();

    // public static void Main(string[] args)
    // {
    //     main();
    //     Exit();
    // }

    static void main()
    {
        int grade = 80;
        string medal = grade >= 90 ? "금메달" : grade >= 80 ? "은메달" : grade >= 70 ? "동메달" : "노메달";

        sw.WriteLine($"{medal}입니다.");
    }

    static void Exit()
    {
        sw.Close();
        sr.Close();
    }
}