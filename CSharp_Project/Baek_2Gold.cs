using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Baek_2Gold
{
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
    static StreamReader sr = new StreamReader(Console.OpenStandardInput());
    static StringBuilder sb = new StringBuilder();

    ~Baek_2Gold()
    {
        sr.Close();
        sw.Close();
    }

    #region Gold I
    // I    name

    #endregion

    #region Gold II
    // II   name

    #endregion

    #region Gold III
    // III  name

    #endregion

    #region Gold IV

    // V    별 찍기10
        public static void Baek2448()
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; ++i)
        {
            for (int j = 0; j < N - i; ++j)
                sb.Append(' ');
            for (int j = 0; j < 1 + (i * 2); ++j)
            {
                if (Recursion2448(i, j))
                    sb.Append('*');
                else
                    sb.Append(' ');
            }
            sb.Append('\n');
        }
        Console.WriteLine(sb);
    }
    static bool Recursion2448(int i, int j)
    {   // 좌표가 2의 나머지가 1이라면 비우기.
        if (i % 3 == 1 && j % 3 == 1)
            return false;
        /*
        상위 좌표가 비워야하는 좌표인지 확인. (확인하는 이유 -> 아래 ex)
        ex (1,1)인 경우 (1,1)은 나머지가 모두 1
        ex (3,3) ~ (5,5)인 경우 (1,1)로 계산    (1,1)은 나머지가 모두 1
        */
        if (j > 1 && i > 1)
            return Recursion2448(i / 3, j / 3);
        return true;
    }

    #endregion

    #region Gold V
    // V    별 찍기10
    public static void Baek2447()
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; ++i)
        {
            for (int j = 0; j < N; ++j)
            {
                if (Recursion2447(i, j))
                    sb.Append('*');
                else
                    sb.Append(' ');
            }
            sb.Append('\n');
        }
        Console.WriteLine(sb);
    }
    static bool Recursion2447(int i, int j)
    {   // 좌표가 3의 나머지가 1이라면 비우기.
        if (i % 3 == 1 && j % 3 == 1)
            return false;
        /*
        상위 좌표가 비워야하는 좌표인지 확인. (확인하는 이유 -> 아래 ex)
        ex (3,3) ~ (5,5)인 경우 (1,1)로 계산    (1,1)은 나머지가 모두 1
        ex (3,12)~(5,14)인 경우 (1,4)로 계산    (1,4)는 나머지가 모두 1
        ex (9,9)~(17,17)인 경우 (3,3),(5,5)->(1,1)로 계산    (1,1)는 나머지가 모두 1
        */
        if (i > 1 || j > 1)
            return Recursion2447(i / 3, j / 3);
        return true;
    }

    #endregion
}