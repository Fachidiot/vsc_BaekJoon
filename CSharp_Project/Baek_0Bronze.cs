using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

class Baek_0Bronze
{
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
    static StreamReader sr = new StreamReader(Console.OpenStandardInput());
    static StringBuilder sb = new StringBuilder();

    ~Baek_0Bronze()
    {
        sr.Close();
        sw.Close();
    }

    #region Bronze I

    // I    2007년
    public static void Baek1924()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] days = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };    // 각 달의 일수 저장
        string[] dates = { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT" };   // 최종 요일 저장
                                                                                // 0      1     2      3      4      5      6
        int month = input[0], day = input[1];

        while (--month > 0)         // 자기 자신의 달을 제외하기 위한 전위감소 실행
            day += days[month - 1]; // 배열이니까 -1한 index값의 일수를 더해주기.
        Console.WriteLine($"{dates[day % 7]}"); // 모든 일수를 구했다면 7의 나머지가 요일이된다.
    }
    // I    일곱 난쟁이
    public static void Baek2309()
    {
        List<int> dwarfs = new List<int>();
        int sum = 0, num;
        for (int i = 0; i < 9; ++i)
        {
            dwarfs.Add(int.Parse(Console.ReadLine()));
            sum += dwarfs[i];
        }
        dwarfs.Sort();

        num = sum - 100;
        for (int i = 0; i < 9 - 1; ++i)
        {
            for (int j = 0; j < 9; ++j)
            {
                if (i == j) continue;
                if (num == dwarfs[i] + dwarfs[j])
                {
                    var temp1 = dwarfs[i];
                    var temp2 = dwarfs[j];
                    dwarfs.Remove(temp1);
                    dwarfs.Remove(temp2);
                    j = 9;
                    break;
                }

            }
            if (dwarfs.Count == 7)
            {
                foreach (var dwarf in dwarfs)
                {
                    Console.WriteLine(dwarf.ToString());
                }
                break;
            }
        }
    }
    public static void BAek2309_Array()
    {
        int[] dwarfs = new int[9];
        int sum = 0;
        for (int i = 0; i < 9; ++i)
        {
            dwarfs[i] = int.Parse(Console.ReadLine());
            sum += dwarfs[i];
        }

        Array.Sort(dwarfs);
        int num = sum - 100;    // 난쟁이 9명의 총합의 100을 뺀 값의 난쟁이의 키값을 찾자.
        for (int i = 0; i < 9; ++i)
        {
            for (int j = i + 1; j < 9; ++j)
            {
                if (num == dwarfs[i] + dwarfs[j])
                {
                    for (int k = 0; k < 9; ++k)
                    {
                        if (k == i || k == j) continue;
                        Console.WriteLine(dwarfs[k]);
                    }
                    return;
                }
            }
        }
    }
    // I    소인수분해
    public static void Baek11653()
    {
        int N = int.Parse(Console.ReadLine()), divider = 2;
        while (N > 1)
        {
            if (N % divider != 0)
                ++divider;
            else
            {
                N /= divider;
                sb.AppendLine($"{divider}");
            }
        }
        Console.WriteLine(sb);
    }
    // I    진법 변환 2
    static int TenToB_11005(int n, int b, long r = 1)
    {
        r *= b;
        if (n >= r)  // 더 큰 지수로 나눌수 있다면 나눠줌
            n = TenToB_11005(n, b, r);

        r /= b;
        int t = n / (int)r;
        if (t >= 10)
            sb.Append($"{(char)('7' + t)}");
        else
            sb.Append($"{t}");

        return n % (int)r;
    }
    public static void Baek11005()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = input[0], B = input[1];

        TenToB_11005(N, B);
        Console.WriteLine(sb);
    }
    // I    2진수 8진수 (TLE)
    public static void Baek1373()
    {
        var input = Console.ReadLine();
        int first = input.Length % 3;

        if (first > 0)
        {
            int temp = 0;
            for (int i = 0; i < first; ++i)
                temp = (temp << 1) + (input[i] - '0');
            sb.Append(temp);
        }
        for (int i = first; i < input.Length; i += 3)
        {
            int temp = (input[i] - '0') * 4 +
                    (input[i + 1] - '0') * 2 +
                    (input[i + 2] - '0');
            sb.Append(temp);
        }
        Console.WriteLine(sb);
    }
    static string TenToEight_1373(BigInteger num)
    {   // 8진수로 변환
        BigInteger r = num % 8;
        BigInteger a = num / 8;
        if (a >= 8)
            return TenToEight_1373(a) + r;
        return (a != 0 ? a.ToString() : "") + r.ToString();
    }
    public static void Baek1373_TLE()
    {
        string input = Console.ReadLine();
        BigInteger num = 0;
        for (int i = 0; i < input.Length; ++i)
        {   // 10진수 변환
            num = num << 1;
            if (input[i] == '1')
                ++num;
        }
        Console.WriteLine($"{TenToEight_1373(num)}");
    }
    // I    평균
    public static void Baek1546()
    {
        double N = double.Parse(Console.ReadLine()), max = double.MinValue, sum = 0;
        var input = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);

        for (int i = 0; i < N; ++i)
        {
            if (max < input[i])
                max = input[i];
        }
        for (int i = 0; i < N; ++i)
            sum += input[i] / max * 100;
        Console.WriteLine($"{sum / N}");
    }
    // I    최소공배수 (Need Again)
    public static void Baek1934()
    {
        int T = int.Parse(Console.ReadLine());
        for (int i = 0; i < T; ++i)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int A = input[0], B = input[1];
            sb.AppendLine($"{(A * B) / Euclidean_1934(A, B)}");
        }
        Console.WriteLine(sb);
    }
    static int Euclidean_1934(int a, int b)
    {
        int r = a % b;
        if (r == 0)
            return b;
        return Euclidean_1934(b, r);
    }
    // I    최대공약수와 최소공배수
    public static void Baek2609()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int A = input[0], B = input[1], min = 0;

        // 최소공배수
        for (int i = A; i > 0; --i)
        {
            if (A % i == 0 && B % i == 0)
            {
                min = i;
                Console.WriteLine($"{min}");
                break;
            }
        }

        // 최대공약수
        for (int i = min; i < int.MaxValue; i += min)
        {
            if (i % A == 0 && i % B == 0)
            {
                Console.WriteLine($"{i}");
                break;
            }
        }
    }
    // I    단어 뒤집기
    static public void Baek9093()
    {
        int T = int.Parse(Console.ReadLine());
        for (int i = 0; i < T; ++i)
        {
            var input = Console.ReadLine().Split();
            for (int j = 0; j < input.Length; ++j)
            {
                for (int k = 0; k < input[j].Length; ++k)
                    sb.Append(input[j][input[j].Length - k - 1]);
                if (j < input.Length - 1)
                    sb.Append(' ');
            }
            sb.Append('\n');
        }
        Console.WriteLine(sb);
    }
    // I    ISBN
    static public void Baek14626()
    {
        string input = Console.ReadLine();
        int m = input[input.Length - 1] - '0';

        int multiplier = 1, calc = 0;

        for (int i = 0; i < 12; i++)
        {
            if (input[i] == '*')
            {
                multiplier = i % 2 == 0 ? 1 : 3;
                continue;
            }

            int temp = input[i] - '0';

            if (i % 2 == 0)
            {
                // Console.WriteLine("{0} + {1} = {2}", calc, temp, calc + temp);
                calc += temp;
            }
            else
            {
                // Console.WriteLine("{0} + {1} = {2}", calc, temp * 3, calc + temp * 3);
                calc += temp * 3;
            }
        }
        calc %= 10;

        for (int i = 0; i < 10; i++)
        {
            // Console.WriteLine((i * multiplier + calc + m) % 10 + " == " + 0);

            if (0 == (i * multiplier + calc + m) % 10)
                Console.WriteLine(i);
        }
    }
    // I    명령 프롬프트
    static public void Baek1032()
    {
        int N = int.Parse(Console.ReadLine());
        string path = "";
        for (int i = 0; i < N; ++i)
        {
            var input = Console.ReadLine();
            for (int j = 0; j < input.Length; ++j)
            {
                if (path.Length <= j)
                    path += input[j];
                else if (path[j] != input[j])
                {
                    string front = path.Substring(0, j);
                    string back = path.Substring(j + 1, path.Length - (j + 1));
                    path = front + "?" + back;
                }
            }
        }
        Console.WriteLine(path);
    }
    static public void Baek1032_Plus()
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; ++i)
        {
            var input = Console.ReadLine();
            for (int j = 0; j < input.Length; ++j)
            {
                if (sb.Length <= j)
                    sb.Append(input[j]);
                else if (sb[j] != input[j])
                    sb.Remove(j, 1).Insert(j, "?");
            }
        }
        Console.WriteLine(sb);
    }

    #endregion

    #region Bronze II

    // II   오르막
    public static void Baek14910()
    {
        var n = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int first = n[0];
        for (int i = 1; i < n.Length; ++i)
        {
            if (first > n[i])
            {
                Console.WriteLine("Bad");
                return;
            }
            first = n[i];
        }
        Console.WriteLine("Good");
    }
    // II   탄산 음료
    public static void Baek5032()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int e = input[0], f = input[1], c = input[2];

        int total = e + f, drinkCount = 0;
        while (total >= c)
        {
            int newBottles = total / c;
            drinkCount += newBottles;

            total = total % c;
            total += newBottles;
        }
        Console.WriteLine(drinkCount);
    }
    // II   벌집
    public static void Baek2292()
    {
        // 1 : 1            1
        // 2 ~ 7 : 2        6
        // 8 ~ 19 : 3       12
        // 20 ~ 37 : 4      18
        // 38 ~ 61 : 5      24
        // 62 ~ 91 : 6      30

        int n = int.Parse(Console.ReadLine());
        int count = 1;

        for (int i = 1; i <= int.MaxValue; i += 6 * (count - 1))
        {
            if (i >= n)
            {
                Console.WriteLine($"{count}");
                return;
            }
            ++count;
        }
    }
    // II   손익분기점
    public static void Baek1712()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int A = input[0], B = input[1], C = input[2];

        if (B >= C)
            Console.WriteLine($"-1");
        else
            Console.WriteLine($"{A / (C - B) + 1}");
    }
    // II   카드 역배치
    public static void Baek10804()
    {
        int[] cards = new int[20];
        for (int i = 0; i < 20; ++i)
            cards[i] = i + 1;       // 1 ~ 20 숫자 카드 생성

        for (int i = 0; i < 10; ++i)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int a = input[0] - 1, b = input[1] - 1; // a, b 입력 받기

            for (int j = 0; j <= (b - a) / 2; ++j)  // b - a번 반복해서 카드 재배치
            {   // 카드 Swap
                var temp = cards[j + a];
                cards[j + a] = cards[b - j];
                cards[b - j] = temp;
            }
        }

        foreach (var card in cards)     // 순서대로 카드 출력
            Console.Write($"{card} ");
    }
    // II   숫자의 개수
    public static void Baek2577()
    {
        int A = int.Parse(Console.ReadLine());
        int B = int.Parse(Console.ReadLine());
        int C = int.Parse(Console.ReadLine());
        int[] counts = new int[10]; // 0 ~ 9까지의 갯수를 저장할 배열

        string result = (A * B * C).ToString(); // A*B*C한 값은 int를 초과하니까 string으로 받기

        foreach (var c in result)   // ex) char:1 -> int:49
            ++counts[c - '0'];      // char - char로 값을 구함.
                                    // '1' - '0'-> 49 - 48 = 1
        foreach (var i in counts)
            Console.WriteLine($"{i}");  // 출력
    }
    // II   진법 변환
    public static void Baek2745()
    {
        var input = Console.ReadLine().Split();
        string N = input[0];
        int B = int.Parse(input[1]), R = 0;

        for (int i = N.Length - 1; i >= 0; --i)
        {
            if (N[i] == '0') continue;
            int t = (int)Math.Pow(B, N.Length - i - 1);
            int p;
            if ('A' <= N[i])
                p = N[i] - 'A' + 10;
            else
                p = int.Parse(N[i].ToString());
            R += t * p;
        }
        Console.WriteLine($"{R}");
    }
    // II   8진수 2진수
    static void EightToB_1212(int n, int div = 4)
    {
        if (div == 0)
            return;
        if (n >= div)
        {
            n -= div;
            sb.Append("1");
            EightToB_1212(n, div / 2);
        }
        else
        {
            if (sb.Length != 0)
                sb.Append("0");
            EightToB_1212(n, div / 2);
        }
    }
    public static void Baek1212()
    {
        var input = Console.ReadLine();
        for (int i = 0; i < input.Length; ++i)
        {
            EightToB_1212(input[i] - '0');
        }
        if (input == "0")
            Console.WriteLine($"0");
        else
            Console.WriteLine(sb);
    }
    // II   대표값2
    public static void Baek2587()
    {
        int sum = 0;
        int[] array = new int[5];
        for (int i = 0; i < 5; ++i)
        {
            int input = int.Parse(Console.ReadLine());
            sum += input;

            array[i] = input;
        }
        Array.Sort(array);
        Console.WriteLine($"{sum / 5}\n{array[2]}");
    }
    // II   대표값
    public static void Baek2592()
    {
        int sum = 0, mode = 0, count = 10;
        int[] nums = new int[1001];
        while (count-- > 0)
        {
            int input = int.Parse(Console.ReadLine());
            sum += input;
            nums[input]++;
        }
        for (int i = 1; i <= 1000; ++i)
        {
            if (count < nums[i])
            {
                count = nums[i];
                mode = i;
            }
        }
        Console.WriteLine($"{sum / 10}\n{mode}");
    }
    // II   저항
    public static void Baek1076()
    {
        // 함수방법
        string c1 = Console.ReadLine();
        string c2 = Console.ReadLine();
        string c3 = Console.ReadLine();
        long sum = GetValue1076(c1) * 10 + GetValue1076(c2);
        sum *= GetMulti1076(c3);
        Console.WriteLine($"{sum}");

        #region Mine
        string num = "";
        long total = 0;
        for (int i = 0; i < 3; ++i)
        {
            var input = Console.ReadLine();
            switch (input)
            {
                case "black":
                    if (i != 2) num += 0;
                    else total = 1 * long.Parse(num);
                    break;
                case "brown":
                    if (i != 2) num += 1;
                    else total = 10 * long.Parse(num);
                    break;
                case "red":
                    if (i != 2) num += 2;
                    else total = 100 * long.Parse(num);
                    break;
                case "orange":
                    if (i != 2) num += 3;
                    else total = 1000 * long.Parse(num);
                    break;
                case "yellow":
                    if (i != 2) num += 4;
                    else total = 10000 * long.Parse(num);
                    break;
                case "green":
                    if (i != 2) num += 5;
                    else total = 100000 * long.Parse(num);
                    break;
                case "blue":
                    if (i != 2) num += 6;
                    else total = 1000000 * long.Parse(num);
                    break;
                case "violet":
                    if (i != 2) num += 7;
                    else total = 10000000 * long.Parse(num);
                    break;
                case "grey":
                    if (i != 2) num += 8;
                    else total = 100000000 * long.Parse(num);
                    break;
                case "white":
                    if (i != 2) num += 9;
                    else total = 1000000000 * long.Parse(num);
                    break;
            }
        }
        Console.WriteLine($"{total}");
        #endregion
    }
    static long GetValue1076(string color)
    {
        switch (color)
        {
            case "black":
                return 0;
            case "brown":
                return 1;
            case "red":
                return 2;
            case "orange":
                return 3;
            case "yellow":
                return 4;
            case "green":
                return 5;
            case "blue":
                return 6;
            case "violet":
                return 7;
            case "grey":
                return 8;
            case "white":
                return 9;
        }
        return -1;
    }
    static long GetMulti1076(string color)
    {
        switch (color)
        {
            case "black":
                return 1;
            case "brown":
                return 10;
            case "red":
                return 100;
            case "orange":
                return 1000;
            case "yellow":
                return 10000;
            case "green":
                return 100000;
            case "blue":
                return 1000000;
            case "violet":
                return 10000000;
            case "grey":
                return 100000000;
            case "white":
                return 1000000000;
        }
        return -1;
    }
    // II   문자열 분석
    public static void Baek10820()
    {
        while (true)
        {   // 0:a~z / 1:A~Z / 2:0~9 / 3:' '
            int[] results = new int[4];
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine(sb);
                return;
            }

            foreach (var c in input)
            {
                if ('a' <= c && c <= 'z')
                    ++results[0];
                else if ('A' <= c && c <= 'Z')
                    ++results[1];
                else if ('0' <= c && c <= '9')
                    ++results[2];
                else if (' ' == c)
                    ++results[3];
            }
            foreach (var result in results)
                sb.Append($"{result} ");
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine();
        }
    }
    // II   알파벳 찾기
    public static void Baek10809()
    {
        var input = Console.ReadLine();
        int[] alphabets = new int[26];
        Array.Fill(alphabets, -1);
        for (int i = 0; i < input.Length; ++i)
        {
            if (alphabets[input[i] - 'a'] == -1)
                alphabets[input[i] - 'a'] = i;
        }
        foreach (var idx in alphabets)
            sb.Append($"{idx} ");
        sb.Remove(sb.Length - 1, 1);
        Console.WriteLine(sb);
    }
    // II   나머지
    public static void Baek3052()
    {
        int[] nums = new int[42];
        for (int i = 0; i < 10; ++i)
        {
            int num = int.Parse(Console.ReadLine());
            nums[num % 42]++;
        }
        int difcnt = 0;
        foreach (var cnt in nums)
        {
            if (cnt == 0) continue;
            ++difcnt;
        }
        Console.WriteLine(difcnt);
    }
    // II   소수 찾기
    public static void Baek1978()
    {   // 주어질 숫자의 개수 N, 소수의 개수를 셀 count
        int N = int.Parse(Console.ReadLine()), count = 0;
        // 숫자를 받아 나누어 int배열로 변환
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        // 소수 찾는 로직
        for (int i = 0; i < N; ++i)
        {   // 1은 소수가 아니니까 버린다.
            if (input[i] == 1) continue;
            for (int j = 2; j < input[i] + 1; ++j)
            {   // 2부터 input[i] : 입력받은 수까지 소수인지 확인하기 위한 과정
                if (input[i] != j && input[i] % j == 0)    // 나머지가 0이라면 소수가아님.
                    break;
                if (j == input[i])    // 자기 자신까지 체크했다면 소수임.
                    ++count;
            }
        }
        Console.WriteLine(count);
    }
    // II   피시방 알바
    public static void Baek1453()
    {
        int N = int.Parse(Console.ReadLine()), count = 0;
        bool[] seats = new bool[101];

        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        for (int i = 0; i < N; ++i)
        {
            if (!seats[input[i]])
                seats[input[i]] = true;
            else
                ++count;
        }
        Console.WriteLine($"{count}");
    }
    // II   피보나치 수
    public static void Baek2747()
    {
        int N = int.Parse(Console.ReadLine());
        int[] array = new int[N + 1];
        Console.WriteLine($"{Fibo2747(array, N)}");
    }
    static int Fibo2747(int[] array, int n)
    {
        if (n == 0)
            return 0;
        if (n == 1)
            return 1;
        else if (array[n] == 0)
            array[n] = Fibo2747(array, n - 1) + Fibo2747(array, n - 2);
        return array[n];
    }
    // II   블랙잭
    static public void Baek2798()
    {
        var NM = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int N = NM[0], M = NM[1], max = 0;
        int[] cards = new int[N];
        cards = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

        for (int i = 0; i < N - 2; ++i)
        {   // 3장의 카드만 뽑음 -> 마지막 2장은 탐색 X
            for (int j = i + 1; j < N - 1; ++j)
            {   // 2번째 for문에서는 마지막 1장만 탐색 X
                for (int k = j + 1; k < N; ++k)
                {   // 3번째 for문에서는 마지막 카드까지 탐색
                    int sum = cards[i] + cards[j] + cards[k];
                    if (sum > max && sum <= M)
                        max = sum;
                }
            }
        }
        sw.WriteLine(max);
    }
    // II   분해합
    static public void Baek2231()
    {
        int N = int.Parse(sr.ReadLine()), min = int.MaxValue;
        for (int i = 1; i < N; ++i)
        {
            int temp = N - i, sum = 0;
            for (int j = 0; j < temp.ToString().Length; ++j)
                sum += temp.ToString()[j] - '0';
            if (sum == i && min > sum)
                min = temp;
        }
        if (min == int.MaxValue)
            sw.WriteLine('0');
        else
            sw.WriteLine(min);
    }

    #endregion

    #region Bronze III

    // III  팰린드롬인지 확인하기
    public static void Baek10988()
    {
        var input = Console.ReadLine();
        Console.WriteLine(Palindrome_10988(input));
    }
    static int Palindrome_10988(string str)
    {
        for (int i = 0; i < str.Length / 2; ++i)
        {
            if (str[i] != str[str.Length - 1 - i]) return 0;
        }
        return 1;
    }
    // III  수학은 체육과목 입니다
    public static void Baek15984()
    {
        /*
        1 : 4  -> 4
        2 : 8  -> 4 (-1 +1 +2 +2)
        3 : 12 -> 8 (-2 +1 +2 +3)
        4 : 16 -> 12 (-3 +1 +2 +4)
        5 : 20 -> 16 (-4 +1 +2 +5)
        */
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine((long)n * 4);
    }
    // III  헬멧과 조끼
    public static void Baek15781()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int n = input[0], m = input[1];

        int[] helmets = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] vests = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        Array.Sort(helmets);
        Array.Sort(vests);

        Console.WriteLine($"{helmets[n - 1] + vests[m - 1]}");
    }
    // III  공
    public static void Baek1547()
    {
        int m = int.Parse(Console.ReadLine());
        bool[] cups = new bool[3];
        cups[0] = true;

        while (m-- > 0)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int x = input[0] - 1, y = input[1] - 1;
            var temp = cups[x];
            cups[x] = cups[y];
            cups[y] = temp;
        }

        for (int i = 0; i < 3; ++i)
            if (cups[i]) Console.WriteLine($"{i + 1}");
    }
    // III  약수 구하기
    public static void Baek2501()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = input[0], K = input[1], count = 0;

        for (int i = 0; i < N; ++i)
        {
            if (0 == N % (i + 1))   // 나눠 질때만 카운팅
                ++count;
            if (count == K)         // 카운팅이 목표점에 도달했을때
            {
                Console.WriteLine($"{i + 1}");  // 약수 값 출력후
                return;                         // 종료
            }
        }
        Console.WriteLine($"0");    // for문을 빠져 나왔음 -> 목표값의 약수가 존재하지 않음.
    }
    // III  직사각형에서 탈출
    public static void Baek1085()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int x = input[0], y = input[1], w = input[2], h = input[3];

        Console.WriteLine($"{Math.Min(Math.Min(w - x, h - y), Math.Min(x, y))}");
    }
    // III  2의 제곱인가?
    public static void Baek11966()
    {
        int N = int.Parse(Console.ReadLine());
        while (N > 1)
        {
            if (0 == N % 2)
                N /= 2;
            else
            {
                Console.WriteLine(0);
                return;
            }
        }
        Console.WriteLine(1);
    }
    // III  플러그
    public static void Baek2010()
    {
        int N = int.Parse(Console.ReadLine()), total = 0;
        for (int i = 0; i < N; ++i) // 멀티탭에 연결 가능한 모든 구멍 갯수 더해주기.
            total += int.Parse(Console.ReadLine());
        total -= N - 1; // 마지막 멀티탭 빼곤 1개씩 서로의 멀티탭에 연결해주니까 빼주기.
        Console.WriteLine(total);
    }
    // III  집 주소
    public static void Baek1284()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (input == "0")
                return;
            int sum = 2;
            for (int i = 0; i < input.Length; ++i)
            {
                if (input[i] == '1') sum += 2 + 1;
                else if (input[i] == '0') sum += 4 + 1;
                else sum += 3 + 1;
            }
            Console.WriteLine($"{sum - 1}");
        }
    }
    // III  꼬리를 무는 숫자 나열
    public static void Baek1598()
    {   // 두 수를 입력 받기
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        // 두 수를 변수로 만들기
        int A = input[0], B = input[1];

        // 두수의 좌푯값 구하기
        int Ax = (A - 1) / 4, Ay = (A - 1) % 4;
        int Bx = (B - 1) / 4, By = (B - 1) % 4;

        // 두 수의 좌푯값을 빼서 거리 구하기 (Unity의 Vec3 - Vec3같은 원리)
        Console.WriteLine($"{Math.Abs(Ax - Bx) + Math.Abs(Ay - By)}");
    }
    // III  팩토리얼
    public static void Baek10872()
    {
        int N = int.Parse(Console.ReadLine());
        Console.WriteLine($"{Factorial_10872(N)}");
    }
    static int Factorial_10872(int N)
    {
        if (N == 0)
            return 1;
        return N * Factorial_10872(N - 1);
    }
    // III  네 수
    public static void Baek10824()
    {
        var input = Console.ReadLine().Split();
        int AB = int.Parse(input[0] + input[1]);
        int CD = int.Parse(input[2] + input[3]);
        Console.WriteLine($"{AB + CD}");
    }
    // III  과제 안 내신 분..?
    public static void Baek5597()
    {
        bool[] students = new bool[30];
        for (int i = 0; i < 28; ++i)
        {
            var num = int.Parse(Console.ReadLine());
            students[num - 1] = true;
        }

        for (int i = 0; i < 30; ++i)
        {
            if (!students[i])
                Console.WriteLine(i + 1);
        }
    }
    // III  최댓값
    public static void Baek2566()
    {
        int max = int.MinValue;
        int[] pos = new int[2];
        for (int i = 0; i < 9; ++i)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            for (int j = 0; j < 9; ++j)
            {
                if (max < input[j])
                {
                    max = input[j];
                    pos[0] = i + 1;
                    pos[1] = j + 1;
                }
            }
        }
        Console.WriteLine($"{max}\n{pos[0]} {pos[1]}");
    }
    // III  홀수
    public static void Baek2576()
    {
        int sum = 0, min = int.MaxValue;
        for (int i = 0; i < 7; ++i)
        {
            int N = int.Parse(Console.ReadLine());
            if (N % 2 == 0) continue;
            sum += N;
            if (min > N)
                min = N;
        }
        if (sum == 0)
            Console.WriteLine("-1");
        else
            Console.WriteLine($"{sum}\n{min}");
    }
    // III  세탁소 사장 동혁
    public static void Baek2720()
    {
        int T = int.Parse(Console.ReadLine());
        while (T > 0)
        {   // Quat:25 Dime:10 Nick:5 Peny:1    (단위:cent)
            T--;
            int Cent = int.Parse(Console.ReadLine());
            int Quat = Cent / 25;
            int Dime = Cent % 25 / 10;
            int Nick = Cent % 25 % 10 / 5;
            int Peny = Cent % 25 % 10 % 5;
            Console.WriteLine($"{Quat} {Dime} {Nick} {Peny}");
        }
    }
    // III  J박스
    public static void Baek5354()
    {
        int T = int.Parse(Console.ReadLine());
        for (int i = 0; i < T; ++i)
        {
            int L = int.Parse(Console.ReadLine());
            for (int x = 0; x < L; ++x)
            {
                for (int y = 0; y < L; ++y)
                {
                    if (x == 0 || x == L - 1 || y == 0 || y == L - 1)
                        Console.Write($"#");
                    else
                        Console.Write($"J");
                }
                System.Console.WriteLine();
            }
        }
    }
    // III  삼각형과 세 변
    public static void Baek5073()
    {
        while (true)
        {
            // 한번에 int로 변환, ConvertAll은 int[]을 리턴합니다.
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int a = input[0], b = input[1], c = input[2];
            if (a == 0 || b == 0 || c == 0)
                break;

            if (a == b && b == c)   // a == b == c 일때.
                Console.WriteLine("Equilateral");
            else if (a >= b + c || b >= a + c || c >= a + b)   // 삼각형이 될수 없을때.
                Console.WriteLine("Invalid");
            else if (a == b || b == c || a == c)    // 두변의 길이는 같을때.
                Console.WriteLine("Isosceles");
            else
                Console.WriteLine("Scalene");
        }
    }
    // III  서울사이버대학을 다니고
    public static void Baek30958()
    {
        int[] cnts = new int[26];
        int N = int.Parse(Console.ReadLine()), max = 0;
        var input = Console.ReadLine();
        foreach (var alphabet in input)
        {
            if ('.' == alphabet || ',' == alphabet || ' ' == alphabet)
                continue;
            cnts[alphabet - 'a']++;
        }
        foreach (var cnt in cnts)
        {
            if (max < cnt)
                max = cnt;
        }
        Console.WriteLine($"{max}");
    }
    // III  TGN
    public static void Baek5063()
    {   // r,e,c 광고x수익 / 광고o수익 / 광고비용
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; ++i)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            if (input[1] - input[2] < input[0])
                Console.WriteLine($"do not advertise");
            else if (input[1] - input[2] > input[0])
                Console.WriteLine($"advertise");
            else
                Console.WriteLine($"does not matter");
        }
    }
    // III  최댓값
    public static void Baek2562()
    {
        int max = int.MinValue, index = -1;
        for (int i = 0; i < 9; ++i)
        {
            var input = int.Parse(Console.ReadLine());
            if (max < input)
            {
                max = input;
                index = i + 1;
            }
        }
        Console.WriteLine($"{max}\n{index}");
    }
    // III  최소, 최대
    public static void Baek10818()
    {
        int N = int.Parse(Console.ReadLine()), min = int.MaxValue, max = int.MinValue;
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        for (int i = 0; i < N; ++i)
        {
            if (min > input[i])
                min = input[i];
            if (max < input[i])
                max = input[i];
        }
        Console.WriteLine($"{min} {max}");
    }
    // III  경기 결과
    public static void Baek5523()
    {
        int N = int.Parse(Console.ReadLine()), aWin = 0, bWin = 0;
        for (int i = 0; i < N; ++i)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int A = input[0], B = input[1];
            aWin += A > B ? 1 : 0;
            bWin += A < B ? 1 : 0;
        }
        Console.WriteLine($"{aWin} {bWin}");
    }
    // III  곱셈
    static public void Baek2588()
    {
        int a = int.Parse(sr.ReadLine());
        int b = int.Parse(sr.ReadLine());
        sb.Append((a * (b % 10)).ToString() + '\n');
        sb.Append((a * ((b / 10) % 10)).ToString() + '\n');
        sb.Append((a * (b / 100)).ToString() + '\n');
        sb.Append((a * b).ToString() + '\n');
        sw.Write(sb);
    }
    // III  알람시계
    static public void Baek2884()
    {
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int H = input[0], M = input[1] - 45;
        if (M < 0)
        {
            M += 60;
            if (H == 0)
                H += 24;
            else
                H--;
        }
        sw.WriteLine($"{H} {M}");
    }
    // III  네 번째 수
    static public void Baek2997()
    {
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

        for (int i = 0; i < 2; ++i)
        {
            for (int j = i + 1; j < 3; ++j)
            {
                if (input[i] > input[j])
                {
                    int temp = input[i];
                    input[i] = input[j];
                    input[j] = temp;
                }
            }
        }

        var minus = Math.Min(input[1] - input[0], input[2] - input[1]);
        if (input[0] + minus != input[1])
            Console.WriteLine(input[0] + minus);
        else if (input[1] + minus != input[2])
            Console.WriteLine(input[1] + minus);
        else
            Console.WriteLine(input[2] + minus);
    }
    // III  별 찍기4
    public static void Baek2441()
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; ++i)
        {
            for (int j = 0; j < i; ++j)
                sb.Append(' ');
            for (int j = i; j < N; ++j)
                sb.Append('*');
            sb.Append('\n');
        }
        Console.WriteLine(sb);
    }

    #endregion

    #region Bronze IV

    // IV   모음의 개수
    public static void Baek1264()
    {
        string list = "aeiouAEIOU";
        while (true)
        {
            var input = Console.ReadLine();
            if ("#" == input)
                break;

            int count = 0;
            foreach (var c in input)
                if (list.Contains(c)) count++;
            sb.AppendLine(count.ToString());
        }
        Console.WriteLine(sb);
    }
    // IV   줄번호
    public static void Baek4470()
    {
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; ++i)
        {
            var input = Console.ReadLine();
            sb.AppendLine($"{i + 1}. {input}");
        }
        Console.WriteLine(sb);
    }
    // IV   파티가 끝나고 난 뒤
    public static void Baek2845()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int L = input[0], P = input[1]; // 인원 : L & 넓이 : P

        input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        for (int i = 0; i < input.Length; ++i)
            sb.Append($"{input[i] - L * P} ");  // 신문사 5곳의 틀린수 출력
        sb.Remove(sb.Length - 1, 1);    // 마지막의 공백 지움
        Console.WriteLine(sb);
    }
    // IV   주사위 세개
    public static void Baek2480_Simple()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int A = input[0], B = input[1], C = input[2];
        if (A == B && B == C)
            Console.WriteLine($"{A * 1000 + 10000}");
        else if (A != B && B != C && A != C)
            Console.WriteLine($"{(A > B && A > C ? A * 100 : B > A && B > C ? B * 100 : C * 100)}");
        else
            Console.WriteLine($"{(A == B ? A * 100 + 1000 : C * 100 + 1000)}");
    }
    public static void Baek2480()
    {
        int[] results = new int[6];
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        for (int i = 0; i < 3; ++i)
            results[input[i] - 1]++;
        int only1 = 0, max = 0;
        for (int i = 0; i < 6; ++i)
        {
            if (results[i] == 3)
                Console.WriteLine($"{10_000 + (i + 1) * 1_000}");
            else if (results[i] == 2)
                Console.WriteLine($"{1_000 + (i + 1) * 100}");
            else if (results[i] == 1)
            {
                if (max < i + 1)
                    max = i + 1;
                only1++;
            }
        }
        if (only1 == 3)
            Console.WriteLine($"{max * 100}");
    }
    // IV   뜨거운 붕어빵
    public static void Baek11945()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = input[0], M = input[1];

        for (int i = 0; i < N; ++i)
        {
            var str = Console.ReadLine();
            for (int j = 0; j < M; ++j)
                sb.Append(str[M - j - 1]);
            sb.AppendLine();
        }
        Console.WriteLine(sb);
    }
    // IV   알파벳 개수
    public static void Baek10808()
    {
        int[] alphabets = new int[26];
        var input = Console.ReadLine();
        for (int i = 0; i < input.Length; ++i)
            alphabets[input[i] - 'a']++;
        foreach (var cnt in alphabets)
            sb.Append($"{cnt} ");
        sb.Remove(sb.Length - 1, 1);
        Console.WriteLine(sb);
    }
    // IV   시험 점수
    public struct Student_5596()
    {
        int inf, math, sci, eng;
        public Student_5596(int[] scores) : this()
        {
            inf = scores[0];
            math = scores[1];
            sci = scores[2];
            eng = scores[3];
        }
        public int Sum()
        {
            return inf + math + sci + eng;
        }
        public int Result(Student_5596 student)
        {
            return Sum() > student.Sum() ? Sum() : student.Sum();
        }
    }
    public static void Baek5596()
    {
        // 1
        Student_5596 A = new Student_5596(Array.ConvertAll(Console.ReadLine().Split(), int.Parse));
        Student_5596 B = new Student_5596(Array.ConvertAll(Console.ReadLine().Split(), int.Parse));
        Console.WriteLine($"{A.Result(B)}");
        // 2
        int Ssum = 0, Tsum = 0;
        var S = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        foreach (var n in S)
            Ssum += n;
        var T = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        foreach (var n in T)
            Tsum += n;
        Console.WriteLine($"{(Ssum > Tsum ? Ssum : Tsum)}");
    }
    // IV   나는 행복합니다~
    public static void Baek14652()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = input[0], M = input[1], K = input[2];

        int x = K / M;  // x좌표값
        int y = K % M;  // y좌표값
        Console.WriteLine($"{x} {y}");
    }
    // IV   나이 계산하기
    public static void Baek16199()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int byear = input[0], bmonth = input[1], bday = input[2];
        input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int year = input[0], month = input[1], day = input[2];

        int syear = year - byear;
        int smonth = month - bmonth;
        int sday = day - bday;

        // 만나이 세는나이 연나이 출력
        Console.WriteLine(syear + (smonth > 0 ? 0 : (smonth == 0 && sday >= 0) ? 0 : -1));
        Console.WriteLine(syear + 1);
        Console.WriteLine(syear);
    }
    // IV   Pokemon Buddy
    public static void Baek18691()
    {
        int T = int.Parse(Console.ReadLine());
        for (int i = 0; i < T; ++i)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            if (input[1] >= input[2])
            {
                Console.WriteLine("0");
                continue;
            }
            Console.WriteLine($"{(input[0] == 2 ? 3 : input[0] == 3 ? 5 : 1) * (input[2] - input[1])}");
        }
    }
    // IV   별 찍기3
    public static void Baek2440()
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; ++i)
        {
            for (int j = i; j < N; ++j)
                sb.Append('*');
            sb.Append('\n');
        }
        Console.WriteLine(sb);
    }
    // IV   별 찍기2
    public static void Baek2439()
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; ++i)
        {
            for (int j = i + 1; j < N; ++j)
                sb.Append(' ');
            for (int j = 0; j < i + 1; ++j)
                sb.Append('*');
            sb.Append('\n');
        }
        Console.WriteLine(sb);
    }
    // IV   평균 점수
    public static void Baek10039()
    {
        int sum = 0;
        for (int i = 0; i < 5; ++i)
        {
            int input = int.Parse(Console.ReadLine());
            sum += input > 40 ? input : 40;
        }
        Console.WriteLine($"{sum / 5}");
    }
    // IV   과자
    static public void Baek10156()
    {
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int K = input[0], N = input[1], M = input[2];
        sw.WriteLine(K * N > M ? K * N - M : 0);
    }
    // IV   가희와 방어율 무시
    static public void Baek25238()
    {
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        float defense = input[0], ignore = input[1];

        float trueDamage = defense - (defense * 0.01f * ignore);
        sw.WriteLine(trueDamage < 100 ? 1 : 0);
    }

    #endregion

    #region Bronze V

    // V    문자열
    public static void Baek9086()
    {
        int t = int.Parse(Console.ReadLine());
        for (int i = 0; i < t; ++i)
        {
            string input = Console.ReadLine();
            Console.WriteLine($"{input[0]}{input[input.Length - 1]}");
        }
    }
    // V    문자와 문자열
    public static void Baek27866()
    {
        string input = Console.ReadLine();
        int num = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(input[num - 1]);
    }
    // V    엄청난 부자2
    public static void Baek1271()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), BigInteger.Parse);
        BigInteger n = input[0], m = input[1];

        Console.WriteLine($"{n / m}\n{n % m}");
    }
    // V    X보다 작은 수
    public static void Baek10871()
    {
        var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = input[0], X = input[1];
        input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        foreach (var n in input)
            if (n < X) sb.Append($"{n} ");
        sb.Remove(sb.Length - 1, 1);
        Console.WriteLine(sb);
    }
    // V    코딩은 체육과목 입니다
    public static void Baek25314()
    {
        int N = int.Parse(Console.ReadLine());

        for (int i = 4; i <= N; i += 4)
            sb.Append("long ");
        sb.Append("int");
        Console.WriteLine(sb);
    }
    // V    단어 길이 재기
    public static void Baek2743()
    {
        string word = Console.ReadLine();
        Console.WriteLine($"{word.Length}");
    }
    // V    개수 세기
    public static void Baek10807()
    {
        int N = int.Parse(Console.ReadLine());
        int[] array = new int[N];
        array = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int R = int.Parse(Console.ReadLine()), count = 0;
        foreach (var num in array)
        {
            if (num == R)
                count++;
        }
        Console.WriteLine($"{count}");
    }
    // V    합
    public static void Baek8393()
    {
        int N = int.Parse(Console.ReadLine()), sum = 0;
        for (int i = 1; i <= N; ++i)
            sum += i;
        Console.WriteLine(sum);
    }
    // V    A+B -4
    public static void Baek10951()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (input == string.Empty)
                break;
            var nums = Array.ConvertAll(input.Split(), int.Parse);
            Console.WriteLine($"{nums[0] + nums[1]}");
        }
    }
    // V    개
    public static void Baek10172()
    {
        // |\_/|
        // |q p|   /}
        // ( 0 )"""\
        // |"^"`    |
        // ||_/=\\__|

        Console.WriteLine("|\\_/|");
        Console.WriteLine("|q p|   /}");
        Console.WriteLine("( 0 )\"\"\"\\");
        Console.WriteLine("|\"^\"`    |");
        Console.WriteLine("||_/=\\\\__|");
    }
    // V    별 찍기1
    public static void Baek2438()
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; ++i)
        {
            for (int j = 0; j < i + 1; ++j)
                sb.Append('*');
            sb.Append('\n');
        }
        Console.WriteLine(sb);
    }
    // V    A+B -2
    public static void Baek2558()
    {
        int A = int.Parse(Console.ReadLine()), B = int.Parse(Console.ReadLine());
        Console.WriteLine($"{A + B}");
    }
    // V    A+B -7
    public static void Baek11021()
    {
        int T = int.Parse(Console.ReadLine());
        for (int i = 0; i < T; ++i)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            sb.Append($"Case #{i + 1}: {input[0] + input[1]}\n");
        }
        Console.WriteLine(sb);
    }
    // V    기찍 N
    public static void Baek2742()
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = N; i > 0; --i)
            sb.Append($"{i}\n");

        Console.WriteLine(sb);
    }
    // V    N 찍기
    public static void Baek2741()
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = 1; i <= N; ++i)
            sb.Append($"{i}\n");

        Console.WriteLine(sb);
    }
    // V    R2
    static public void Baek3046()
    {   // (R1 + R2) / 2 = S    ->  R2 = 2S - R1
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int R1 = input[0], S = input[1], R2;
        R2 = S * 2 - R1;
        sw.WriteLine(R2);
    }
    // V    구구단
    static public void Baek2739()
    {
        int N = int.Parse(sr.ReadLine());
        for (int i = 1; i < 10; ++i)
            sb.Append($"{N} * {i} = {N * i}\n");
        sw.WriteLine(sb);
    }
    // V    인공지능 시계
    static public void Baek2530()
    {
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int A = input[0], B = input[1], C = input[2];
        int D = int.Parse(sr.ReadLine());
        // 60 = 1min 3600 = 1hour
        C += D % 60;
        if (C >= 60)
        {
            B += C / 60;
            C %= 60;
        }
        B += D / 60;
        if (B >= 60)
        {
            A += B / 60;
            B %= 60;
        }
        if (A > 23)
            A %= 24;
        sw.WriteLine("{0} {1} {2}", A, B, C);
    }
    // V    이상한 기호
    static public void Baek15964()
    {
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int A = input[0], B = input[1];
        sw.WriteLine((A + B) * (A - B));
    }
    // V    나머지
    static public void Baek10430()
    {
        //첫째 줄에 (A+B)%C, 둘째 줄에 ((A%C) + (B%C))%C, 셋째 줄에 (A×B)%C, 넷째 줄에 ((A%C) × (B%C))%C를 출력한다.
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int A = input[0], B = input[1], C = input[2];
        sw.WriteLine(((A + B) % C).ToString() + '\n' + (((A % C) + (B % C)) % C).ToString() + '\n' +
                        ((A * B) % C).ToString() + '\n' + (((A % C) * (B % C)) % C).ToString());
    }
    // V    제리와 톰
    static public void Baek16430()
    {
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int A = input[0], B = input[1];
        sw.WriteLine(B - A + " " + B);
    }
    // V    ??!
    static public void Baek10926()
    {
        var id = sr.ReadLine();
        sw.WriteLine(id + "??!");
    }
    // V    스위트콘 가격 구하기
    static public void Baek30030()
    {   // A : 10배수, 원가 B : 11배수, 부가가치세 포함 가격
        int B = int.Parse(sr.ReadLine());
        sw.WriteLine((B / 11) * 10);
    }
    // V    꼬마 정민이
    static public void Baek11382()
    {
        long[] inputs = Array.ConvertAll(sr.ReadLine().Split(), long.Parse);
        long A = inputs[0], B = inputs[1], C = inputs[2];
        sw.WriteLine((A + B + C).ToString());
    }
    // V    ASCII 코드
    static public void Baek11654()
    {
        char input = sr.ReadLine()[0];
        int ascii = input;
        sw.WriteLine(ascii);
    }
    // V    검증수
    static public void Baek2475()
    {
        int[] input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int result = 0;
        for (int i = 0; i < 5; i++)
            result += input[i] * input[i];

        result = result % 10;
        sw.WriteLine(result.ToString());
    }
    // V    사칙연산
    static public void Baek10869()
    {
        int[] input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        sb.Append((input[0] + input[1]).ToString() + '\n');
        sb.Append((input[0] - input[1]).ToString() + '\n');
        sb.Append((input[0] * input[1]).ToString() + '\n');
        sb.Append((input[0] / input[1]).ToString() + '\n');
        sb.Append((input[0] % input[1]).ToString() + '\n');
        sw.WriteLine(sb);
    }
    // V    A+B
    static public void Baek1000()
    {
        int[] input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        sw.WriteLine((input[0] + input[1]).ToString());
    }
    // V    A-B
    static public void Baek1001()
    {
        int[] input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        sw.WriteLine((input[0] - input[1]).ToString());
    }
    // V    두 수 비교하기
    static public void Baek1330()
    {
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        int A = input[0], B = input[1];
        sw.WriteLine(A > B ? '>' : A < B ? '<' : "==");
    }
    // V    시험 성적
    static public void Baek9498()
    {
        int grade = int.Parse(sr.ReadLine());
        sw.WriteLine(grade >= 90 ? "A" : grade >= 80 ? "B" : grade >= 70 ? "C" : grade >= 60 ? "D" : "F");
    }
    // V    윤년
    static public void Baek2753()
    {   // 윤년은 연도가 4의 배수이면서, 100의 배수가 아닐 때 또는 400의 배수일 때이다.
        int year = int.Parse(sr.ReadLine());
        sw.WriteLine(year % 4 == 0 && (year % 100 != 0 || year % 400 == 0) ? 1 : 0);
    }
    // V    사분면 고르기
    static public void Baek14681()
    {   // x > 0 & y > 0 : 1 / x < 0 & y > 0 : 2 / x < 0 & y < 0 : 3 / x > 0 & y < 0 : 4
        bool x = sr.ReadLine()[0] != '-' ? true : false;    // x > 0 ? true : false
        bool y = sr.ReadLine()[0] != '-' ? true : false;    // y > 0 ? true : false
        sw.WriteLine(x && y ? 1 : !x && y ? 2 : !x && !y ? 3 : 4);
    }
    // V    마이크로소프트 로고
    public static void Baek5338()
    {
        //       _.-;;-._
        //'-..-'|   ||   |
        //'-..-'|_.-;;-._|
        //'-..-'|   ||   |
        //'-..-'|_.-''-._|
        Console.WriteLine("       _.-;;-._");
        Console.WriteLine("\'-..-\'|   ||   |");
        Console.WriteLine("\'-..-\'|_.-;;-._|");
        Console.WriteLine("\'-..-\'|   ||   |");
        Console.WriteLine("\'-..-\'|_.-''-._|");
    }
    // V    긴자리 계산
    public static void Baek2338()
    {
        BigInteger A = BigInteger.Parse(Console.ReadLine()), B = BigInteger.Parse(Console.ReadLine());
        Console.WriteLine($"{A + B}\n{A - B}{A * B}");
    }
    // V    큰 수 (BIG)
    public static void Baek14928()
    {
        BigInteger N = BigInteger.Parse(Console.ReadLine());
        Console.WriteLine($"{N % 20000303}");
    }
    // V    가희와 4시간의 벽 1
    public static void Baek32775()
    {
        int Sab = int.Parse(Console.ReadLine()), Fab = int.Parse(Console.ReadLine());

        if (Sab <= Fab)
        {
            if (Sab == Fab && 240 < Sab)
                Console.WriteLine("flight");
            else
                Console.WriteLine("high speed rail");
        }
        else
            Console.WriteLine("flight");
    }
    // V    Conveyor Belt Sushi
    public static void Baek32326()
    {
        int R = int.Parse(Console.ReadLine()), G = int.Parse(Console.ReadLine()), B = int.Parse(Console.ReadLine());
        Console.WriteLine($"{R * 3 + G * 4 + B * 5}");
    }

    #endregion

    #region 부록
    public static void DanguJang()
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; ++i)
        {
            for (int j = 0; j < N; ++j)
            {
                if (i == j && i + j == N - 1)
                    sb.Append('X');
                else if (i + j == N - 1)
                    sb.Append('/');
                else if (i == j)
                    sb.Append('\\');
                else
                    sb.Append('*');
            }
            sb.Append('\n');
        }
        Console.WriteLine(sb);
    }

    public void StarPiramid()
    {
        for (int i = 0; i < 6; ++i)
        {
            for (int j = 5 - i; j > 0; --j)
            {
                Console.Write(' ');
            }
            for (int j = 0; j < 1 + (i * 2); ++j)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
    }
    #endregion
}