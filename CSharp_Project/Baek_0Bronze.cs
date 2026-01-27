using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Reflection;
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
    // I    일곱 난쟁이
    static public void Baek2109()
    {
        List<int> dwarfs = new List<int>();
        int sum = 0, num;
        for (int i = 0; i < 9; ++i)
        {
            dwarfs.Add(int.Parse(sr.ReadLine()));
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
                    sw.WriteLine(dwarf.ToString());
                }
                break;
            }
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
    static public void Baek1032Plus()
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