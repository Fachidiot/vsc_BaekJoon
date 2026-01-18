using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.WebSockets;
using System.Text;

class Baek_1Silver
{
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
    static StreamReader sr = new StreamReader(Console.OpenStandardInput());
    static StringBuilder sb = new StringBuilder();

    ~Baek_1Silver()
    {
        sr.Close();
        sw.Close();
    }

    #region Silver I
    // I    미로 탐색
    static void Baek2178()
    {   // 0,0 => N,M
        var NM = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int N = NM[0], M = NM[1];
        bool[,] graph = new bool[N, M];

        for (int i = 0; i < N; ++i)
        {   // input
            string input = sr.ReadLine();
            for (int j = 0; j < M; ++j)
            {
                graph[i, j] = input[j] == '0' ? false : true;
            }
        }
        BFS_2178(1, 1, graph);
        sw.WriteLine(graph[N, M]);
    }
    static void BFS_2178(int x, int y, bool[,] graph)
    {
        int[] xDir = { 1, -1, 0, 0 };   // 아래, 위, 오, 왼
        int[] yDir = { 0, 0, 1, -1 };
        bool[,] visited = new bool[graph.GetLength(0), graph.GetLength(1)];
        visited[x, y] = true;
        Queue<(int, int)> q = new Queue<(int, int)>();
        q.Enqueue((x, y));
        while (0 < q.Count)
        {
            (int, int) now = q.Dequeue();
            for (int i = 0; i < 4; ++i)
            {
                int newX = now.Item1 + xDir[i], newY = now.Item2 + yDir[i];
                if (!graph[newX, newY]) continue;
                else if (graph[newX, newY])
                {
                    graph[newX, newY] = graph[now.Item1, now.Item2];
                    q.Enqueue((newX, newY));
                }
            }

        }
    }
    // I    정수삼각형
    static void Baek1932()
    {
        int N = int.Parse(sr.ReadLine());
        int[,] dp = new int[N, N];
        dp[0, 0] = int.Parse(sr.ReadLine());
        for (int i = 1; i < N; ++i)
        {
            int[] input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            for (int j = 0; j < input.Length; ++j)
            {
                if (j == 0) // 맨 왼쪽
                    dp[i, j] = dp[i - 1, j] + input[j];
                else if (j == input.Length - 1) // 맨 오른쪽
                    dp[i, j] = dp[i - 1, j - 1] + input[j];
                else
                    dp[i, j] = Math.Max(dp[i - 1, j] + input[j], dp[i - 1, j - 1] + input[j]);
            }
        }

        int max = int.MinValue;
        for (int i = 0; i < N; ++i)
        {
            if (max < dp[N - 1, i])
                max = dp[N - 1, i];
        }
        sw.WriteLine(max.ToString());
    }
    static void Baek1932Plus()
    {
        int N = int.Parse(sr.ReadLine());
        int[][] dp = new int[N][];
        dp[0] = new int[1];
        dp[0][0] = int.Parse(sr.ReadLine());
        for (int i = 1; i < N; ++i)
        {
            int[] input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            dp[i] = new int[input.Length];
            for (int j = 0; j < input.Length; ++j)
            {
                if (j == 0)
                    dp[i][j] = dp[i - 1][j] + input[j];
                else if (j == input.Length - 1)
                    dp[i][j] = dp[i - 1][j - 1] + input[j];
                else
                    dp[i][j] = Math.Max(dp[i - 1][j - 1] + input[j], dp[i - 1][j] + input[j]);
            }
        }

        int max = 0;
        for (int i = 0; i < N; ++i)
        {
            if (max < dp[N - 1][i])
                max = dp[N - 1][i];
        }
        sw.WriteLine(max.ToString());
    }
    static void Baek1932Again()
    {
        int N = int.Parse(sr.ReadLine());
        int[,] dp = new int[N, N];
        for (int i = 0; i < N; ++i)
        {
            var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            for (int j = 0; j < i + 1; ++j)
            {
                if (i == 0) // i == 0일때 예외처리
                    dp[i, 0] = input[0];
                else if (j == 0)
                    dp[i, j] = dp[i - 1, j] + input[j];
                else if (j == i)
                    dp[i, j] = dp[i - 1, j - 1] + input[j];
                else
                    dp[i, j] = Math.Max(dp[i - 1, j] + input[j], dp[i - 1, j - 1] + input[j]);
            }
        }
        int max = int.MinValue;
        for (int i = 0; i < N; ++i)
        {
            if (max < dp[N - 1, i])
                max = dp[N - 1, i];
        }
        sw.WriteLine(max);
    }

    #endregion

    #region  Silber II
    // II   연결 요소의 개수
    static void Baek11724()
    {
        int[] NM = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int N = NM[0], M = NM[1];   // 정점:N, 간선:M
        List<int>[] graph = new List<int>[N + 1];

        for (int i = 1; i <= N; ++i)
            graph[i] = new List<int>();

        for (int i = 0; i < M; ++i)
        {
            int[] input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int u = input[0], v = input[1];
            graph[u].Add(v);
            graph[v].Add(u);
        }

        bool[] bfsCheck = new bool[N + 1];
        int result = 0;

        for (int i = 1; i <= N; ++i)
        {
            if (bfsCheck[i])
                continue;
            DFS_11724(graph, i, bfsCheck);
            result++;
        }
        sw.WriteLine(result.ToString());
    }
    static void DFS_11724(List<int>[] graph, int now, bool[] visited)
    {
        visited[now] = true;
        foreach (int next in graph[now])
        {
            if (visited[next])
                continue;
            DFS_11724(graph, next, visited);
        }
    }
    static void BFS_11724(List<int>[] graph, int start, bool[] visited)
    {
        visited[start] = true;

        Queue<int> q = new Queue<int>();
        q.Enqueue(start);
        while (0 < q.Count)
        {
            int now = q.Dequeue();
            foreach (int next in graph[now])
            {
                if (visited[next])
                    continue;
                q.Enqueue(next);
                visited[next] = true;
            }
        }
    }
    // II   최대 힙
    static void Baek11279()
    {
        // 우선순위 큐 | 1인자 : 값 / 2인자 : 우선순위
        PriorityQueue<int, int> queue = new PriorityQueue<int, int>(new decComparer());
        int N = int.Parse(sr.ReadLine());

        for (int i = 0; i < N; ++i)
        {
            int input = int.Parse(sr.ReadLine());
            if (0 == input)
            {
                if (queue.Count <= 0)
                    sb.AppendLine("0");
                else
                    sb.AppendLine(queue.Dequeue().ToString());
            }
            else
                queue.Enqueue(input, input);
        }

        sw.WriteLine(sb.ToString());
    }
    class decComparer : IComparer<int>
    {
        public int Compare(int a, int b)
        {
            return b.CompareTo(a);
        }
    }
    // II   색종이 만들기
    static void Baek2630()
    {
        // main
        int N = int.Parse(Console.ReadLine());
        sqaure = new byte[N, N];

        for (int i = 0; i < N; ++i)
        {
            byte[] input = Array.ConvertAll(Console.ReadLine().Split(), byte.Parse);
            for (int j = 0; j < N; ++j)
            {
                sqaure[i, j] = input[j];
            }
        }
        IsSquare(0, 0, N);
        Console.WriteLine(white + "\n" + blue);

    }
    static byte[,] sqaure;
    static int white = 0, blue = 0;
    static void IsSquare(int x, int y, int length)
    {
        byte first = sqaure[x, y];
        bool isSquare = true;
        for (int i = x; i < x + length; ++i)
        {
            for (int j = y; j < y + length; ++j)
            {
                if (sqaure[i, j] != first)
                {
                    isSquare = false;
                    break;
                }
            }
            if (!isSquare)
                break;
        }
        if (isSquare)
        {
            if (first == 0)
                white++;
            else
                blue++;
            return;
        }
        else
        {
            IsSquare(x, y, length / 2);
            IsSquare(x + length / 2, y, length / 2);
            IsSquare(x, y + length / 2, length / 2);
            IsSquare(x + length / 2, y + length / 2, length / 2);
        }

    }
    // II   마인크래프트
    static void Baek18111()
    {
        var NMB = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int N = NMB[0], M = NMB[1], B = NMB[2], min = int.MaxValue, minCount = 0;
        int[,] map = new int[N, M];
        for (int i = 0; i < N; ++i)
        {
            var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            for (int j = 0; j < M; ++j)
            {
                if (min == input[j])
                    minCount++;
                map[i, j] = input[j];
                if (min > input[j])
                {
                    min = input[j];
                    minCount = 1;
                }
            }
        }
        // if (minCount > M * N * 2)
    }

    #endregion

    #region Silver III
    // III  체스판 다시 칠하기
    static void Baek1018()
    {
        var NM = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int N = NM[0], M = NM[1], min = int.MaxValue;
        bool[,] map = new bool[N, M];
        for (int n = 0; n < N; ++n)
        {
            var input = sr.ReadLine();
            for (int m = 0; m < M; ++m)
                map[n, m] = input[m].ToString() == "W" ? true : false;
        }

        for (int x = 0; x < N - 7; ++x)
        {
            for (int y = 0; y < M - 7; ++y)
            {
                var count = CheckChess_1018(x, y, map, N, M);
                if (min > count)
                    min = count;
            }
        }

        sw.WriteLine(min.ToString());
    }
    static bool[,] BW = { { false, true, false, true, false, true, false, true },
                        { true, false, true, false, true, false, true, false },
                        { false, true, false, true, false, true, false, true },
                        { true, false, true, false, true, false, true, false },
                        { false, true, false, true, false, true, false, true },
                        { true, false, true, false, true, false, true, false },
                        { false, true, false, true, false, true, false, true },
                        { true, false, true, false, true, false, true, false } };
    static bool[,] WB = { { true, false, true, false, true, false, true, false },
                        { false, true, false, true, false, true, false, true },
                        { true, false, true, false, true, false, true, false },
                        { false, true, false, true, false, true, false, true },
                        { true, false, true, false, true, false, true, false },
                        { false, true, false, true, false, true, false, true },
                        { true, false, true, false, true, false, true, false },
                        { false, true, false, true, false, true, false, true } };
    static int CheckChess_1018(int x, int y, bool[,] map, int N, int M)
    {
        int bwCount = 0, wbCount = 0;
        for (int n = 0; n < 8; ++n)
        {
            for (int m = 0; m < 8; ++m)
            {
                if (map[n + x, m + y] != BW[n, m])
                    bwCount++;
                if (map[n + x, m + y] != WB[n, m])
                    wbCount++;
            }
        }
        return Math.Min(bwCount, wbCount);
    }

    #endregion

    #region Silver IV
    // IV   한수    
    static void Baek1065()
    {
        int N = int.Parse(sr.ReadLine());
        if (N < 100)
            sw.WriteLine(N);
        else
        {
            int count = 0;
            for (int i = 100; i <= N; ++i)
            {
                string num = i.ToString();
                sw.WriteLine("{0} - {1} == {1} - {2}", (num[2] - '0'), (num[1] - '0'), (num[0] - '0'));
                if ((num[2] - '0') - (num[1] - '0') == (num[1] - '0') - (num[0] - '0'))
                    count++;
            }
            sw.WriteLine(99 + count);
        }
    }

    #endregion

    #region Silver V
    // V    셀프 넘버
    static void Baek4673()
    {   // 1 3 5 7 9 20 31 42 53 64...
        for (int i = 1; i <= 10000; ++i)
        {   // self num인지 판단.
            if (i < 10)
            {
                if (i % 2 != 0)
                    sw.WriteLine(i);    // 1,3,5,7,9
            }
            else
            {
                bool self = true;
                for (int minus = 1; minus < i; ++minus)
                {
                    int temp = i - minus, plus = 0;
                    foreach (char n in temp.ToString())
                        plus += n - '0';
                    if (plus == minus)
                        self = false;
                }
                if (self)
                    sw.WriteLine(i);
            }
        }
    }
    // V    영화감독 숌
    static void Baek1436()
    {
        int N = int.Parse(sr.ReadLine()), start = 666, count = 0;
        while (count < N)
        {
            int is666 = 0;
            for (int i = 0; i < start.ToString().Length; ++i)
            {
                if (start.ToString()[i] == '6')
                {
                    is666++;
                    if (is666 == 3)
                    {
                        count++;
                        break;
                    }
                }
                else if (is666 > 0 && is666 != 3)
                    is666 = 0;
            }
            start++;
        }
        sw.WriteLine(start - 1);
    }
    // V    덩치
    static void Baek7568()
    {
        int N = int.Parse(sr.ReadLine());
        (int, int)[] p = new (int, int)[N];
        for (int i = 0; i < N; ++i)
        {
            var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            p[i] = (input[0], input[1]);
        }

        for (int i = 0; i < N; ++i)
        {
            int count = 1;
            for (int j = 0; j < N; ++j)
            {
                if (i != j && p[i].Item1 < p[j].Item1 && p[i].Item2 < p[j].Item2)
                    count++;
            }
            sw.Write($"{count} ");
        }
        sw.WriteLine();
    }

    #endregion
}