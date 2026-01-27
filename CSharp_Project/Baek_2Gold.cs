using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;

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

    // IV   오큰수
    public static void Baek17298()
    {
        Stack<int> stack = new Stack<int>();
        int N = int.Parse(Console.ReadLine());
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        foreach (var num in input)
            stack.Push(num);    // 스택 완성

        int max = -1, right = 0;    // 뒤에서 부터 큰값 / 오른값 저장용 변수
        for (int i = 0; i < N; ++i) // Big(O) N번만큼만 실행.
        {
            right = stack.Pop();    // 오른값 저장.
            if (max < right)        // Max값 설정.
                max = right;
            if (0 == i)             // 맨 뒤부터니까 i == 0 일때 -1추가
                sb.Insert(0, " -1");

            if (stack.Count > 0)    // Stack이 비어있지 않다면 오른값과 Max값 비교.
            {
                if (right > stack.Peek())       // 오른값이 비교수보다 클때 (기본)
                    sb.Insert(0, $" {right}");
                else if (max > stack.Peek())    // Max값이 비교수보다 클때
                    sb.Insert(0, $" {max}");
                else                            // 오른값&Max값이 비교수보다 작을때. ex (9) 3 5 8
                    sb.Insert(0, " -1");
            }
        }
        sb.Remove(0, 1);    // 제일 앞줄의 공백 지우기

        Console.WriteLine(sb);
    }
    // IV   별 찍기11
    public static void Baek2448()
    {
        int N = int.Parse(Console.ReadLine());

        for (int i = 0; i < N; ++i)
        {
            for (int j = 0; j < N - i - 1; ++j)
                sb.Append(' ');

            for (int j = 0; j < 2 * i + 1; ++j)
            {   // 피라미드 형식으로 출력
                if (Recursion2448(i, j, N))
                    sb.Append('*');
                else
                    sb.Append(' ');
            }

            for (int j = 0; j < N - i - 1; ++j)
                sb.Append(' ');

            sb.Append('\n');
        }
        Console.Write(sb);
    }
    static bool Recursion2448(int i, int j, int length)
    {   // 길이가 제일 작은 삼각형 일때
        //  *   (0,0)
        // * *  (1,0) (1,1) (1,2)
        //***** (2,0) (2,1) (2,2) (2,3) (2,4)
        if (length == 3)
        {   // i와 j가 1일때만 공백
            if (i == 1 && j == 1) return false;
            return true;
        }

        // 길이가 긴 삼각형일때, 삼각형의 절반을 구함 48->24->12->6->3
        int half = length / 2;
        // 절반의 길이로 삼각형을 3부분으로 나누어 재귀실행 -> 작은 삼각형으로 그리자

        // 삼각형의 윗부분
        if (i < half)
            return Recursion2448(i, j, half);   // 절반의 길이의 같은 좌표로 재귀
                                                // // 삼각형의 아래 오른쪽부분
        else if (j >= length)
            return Recursion2448(i - half, j - length, half);   // i: 위로 / j: 왼쪽으로
                                                                // 삼각형의 아래 왼쪽부분
        else if (j < 2 * (i - half) + 1)
            return Recursion2448(i - half, j, half);    // i: 위로 / j: 그대로

        // 나머지 부분은 모두 비워서 그리자
        return false;
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