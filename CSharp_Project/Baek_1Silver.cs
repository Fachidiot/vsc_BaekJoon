using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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

    // I    1, 2, 3 더하기 5    (Need Again)
    public static void Baek15990()
    {
        // 마지막에 더할 숫자를 1, 2, 3으로 구분지어 경우의수 계산
        // 1 : 1                -> [1, 1] = 1
        // 2 : 2                -> [2, 1] = 0, [2, 2] = 1
        // 3 : 1 + 2, 2 + 1, 3  -> [3, 1] = 1, [3, 2] = 1, [3, 3] = 1

        int[,] dp = new int[100001, 4];     // 100000까지 저장할 dp 배열
        dp[1, 1] = dp[2, 2] = dp[3, 1] = dp[3, 2] = dp[3, 3] = 1;   // 기본값

        for (int i = 4; i < 100001; ++i)
        {   // 연산 수행 : 1, 2, 3을 마지막에 더해줄 경우의 수를 저장해주기.
            dp[i, 1] = (dp[i - 1, 2] + dp[i - 1, 3]) % 1_000_000_009;
            dp[i, 2] = (dp[i - 2, 1] + dp[i - 2, 3]) % 1_000_000_009;
            dp[i, 3] = (dp[i - 3, 1] + dp[i - 3, 2]) % 1_000_000_009;
        }
        // 4의 1일때 : 3을 만들수 있는 경우의수중 2, 3이 가장 마지막에 더해줄때를 더해준다.
        // 4의 2일때 : 3을 만들수 있는 경우의수중 1, 3이 가장 마지막에 더해줄때를 더해준다.
        // 4의 3일때 : 3을 만들수 있는 경우의수중 1, 2이 가장 마지막에 더해줄때를 더해준다.

        int t = int.Parse(Console.ReadLine());
        while (t-- > 0)
        {
            int n = int.Parse(Console.ReadLine());
            long result = ((long)dp[n, 1] + dp[n, 2] + dp[n, 3]) % 1_000_000_009L;  // 연산하려는 숫자 하나만 캐스팅하고 연산
            sb.AppendLine(result.ToString());
        }
        Console.WriteLine(sb);
    }
    // I    쉬운 계단 수
    public static void Baek10844()
    {
        int[,] dp = new int[101, 10];
        for (int i = 1; i < 10; ++i)    // 1 ~ 9 기본값 세팅
            dp[1, i] = 1;

        for (int i = 2; i < 101; ++i)   // 2자리수 부터 계산 시작.
        {
            for (int j = 1; j < 9; ++j) // 1 ~ 9가 마지막 전자리일때의 경우의 수 계산
                dp[i, j] = (dp[i - 1, j - 1] + dp[i - 1, j + 1]) % 1_000_000_000;   // 점화식
            dp[i, 0] = dp[i - 1, 1] % 1_000_000_000;    // 0일때 예외 처리
            dp[i, 9] = dp[i - 1, 8] % 1_000_000_000;    // 9일때 예외 처리
        }

        int n = int.Parse(Console.ReadLine());
        long result = 0;
        for (int i = 0; i < 10; ++i)    // 결과값 더해주기.
            result += dp[n, i];
        Console.WriteLine(result % 1_000_000_000L);     // 출력
    }
    // I    카드 구매하기
    public static void Baek11052()
    {
        int n = int.Parse(Console.ReadLine());
        var p = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] dp = new int[n + 1];  // dp[1 ~ n]

        for (int i = 1; i <= n; ++i)        // 카드 i장의 최대 크기를 저장.
        {
            for (int j = 1; j <= i; ++j)    // 카드팩 j번과 이전 크기의 최대값의 합중 최대값을 저장.    
                dp[i] = Math.Max(p[j - 1] + dp[i - j], dp[i]);  // p는 0번 인덱스부터 시작하므로 j - 1로 구해줌.
        }

        Console.WriteLine(dp[n]);
    }
    // I    카드 구매하기 2
    public static void Baek16194()
    {
        int n = int.Parse(Console.ReadLine());
        var p = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] dp = new int[n + 1];
        Array.Fill(dp, int.MaxValue);
        dp[0] = 0;

        for (int i = 1; i <= n; ++i)
        {
            for (int j = 1; j <= i; ++j)
                dp[i] = Math.Min(dp[i], p[j - 1] + dp[i - j]);
        }
        Console.WriteLine($"{dp[n]}");
    }
    // I    골드바흐의 추측
    public static void Baek6588()
    {
        bool[] nums = new bool[1000001];
        nums[0] = nums[1] = true;  // 0, 1은 소수가 아님
        for (int i = 2; i * i <= 1000000; ++i)
        {
            if (nums[i]) continue;
            for (int j = i * i; j <= 1000000; j += i)
            {   // 2 * 2 부터 소수가 아닌 수를 판별
                nums[j] = true;
            }
        }

        while (true)
        {
            int n = int.Parse(Console.ReadLine());
            if (n == 0)
                break;

            for (int i = 2; i <= n / 2; ++i)
            {
                if (!nums[i] && !nums[n - i])
                {
                    sb.AppendLine($"{n} = {i} + {n - i}");
                    break;
                }
            }
        }
        Console.WriteLine(sb);

    }
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
    static public void Baek1932_Plus()
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
    static public void Baek1932_Again()
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

    #region Silver II

    // II   가장 긴 증가하는 부분 수열
    public static void Baek11053()
    {
        int n = int.Parse(Console.ReadLine());
        int max = 1;
        int[] A = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] dp = new int[n];

        for (int i = 0; i < n - 1; ++i)
        {
            int temp = A[i];
            dp[i] = 1;
            for (int j = i + 1; j < n; ++j)
            {
                if (temp < A[j])
                {
                    temp = A[j];
                    ++dp[i];
                }
            }
            if (max < dp[i])
                max = dp[i];
        }
        Console.WriteLine(max);
    }
    // II   골드바흐 파티션
    public static void Baek17103()
    {   // 에라토스테네스의 체
        bool[] nums = new bool[1000001];
        nums[0] = nums[1] = true;
        for (int i = 2; i < 1000; ++i)
        {
            for (int j = i * i; j < 1000001; j += i)
            {
                if (nums[j]) continue;
                if (i != j && j % i == 0)
                    nums[j] = true;
            }
        }

        int T = int.Parse(Console.ReadLine());
        while (T-- > 0)
        {
            int N = int.Parse(Console.ReadLine()), count = 0;
            for (int i = 2; i <= N / 2; ++i)
            {
                if (!nums[i] && !nums[N - i])
                    ++count;
            }
            sb.AppendLine($"{count}");
        }
        Console.WriteLine($"{sb}");
    }
    // II   -2진수  (Need Again)
    static void TenToMinus2_2089(int n)
    {
        int r = n % -2;
        n /= -2;
        if (r < 0)
        {   // 나머지값이 음수일때
            r += 2;
            n += 1;
        }
        if (n == 0)
        {   // 더이상 나눌 수가 아닐때부터 Append 시작
            sb.Append(r);
            return;
        }
        TenToMinus2_2089(n);
        sb.Append(r);
    }
    public static void Baek2089()
    {
        int N = int.Parse(Console.ReadLine());
        TenToMinus2_2089(N);
        Console.WriteLine(sb);
    }
    // II   숨바꼭질 6
    static int Euclidean_17087(int a, int b)
    {
        int r = a % b;
        if (r == 0)
            return b;
        return Euclidean_17087(b, r);
    }
    public static void Baek17087()
    {
        // 동생수 : N / 현재 위치 : S
        var NS = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = NS[0], S = NS[1];

        // 동생들의 위치 입력 받기
        var A = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        for (int i = 0; i < A.Length; ++i)  // 현재 좌표와의 차이값의 절대값을 구해줌.
            A[i] = Math.Abs(A[i] - S);

        // 최종적인 좌표끼리의 최대 공약수를 구해준다.
        for (int i = 0; i < A.Length - 1; ++i)
            A[i + 1] = Euclidean_17087(A[i], A[i + 1]);
        Console.WriteLine($"{A[A.Length - 1]}");
    }
    // II   조합 0의 개수   (Need Again)
    public static void Baek2004()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int n = input[0], m = input[1];

        // n 팩토리얼의 2와 5의 개수
        long n2 = 0, n5 = 0;
        // m 팩토리얼의 2와 5의 개수
        long m2 = 0, m5 = 0;
        // n-m 팩토리얼의 2와 5의 개수
        long nm2 = 0, nm5 = 0;

        // 르장드르 공식 (N!팩토리얼을 소인수분해시 특정 소수가 몇번 곱해지는지를 구하는 공식)
        // 소수의 제곱수를 이용한 풀이 방법.
        // 2,4,8,16...
        for (long i = 2; i <= n; i *= 2)
        {
            if (i <= n) n2 += n / i;
            if (i <= m) m2 += m / i;
            if (i <= n - m) nm2 += (n - m) / i;
        }
        // 5,25,125,625...
        for (long i = 5; i <= n; i *= 5)
        {
            if (i <= n) n5 += n / i;
            if (i <= m) m5 += m / i;
            if (i <= n - m) nm5 += (n - m) / i;
        }

        Console.WriteLine($"{Math.Min(n2 - m2 - nm2, n5 - m5 - nm5)}");
    }
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
        IsSquare_2630(0, 0, N);
        Console.WriteLine(white + "\n" + blue);

    }
    static byte[,] sqaure;
    static int white = 0, blue = 0;
    static public void IsSquare_2630(int x, int y, int length)
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
            IsSquare_2630(x, y, length / 2);
            IsSquare_2630(x + length / 2, y, length / 2);
            IsSquare_2630(x, y + length / 2, length / 2);
            IsSquare_2630(x + length / 2, y + length / 2, length / 2);
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

    // III  이친수
    public static void Baek2193()
    {
        long[,] dp = new long[91, 2];
        dp[1, 1] = dp[2, 0] = 1;
        for (int i = 3; i < 91; ++i)
        {
            dp[i, 0] = dp[i - 1, 0] + dp[i - 1, 1];
            dp[i, 1] = dp[i - 1, 0];
        }

        int n = int.Parse(Console.ReadLine());
        Console.WriteLine(dp[n, 0] + dp[n, 1]);
    }
    // III  1, 2, 3, 더하기
    public static void Baek9095()
    {
        int[] dp = new int[12];
        dp[1] = 1;
        dp[2] = 2;
        dp[3] = 4;
        for (int i = 4; i <= 11; ++i)
            dp[i] = dp[i - 1] + dp[i - 2] + dp[i - 3];

        int T = int.Parse(Console.ReadLine());
        while (T-- > 0)
        {
            int n = int.Parse(Console.ReadLine());
            sb.AppendLine($"{dp[n]}");
        }
        Console.WriteLine(sb);
    }
    // III  2 x n 타일링
    public static void Baek11726()
    {
        int n = int.Parse(Console.ReadLine());
        int[] dp = new int[n + 1];
        dp[0] = dp[1] = 1;

        for (int i = 2; i <= n; ++i)
            dp[i] = (dp[i - 1] + dp[i - 2]) % 10007;
        Console.WriteLine($"{dp[n]}");
    }
    // III  2 x n 타일링 2
    public static void Baek11727()
    {
        int n = int.Parse(Console.ReadLine());
        int[] dp = new int[n + 1];
        dp[0] = dp[1] = 1;

        for (int i = 2; i <= n; ++i)
            dp[i] = (dp[i - 1] + dp[i - 2] * 2) % 10007;
        Console.WriteLine($"{dp[n]}");
    }
    // III  1로 만들기  (Need Again)
    public static void Baek1463()
    {
        long X = long.Parse(Console.ReadLine());
        if (X < 3)
        {
            if (1 == X)
                Console.WriteLine(0);
            else
                Console.WriteLine(1);
            return;
        }

        int[] dp = new int[X + 1];
        dp[1] = dp[2] = dp[3] = 1;

        for (int i = 3; i <= X; ++i)
        {
            if (dp[i] != 0) continue;
            int min = dp[i - 1] + 1;
            if (i % 2 == 0)
                min = Math.Min(min, dp[i / 2] + 1);
            if (i % 3 == 0)
                min = Math.Min(min, dp[i / 3] + 1);

            dp[i] = min;
        }
        Console.WriteLine($"{dp[X]}");
    }
    // III  소수 구하기 (Need Again)
    public static void Baek1929Plus()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int M = input[0], N = input[1];

        for (int i = M; i <= N; ++i)
        {
            if (IsPrime_1929(i))
                sb.AppendLine(i.ToString());
        }
        Console.WriteLine(sb);
    }
    static bool IsPrime_1929(int n)
    {
        if (n < 2) return false;
        if (n == 2) return true;
        if (n % 2 == 0) return false;

        // 
        for (int i = 3; i * i <= n; i += 2)
        {
            if (n % i == 0)
                return false;
        }
        return true;
    }
    public static void Baek1929()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int M = input[0], N = input[1];

        // TLE
        // for (int i = M; i <= N; ++i)
        // {
        //     for (int j = 2; j <= i; ++j)
        //     {
        //         if (j + 1 == i)
        //             sb.AppendLine($"{i}");
        //         if (i % j == 0)
        //             break;
        //     }
        // }
        // Console.WriteLine(sb);

        bool[] nums = new bool[1_000_001];
        nums[0] = nums[1] = true;  // 0, 1은 소수가 아님
        for (int i = 2; i * i <= N; ++i)
        {
            if (nums[i]) continue;
            for (int j = i * i; j <= N; j += i)
            {   // 2 * 2 부터 소수가 아닌 수를 판별
                nums[j] = true;
            }
        }
        for (int i = M; i <= N; ++i)
        {
            if (!nums[i])
                sb.AppendLine(i.ToString());
        }
        Console.WriteLine(sb);
    }
    // III  후위 표기식2
    public static void Baek1935()
    {
        int N = int.Parse(Console.ReadLine());
        string input = Console.ReadLine();
        int[] nums = new int[N];
        Stack<double> stack = new Stack<double>();

        for (int i = 0; i < N; ++i)
            nums[i] = int.Parse(Console.ReadLine());
        for (int i = 0; i < input.Length; ++i)
        {
            double a = 0, b = 0;
            if ('+' == input[i] || '-' == input[i] || '*' == input[i] || '/' == input[i])
            {
                b = stack.Pop();
                a = stack.Pop();
                if ('+' == input[i])
                    stack.Push(a + b);
                if ('-' == input[i])
                    stack.Push(a - b);
                if ('*' == input[i])
                    stack.Push(a * b);
                if ('/' == input[i])
                    stack.Push(a / b);
            }
            else
                stack.Push(nums[input[i] - 'A']);
        }
        Console.WriteLine($"{stack.Pop().ToString("F2")}");
    }
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

    // IV   보물
    public static void Baek1026()
    {
        int N = int.Parse(Console.ReadLine()), sum = 0;
        int[] A = new int[N], B = new int[N];

        A = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        Array.Sort(A, (num1, num2) => num2.CompareTo(num1));    // 내림차순
        B = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        Array.Sort(B);  // 오름차순

        for (int i = 0; i < N; ++i)
            sum += A[i] * B[i];
        Console.WriteLine($"{sum}");
    }
    // IV   GCD 합
    static int Euclidean_9613(int a, int b)
    {
        int r = a % b;
        if (r == 0)
            return b;
        return Euclidean_9613(b, r);
    }
    public static void Baek9613()
    {
        int T = int.Parse(Console.ReadLine());
        while (T > 0)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int n = input[0];
            long sum = 0;

            for (int i = 1; i < n; ++i)
            {
                for (int j = n; j > i; --j)
                    sum += Euclidean_9613(input[i], input[j]);
            }
            sb.AppendLine(sum.ToString());

            --T;
        }
        Console.WriteLine(sb);
    }
    // IV   접미사 배열
    public static void Baek11656()
    {
        var input = Console.ReadLine();
        string[] array = new string[input.Length];

        for (int i = 0; i < input.Length; ++i)
        {
            for (int j = 0; j < input.Length - i; ++j)
                array[i] += input[i + j];
        }
        Array.Sort(array);
        foreach (var s in array)
            sb.AppendLine(s);

        Console.WriteLine(sb);
    }
    // IV   에라토스테네스의 채
    public static void Baek2960()
    {
        // 2부터 N까지 모든 정수를 적는다.
        // 아직 지우지 않은 수 중 가장 작은 수를 찾는다. 이것을 P라고 하고, 이 수는 소수이다.
        // P를 지우고, 아직 지우지 않은 P의 배수를 크기 순서대로 지운다.
        // 아직 모든 수를 지우지 않았다면, 다시 2번 단계로 간다.
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = input[0], K = input[1], count = 0;
        bool[] nums = new bool[N + 1];

        for (int i = 2; i <= N; ++i)
        {   // i는 P값이 된다.
            for (int j = i; j <= N; j += i)
            {   // j는 P ~ (P의 배수 < N)N보다 작은 P의 배수까지의 수다.
                if (!nums[j])
                {   // 걸러지지 않은 상태라면 거르자.
                    count++;        // 몇번째로 거르는지 확인
                    nums[j] = true; // 거른다.
                    if (count == K) // K번째로 거르는 수라면
                    {   // 출력 및 loop 종료
                        Console.WriteLine($"{j}");
                        return; // 종료
                    }
                }
            }
        }
    }
    // IV   카드2
    public static void Baek2164()
    {
        Queue<int> que = new Queue<int>();
        int N = int.Parse(Console.ReadLine()), cnt = 0;
        for (int i = 0; i < N; ++i)
            que.Enqueue(i + 1);

        while (que.Count != 1)
        {
            var temp = que.Dequeue();
            if (cnt % 2 == 1)
                que.Enqueue(temp);
            cnt++;
        }
        Console.WriteLine($"{que.Dequeue()}");
    }
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
        Josephus_1158(N, M);
    }
    static void Josephus_1158(int N, int K)
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
        Stack<int> stack = new Stack<int>();
        int n = int.Parse(Console.ReadLine());
        while (n-- > 0)
        {
            var input = Console.ReadLine().Split();
            switch (input[0])
            {
                case "push":
                    stack.Push(int.Parse(input[1]));
                    break;
                case "pop":
                    if (stack.Count == 0)
                        Console.WriteLine(-1);
                    else
                        Console.WriteLine(stack.Pop());
                    break;
                case "size":
                    Console.WriteLine(stack.Count);
                    break;
                case "empty":
                    if (stack.Count == 0)
                        Console.WriteLine(1);
                    else
                        Console.WriteLine(0);
                    break;
                case "top":
                    if (stack.Count == 0)
                        Console.WriteLine(-1);
                    else
                        Console.WriteLine(stack.Peek());
                    break;
            }
        }
    }
    public static void Baek10828_OLD()
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

    // V    크로아티아 알파벳
    public static void Baek2941()
    {
        var input = Console.ReadLine();
        int count = 0;

        for (int i = 0; i < input.Length; ++i)
        {
            switch (input[i])
            {
                case 'd':
                    if (i < input.Length - 2 && input[i + 1] == 'z' && input[i + 2] == '=') continue;
                    break;
                case 'l':
                    if (i < input.Length - 1 && input[i + 1] == 'j') continue;
                    break;
                case 'n':
                    if (i < input.Length - 1 && input[i + 1] == 'j') continue;
                    break;
                case '-':
                    continue;
                case '=':
                    continue;
            }
            ++count;
        }
        Console.WriteLine(count);
    }
    public static void Baek2941_Simple()
    {
        sb.Clear();
        sb.Append(Console.ReadLine());
        sb.Replace("dz=", "d");
        sb.Replace("lj", "l");
        sb.Replace("nj", "n");
        sb.Replace("=", "");
        sb.Replace("-", "");
        Console.WriteLine(sb.Length);
    }
    // V    배열 합치기
    public static void Baek11728()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int n = input[0], m = input[1], index = 0;

        int[] A = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] B = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] C = new int[n + m];

        foreach (var a in A)
            C[index++] = a;
        foreach (var b in B)
            C[index++] = b;

        Array.Sort(C);
        foreach (var c in C)
            Console.Write($"{c} ");
    }
    // V    너의 평점은
    struct Student_25026
    {
        float[] grades; // 학점
        float[] ranks;  // 등급
        int index;

        public Student_25026(int length)
        {
            grades = new float[length];
            ranks = new float[length];
            index = 0;
        }
        public void InsertData(float grade, float rank)
        {
            grades[index] = grade;
            ranks[index++] = rank;
        }
        public float GetSum()
        {
            float sum = 0;
            for (int i = 0; i < grades.Length; ++i)
                sum += grades[i] * ranks[i];
            return sum;
        }
        public float GetGradeSum()
        {
            float sum = 0;
            foreach (var grade in grades)
                sum += grade;
            return sum;
        }
    }
    public static void Baek25206()
    {
        Student_25026 student = new Student_25026(20);
        for (int i = 0; i < 20; ++i)
        {
            var input = Console.ReadLine().Split();
            float rank = 0;
            if ("P" == input[2])    // P일경우 점수 합산에 미포함.
                continue;
            else if ("F" == input[2])
                rank = 0;
            else
                rank = 4 - (input[2][0] - 65) + (input[2][1] == '+' ? 0.5f : 0);

            student.InsertData(float.Parse(input[1]), rank);
        }

        float sum = student.GetSum();
        float gradeSum = student.GetGradeSum();

        if (gradeSum == 0)
            Console.WriteLine(gradeSum.ToString("F6"));
        else
            Console.WriteLine((sum / gradeSum).ToString("F6"));
    }
    // V    성적 통계
    public static void Baek5800()
    {
        int K = int.Parse(Console.ReadLine());
        for (int i = 0; i < K; ++i)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int N = input[0], maxGap = 0;
            int[] grades = new int[N], gaps = new int[N - 1];

            for (int j = 0; j < N; ++j)
                grades[j] = input[j + 1];
            Array.Sort(grades);
            for (int j = 1; j < N; ++j)
            {
                gaps[j - 1] = grades[j] - grades[j - 1];
                if (maxGap < gaps[j - 1])
                    maxGap = gaps[j - 1];
            }
            Console.WriteLine($"Class {i + 1}\nMax {grades[N - 1]}, Min {grades[0]}, Largest gap {maxGap}");
        }
    }
    // V    인기 투표
    public static void Baek11637()
    {
        int T = int.Parse(Console.ReadLine());
        while (T-- > 0)
        {
            int N = int.Parse(Console.ReadLine()), total = 0;
            int[] p = new int[N], max = new int[2];    // max[0] : 투표수, max[1] : 번호
            for (int i = 0; i < N; ++i)
            {
                p[i] = int.Parse(Console.ReadLine());
                if (max[0] < p[i])
                {
                    max[0] = p[i];
                    max[1] = i;
                }
                total += p[i];
            }

            for (int i = 0; i < N; ++i)
            {
                if (i != max[1])
                {
                    if (p[i] == max[0])
                    {   // No Winner
                        Console.WriteLine("no winner");
                        break;
                    }
                }
                if (i == N - 1)
                {
                    if (total / 2 < max[0])    // 과반수 이상일 경우
                        Console.WriteLine($"majority winner {max[1] + 1}");
                    else                  // 과반수 이하일 경우
                        Console.WriteLine($"minority winner {max[1] + 1}");
                }
            }
        }
    }
    // V    Base Conversion
    static int TenToB_11576(int N, int B, int r = 1)
    {
        r *= B;
        if (r < N)
            N = TenToB_11576(N, B, r);
        r /= B;
        if (r > N)
        {
            sb.Append("0 ");
            return N;
        }
        else
        {
            int d = N / r;
            sb.Append($"{d} ");
            return N % r;
        }
    }
    public static void Baek11576()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int A = input[0], B = input[1], m = int.Parse(Console.ReadLine());
        input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        int N = 0, index = 0;
        while (m-- > 0) // A진법의 미지의수를 10진수로
            N += (int)Math.Pow(A, m) * input[index++];

        // 미지의수 N을 B진법 변환
        TenToB_11576(N, B);
        sb.Remove(sb.Length - 1, 1);
        Console.WriteLine(sb);
    }
    // V    팩토리얼 0의 개수
    public static void Baek1676()
    {   // 숫자 N을 입력받기, 개수를 셀 count변수 생성
        int N = int.Parse(Console.ReadLine()), count = 0;

        // Factorial(N) : 1 ~ N까지의 숫자를 곱함 => 5를 곱하는 횟수 구하기 (2 * 5 = 10)
        for (int i = 1; i <= N; ++i)
        {   // i % 5 : 5가 1개, i % 25 0 : 5가 2개, i % 125 : 5가 3개, i % 625 : 5가 4개
            if (i % 5 == 0) ++count;
            if (i % 25 == 0) ++count;
            if (i % 125 == 0) ++count;
            // if (i % 625 == 0) ++count;   // N은 <= 500 이니깐 125까지의 횟수를 구하자.
        }
        Console.WriteLine($"{count}");
    }
    // V    분수찾기
    public static void Baek1193()
    {
        int X = int.Parse(Console.ReadLine());
    }
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
    public struct Person
    {
        int weight, height;
        public Person(int w, int h)
        {
            weight = w;
            height = h;
        }
        public bool CompareWith(Person another)
        {
            return weight < another.weight && height < another.height;
        }
    }

    #endregion
}