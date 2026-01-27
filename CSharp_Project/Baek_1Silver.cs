using System;
using System.Collections.Generic;
using System.IO;
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
    static public void Baek2178()
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
    static public void BFS_2178(int x, int y, bool[,] graph)
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
    static public void Baek1932()
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
    static public void Baek1932Plus()
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
    static public void Baek1932Again()
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

    #region  Silver II

    // II   쇠 막대기
    public static void Baek10799()
    {
        string input = Console.ReadLine();
        Stack<char> stack = new Stack<char>();

        // count: 계산할 총 막대의 수 / init: 계산할 첫 막대의 수 / sum: 막대의 총합
        int count = 0, init = 0, sum = 0;
        foreach (var c in input)
        {
            switch (c)
            {
                case '(':    // 1. '(' 일때
                    stack.Push(c);
                    count++;
                    init++;
                    break;
                case ')':    // 2. ')' 일때
                    count--;
                    init--;
                    if (stack.Peek() == '(')    // ()일때만 연산
                    {   // 레이저 자르기 연산
                        sum += count + init;
                        stack.Push(c);
                    }
                    init = 0;   // 첫 막대의 연산 종료후 초기화
                    break;
            }
        }
        Console.WriteLine($"{sum}");

    }
    // II   에디터  (Need Again)
    public static void Baek1406_TLE()
    {
        sb.Append(Console.ReadLine());
        int M = int.Parse(Console.ReadLine()), cursor = sb.Length;
        for (int i = 0; i < M; ++i)
        {
            var func = Console.ReadLine();
            switch (func[0])
            {
                case 'L':
                    if (cursor > 0)
                        cursor--;
                    break;
                case 'D':
                    if (cursor < sb.Length)
                        cursor++;
                    break;
                case 'B':   // 0X1Y2Z3
                    if (cursor == 0) continue;
                    // 커서가 맨 앞이 아닐 경우만
                    // input = input.ToString(0, cursor - 1) + input.ToString(cursor, input.Length - cursor);
                    sb.Remove(cursor - 1, 1);
                    cursor--;
                    break;
                case 'P':   // 0X1Y2Z3
                    // input = input.ToString(0, cursor) + func[2] + input.ToString(cursor, input.Length - cursor);
                    sb.Insert(cursor, func.Split()[1]);
                    cursor++;
                    break;
            }
        }
        Console.WriteLine(sb);
    }
    public static void Baek1406()
    {
        string input = Console.ReadLine();
        LinkedList<char> list = new LinkedList<char>();
        for (int i = 0; i < input.Length; ++i)
            list.AddLast(input[i]);
        list.AddLast(' ');
        int M = int.Parse(Console.ReadLine());
        LinkedListNode<char> cursor = list.Last;
        for (int i = 0; i < M; ++i)
        {
            var func = Console.ReadLine();
            switch (func[0])
            {
                case 'L':
                    if (null != cursor.Previous)
                        cursor = cursor.Previous;
                    break;
                case 'D':
                    if (null != cursor.Next)
                        cursor = cursor.Next;
                    break;
                case 'B':
                    if (null != cursor.Previous)
                        list.Remove(cursor.Previous);
                    break;
                case 'P':
                    list.AddBefore(cursor, func[2]);
                    break;
            }
        }
        foreach (var c in list)
            sb.Append(c);
        Console.WriteLine(sb);
    }
    // II   스택 수열
    public static void Baek1874()
    {
        int N = int.Parse(Console.ReadLine());
        int[] seq = new int[N];

        Stack<int> stack = new Stack<int>();
        for (int i = 0; i < N; ++i)
            seq[i] = int.Parse(Console.ReadLine());

        int index = 0, count = 0;
        while (index < N)
        {
            if (stack.Count == 0)
            {
                stack.Push(++count);
                sb.AppendLine("+");
            }
            if (stack.Peek() == seq[index])
            {   // pop 실행
                stack.Pop();
                sb.AppendLine("-");
                index++;
            }
            else
            {   // push 실행
                if (count == N)
                {
                    Console.WriteLine("NO");
                    return;
                }
                stack.Push(++count);
                sb.AppendLine("+");
            }
        }
        Console.Write(sb);
    }
    // II   연결 요소의 개수
    static public void Baek11724()
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
    static public void DFS_11724(List<int>[] graph, int now, bool[] visited)
    {
        visited[now] = true;
        foreach (int next in graph[now])
        {
            if (visited[next])
                continue;
            DFS_11724(graph, next, visited);
        }
    }
    static public void BFS_11724(List<int>[] graph, int start, bool[] visited)
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
    static public void Baek11279()
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
    static public void Baek2630()
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
    static public void IsSquare(int x, int y, int length)
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
    // II   마인크래프트    (Need Again)
    static public void Baek18111()
    {
        var NMB = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = NMB[0], M = NMB[1], B = NMB[2];

        int max = 0, min = 256, maxCount = 0;
        int[,] map = new int[N, M];
        for (int i = 0; i < N; ++i)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            for (int j = 0; j < M; ++j)
            {
                if (max == input[j]) maxCount++;
                if (max < input[j])
                {
                    max = input[j];
                    maxCount = 1;
                }
                if (min > input[j])
                    min = input[j];
                map[i, j] = input[j];
            }
        }

        int minCount = N * M - maxCount;
        if (min == max) // 작업이 필요하지 않은 경우
            Console.WriteLine($"0 {max}");
        else if (B >= minCount * (max - min))
        {   // 필요한 블럭개수가 충분한 경우 (불필요한 블럭 파괴가 필요없는 경우)
            if (maxCount >= M * N / 3)
                Console.WriteLine($"{minCount} {max}"); // 블럭을 설치만 할 경우
            else
                Console.WriteLine($"{maxCount * 2} {min}"); // 블럭을 제거만 할 경우
        }
        else
        {   // 블럭을 파거나 설치하는 작업량의 최소시간과 높이 구하기.
            int sum = 0;
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < M; ++j)
                {
                    if (max > map[i, j])    // 최대값 제외의 합 구하기
                        sum += map[i, j];
                }
            }

            int height = (max + B + sum) / (2 * minCount);
            int time = ((max - height) * maxCount * 2) + (height - min) * minCount;
            Console.WriteLine($"{time} {height}");
        }
    }

    #endregion

    #region Silver III

    // III  단어 뒤집기2
    public static void Baek17413()
    {
        var S = Console.ReadLine();
        Stack<char> stack = new Stack<char>();
        bool isTag = false;
        for (int i = 0; i < S.Length; ++i)
        {
            if ('<' == S[i])            // 1. 태그 시작
            {
                isTag = true;
                while (stack.Count > 0) // 1-1. Stack에 내용이 있다면 Append
                    sb.Append(stack.Pop());
                sb.Append('<');
            }
            else if ('>' == S[i])       // 3. 태그 종료
            {
                isTag = false;
                sb.Append('>');
            }
            else if (isTag)             // 2-1. Tag일때 -> 계속 Append처리
                sb.Append(S[i]);
            else if (' ' != S[i])       // 2-2. Tag아닐때 -> Stack에서 처리
                stack.Push(S[i]);
            else                        // 2-3. ' '일때 -> 단어의 끝임.
            {
                while (stack.Count > 0) // 2-3-1. Stack에 내용이 있다면 Append
                    sb.Append(stack.Pop());
                sb.Append(' ');
            }

            if (S.Length - 1 == i)      // 4. 마지막 단어일때 처리.
            {
                while (stack.Count > 0)
                    sb.Append(stack.Pop());
            }
        }
        Console.WriteLine(sb);
    }
    // III  퇴사        (Need Again)
    public static void Baek14501()
    {
        int N = int.Parse(Console.ReadLine());
        int[][] list = new int[N][];
        int[] dp = new int[N + 1];

        for (int i = 0; i < N; ++i)
            list[i] = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        for (int i = 0; i < N; ++i)
        {
            if (i + list[i][0] <= N)
                dp[i + list[i][0]] = Math.Max(dp[i + list[i][0]], list[i][1] + dp[i]);

            dp[i + 1] = Math.Max(dp[i + 1], dp[i]);
        }

        Console.WriteLine($"{dp[N]}");
    }

    // III  체스판 다시 칠하기
    static public void Baek1018()
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

    // IV   덱
    public static void Baek10866()
    {
        int N = int.Parse(Console.ReadLine());
        LinkedList<int> deque = new LinkedList<int>();
        for (int i = 0; i < N; ++i)
        {
            var input = Console.ReadLine().Split();
            switch (input[0])
            {
                case "push_front":
                    deque.AddFirst(int.Parse(input[1]));
                    break;
                case "push_back":
                    deque.AddLast(int.Parse(input[1]));
                    break;
                case "pop_front":
                    if (deque.Count == 0) sb.AppendLine("-1");
                    else
                    {
                        sb.AppendLine($"{deque.First()}");
                        deque.RemoveFirst();
                    }
                    break;
                case "pop_back":
                    if (deque.Count == 0) sb.AppendLine("-1");
                    else
                    {
                        sb.AppendLine($"{deque.Last()}");
                        deque.RemoveLast();
                    }
                    break;
                case "size":
                    sb.AppendLine($"{deque.Count()}");
                    break;
                case "empty":
                    sb.AppendLine($"{(deque.Count == 0 ? 1 : 0)}");
                    break;
                case "front":
                    sb.AppendLine($"{(deque.Count == 0 ? -1 : deque.First())}");
                    break;
                case "back":
                    sb.AppendLine($"{(deque.Count == 0 ? -1 : deque.Last())}");
                    break;
            }
        }
        Console.Write(sb);
    }
    // IV   요세푸스 문제
    public static void Baek1158()
    {
        var NM = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = NM[0], M = NM[1];
        Josephus1158(N, M);
    }
    static void Josephus1158(int N, int K)
    {   // 1 2 3 4 5 6 7
        Queue<int> queue = new Queue<int>();
        for (int i = 0; i < N; ++i)
            queue.Enqueue(1 + i);

        sb.Append('<');
        int count = 0;
        while (queue.Count > 0)
        {
            var front = queue.Dequeue();
            count++;
            if (0 == count % K)
            {
                if (queue.Count > 0)
                    sb.Append($"{front}, ");
                else
                    sb.Append(front);
            }
            else
                queue.Enqueue(front);
        }
        Console.WriteLine(sb.Append('>'));
    }
    // IV   큐
    public static void Baek10845()
    {
        int N = int.Parse(Console.ReadLine());
        int[] queue = new int[N];
        int front = 0, back = 0;
        for (int i = 0; i < N; ++i)
        {
            var input = Console.ReadLine().Split();
            switch (input[0])
            {
                case "push":
                    queue[back++] = int.Parse(input[1]);
                    break;
                case "pop":
                    if (back - front == 0)
                        sb.AppendLine("-1");
                    else
                        sb.AppendLine($"{queue[front++]}");
                    break;
                case "size":
                    sb.AppendLine($"{back - front}");
                    break;
                case "empty":
                    sb.AppendLine($"{(back - front == 0 ? 1 : 0)}");
                    break;
                case "front":
                    if (back - front == 0)
                        sb.AppendLine("-1");
                    else
                        sb.AppendLine($"{queue[front]}");
                    break;
                case "back":
                    if (back - front == 0)
                        sb.AppendLine("-1");
                    else
                        sb.AppendLine($"{queue[back - 1]}");
                    break;
            }
        }
        Console.Write(sb);
    }
    // IV   괄호
    public static void Baek9012()
    {
        int T = int.Parse(Console.ReadLine());
        for (int i = 0; i < T; ++i)
        {
            bool isTrue = true;
            Stack<char> stack = new Stack<char>();
            int count = 0;
            var input = Console.ReadLine();
            foreach (var symbol in input)
            {
                if (stack.Count == 0)
                {
                    stack.Push(symbol);
                    count++;
                    continue;
                }
                if (symbol == '(')
                {
                    stack.Push('(');
                    count++;
                }
                else if (stack.Peek() == '(')
                {
                    stack.Pop();
                    count--;
                }
                else if (stack.Count == 0)
                    isTrue = false;
            }

            Console.WriteLine($"{(count == 0 && isTrue ? "Yes" : "No")}");

        }
    }
    // IV   스택
    public static void Baek10828()
    {
        int N = int.Parse(Console.ReadLine()), count = 0;
        int[] stack = new int[N];

        for (int i = 0; i < N; ++i)
        {
            var input = Console.ReadLine().Split();
            if (input.Length > 1)   // push일때
            {
                stack[count++] = int.Parse(input[1]);
            }
            else
            {
                switch (input[0])
                {
                    case "top":
                        sb.AppendLine($"{(count == 0 ? -1 : stack[count - 1])}");
                        // Console.WriteLine($"{(count == 0 ? -1 : stack[count - 1])}");
                        break;
                    case "size":
                        sb.AppendLine($"{count}");
                        // Console.WriteLine($"{count}");
                        break;
                    case "empty":
                        sb.AppendLine($"{(count == 0 ? 1 : 0)}");
                        // Console.WriteLine($"{(count == 0 ? 1 : 0)}");
                        break;
                    case "pop":
                        sb.AppendLine($"{(count == 0 ? -1 : stack[--count])}");
                        // Console.WriteLine($"{(count == 0 ? -1 : stack[--count])}");
                        break;
                }
            }
        }
        Console.WriteLine(sb);
    }
    // IV   설탕배달
    public static void Baek2839()
    {
        int N = int.Parse(Console.ReadLine()), min = int.MaxValue;
        if (N == 3)
        {   // 예외처리
            Console.WriteLine($"1");
            return;
        }
        for (int i = 0; i < N / 2; ++i)
        {
            for (int j = 0; j < N / 2; ++j)
            {
                if (i * 5 + j * 3 == N)
                    min = Math.Min(min, i + j);
            }
        }
        Console.WriteLine($"{(min == int.MaxValue ? -1 : min)}");
    }
    // IV   한수    
    static public void Baek1065()
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
    static public void Baek4673()
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
    static public void Baek1436()
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
    static public void Baek7568()
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