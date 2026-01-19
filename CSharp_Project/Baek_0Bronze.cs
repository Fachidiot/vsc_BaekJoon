using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

    #endregion

    #region Bronze IV

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

    // V    A+B-7
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

    #endregion

    // 부록
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
}