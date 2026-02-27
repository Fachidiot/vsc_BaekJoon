using System;
using System.Text.RegularExpressions;

class Cs_Basic
{
    // [Catch Exception]
    public static void CatchException()
    {
        try
        {
            int now = DateTime.Now.Second;
            Console.WriteLine($"[0] 현재 초 : {now}");

            int result = 2 / (now % 2);
            Console.WriteLine($"[1] 홀수 초에서는 정상 처리");
        }
        catch
        {
            Console.WriteLine($"[2] 짝수 초에서는 런타임 에러 발생");
        }

        int x = 5;
        int y = 0;
        int r;
        try
        {
            r = x / y;
            Console.WriteLine($"{x} / {y} = {r}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"에러가 발생했습니다. {e}");
        }
        finally
        {
            Console.WriteLine("프로그램을 종료합니다.");
        }
    }

    // [object 형변환]
    public static void ObjectChange()
    {
        object x = "1234";

        if (x is int)
            Console.WriteLine($"{x}는 정수형으로 변환이 가능합니다.");
        else if (x is string)
            Console.WriteLine($"{x}는 문자열형으로 변환이 가능합니다.");
        else if (x is DateTime)
            Console.WriteLine($"{x}는 날짜형으로 변환이 가능합니다.");
        else
            Console.WriteLine($"변환이 불가능 합니다.");

        object s = "반갑습니다.";
        string r1 = s as string;
        Console.WriteLine($"[1] {r1}");

        object i = 1234;
        string r2 = i as string;
        if (r2 == null)
            Console.WriteLine($"[2] null입니다.");

        object i2 = 3456;
        if (i2 is string)
            Console.WriteLine($"[3] {i2 as string}");
        else
            Console.WriteLine($"[3] 변환 불가");

        PrintStars(null);
        PrintStars("하나");
        PrintStars(5);
    }
    static void PrintStars(object o)
    {
        if (o is null)
            return;
        if (o is string)
            return;
        if (!(o is int number))
            return;

        Console.WriteLine(new string('*', number));
    }

    // [Regex 사용법]
    public static void UseRegex()
    {
        string s = "안녕하세요.    반갑습니다.    또 만나요.";
        var regex = new Regex("\\s+");      // 하나 이상의 공백 패턴 검사
        string r = regex.Replace(s, " ");   // 하나 이상의 공백을 공백하나로 변환
        Console.WriteLine(s);
        Console.WriteLine(r);

        // 이메일인지 판별
        string email = "abc@aaa.com";
        regex = new Regex(
                @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)" +
                @"(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
        Console.WriteLine($"{regex.IsMatch(email)}");
    }

    // [Enum문의 종류를 출력하고싶을때]
    public static void PrintEnum()
    {
        Type cc = typeof(ConsoleColor);
        string[] colors = Enum.GetNames(cc);
        foreach (var color in colors)
            Console.WriteLine($"{color}");
    }

    // [char타입 판별 방법 메소드]
    public static void CheckChar()
    {
        char a = 'A';
        if (char.IsUpper(a)) Console.WriteLine($"{a}는 대문자");
        a = 'a';
        if (char.IsLower(a)) Console.WriteLine($"{a}는 소문자");
        a = '1';
        if (char.IsNumber(a)) Console.WriteLine($"{a}는 숫자행");
        a = '\t';
        if (char.IsWhiteSpace(a)) Console.WriteLine($"{a}는 공백");
    }

    // [TimeSpan 사용예시]
    public static void UseTimeSpan()
    {
        TimeSpan dday = Convert.ToDateTime("2026-12-24") - DateTime.Now;
        Console.WriteLine($"{DateTime.Now.Year}년도 크리스마스는 {(int)dday.TotalDays}일 남음");

        TimeSpan times = DateTime.Now - Convert.ToDateTime("2002-04-28");
        Console.WriteLine($"내가 지금까지 살아온 일수 : {(int)times.TotalDays} 일");

        Console.WriteLine(GetDateTimeFromYearlyHourNumber(1));
        Console.WriteLine(GetDateTimeFromYearlyHourNumber(8760 / 2));
        Console.WriteLine(GetDateTimeFromYearlyHourNumber(8760));
        static DateTime GetDateTimeFromYearlyHourNumber(int num)
        {
            return new DateTime(2019, 1, 1, 0, 0, 0).AddHours(--num);
        }
    }
}