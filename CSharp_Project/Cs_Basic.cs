using System;
using System.Collections;
using System.Net.WebSockets;
using System.Text.RegularExpressions;

class Cs_Basic
{
    // [메서드의 매개변수 종류]
    static void Do(int num) => Console.WriteLine(num);  // 값 전달 방식
    static void Do(ref int num) { num = 20; Console.WriteLine(num); }   // 참조 전달 방식(ref)
    static void DoOut(out int num) { num = 30; Console.WriteLine(num); }    // 반환형 전달 방식(out)
    static void SumAll(params int[] numbers) { int sum = 0; foreach (int num in numbers) sum += num; Console.WriteLine(sum); }  // 가변형 전달 방식(params)
    public static void Method_Params()
    {
        int num = 10;
        Do(num);        // 10
        Do(ref num);    // 20
        DoOut(out num); // 30
        SumAll(1, 2, 3);    // 6
        SumAll(4, 5, 6);    // 15
        SumAll(7, 8, 9);    // 24

        DateTime dt;
        if (DateTime.TryParse("2020-01-01", out dt))
            Console.WriteLine(dt);
        else
            Console.WriteLine("Cant convert");

        if (DateTime.TryParse("2020-01-01", out var dt2))
            Console.WriteLine(dt2);
    }

    // [Class 의 ToString Override ]
    class ClassTest()
    {
        public override string ToString()
        {
            return "ClassTest입니다.";
        }
    }
    public static void ClassOverrideToString()
    {
        ClassTest ct = new ClassTest();
        Console.WriteLine(ct);  // "ClassTest" 대신 "ClassTest입니다." 출력
    }

    #region LINQ 알고리즘 사용법
    class Record
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
    // [LINQ 고급 알고리즘 사용법]
    // 근삿값(Near) 알고리즘 사용법 => 데이터에서 특정 값과 가장 가까운 값을 찾는 알고리즘
    // 순위(Rank) 알고리즘 사용법 => 데이터에서 각 요소의 순
    // 정렬(Sort) 알고리즘 사용법 => 데이터를 특정 기준에 따라 정렬하는 알고리즘
    // 검색(Search) 알고리즘 사용법 => 데이터에서 특정 값을 찾는 알고리즘
    // 병합(Merge) 알고리즘 사용법 => 두 개의 정렬된 데이터를 하나의 정렬된 데이터로 병합하는 알고리즘
    // 최빈값(Mode) 알고리즘 사용법 => 데이터에서 가장 빈번하게 나타나는 값을 찾는 알고리즘
    // 그룹(Group) 알고리즘 사용법 => 데이터를 특정 기준으로 그룹화하여 집계하는 알고리즘
    public static void UseLinqAdvancedAlgorithms()
    {
        int[] numbers = { 90, 65, 78, 50, 95 };

        // [Near] 알고리즘 (하나만)
        Console.WriteLine("Near 알고리즘");
        int Abs(int n) => (n < 0) ? -n : n;
        numbers = [0b1010, 0x14, 0b11110, 0x1B, 0b10001];
        int min = int.MaxValue;
        int target = 25, near = default;
        foreach (var n in numbers)
        {
            int abs = Abs(n - target);
            if (abs < min)
            {
                min = abs;
                near = n;
            }
        }
        Console.WriteLine($"식 : {numbers.First(n => Abs(n - target) == min)}, {numbers.Min(m => Math.Abs(m - target))}");
        Console.WriteLine($"문 : {near}, {min}");

        // [Near] 알고리즘 (여러개)
        Console.WriteLine("Near 알고리즘");
        numbers = [10, 20, 23, 27, 17];
        target = 25;
        List<int> nears = new List<int>();
        min = int.MaxValue;

        foreach (var n in numbers)
        {
            if (Math.Abs(n - target) < min)
            {
                min = Math.Abs(n - target);
                nears.Clear();
                nears.Add(n);
            }
            else if (Math.Abs(n - target) == min)
                nears.Add(n);
        }
        Console.WriteLine($"최솟값 : {min}");
        foreach (var n in nears)
            Console.WriteLine(n);

        // [Rank] 알고리즘
        Console.WriteLine("Rank 알고리즘");
        numbers = [90, 87, 100, 95, 80];
        var rankings = Enumerable.Repeat(1, 5).ToArray();
        Console.WriteLine("문 : ");

        foreach (var n in numbers)
        {
            rankings[Array.IndexOf(numbers, n)] = 1;
            foreach (var m in numbers)
            {
                if (n < m)
                    ++rankings[Array.IndexOf(numbers, n)];
            }
            Console.WriteLine($"{n}점 : {rankings[Array.IndexOf(numbers, n)]}등");
        }

        Console.WriteLine("식 : ");
        rankings = numbers.Select(n => numbers.Count(nn => nn > n) + 1).ToArray();
        var ranks = numbers.Select(n => new { Score = n, Rank = numbers.Count(nn => nn > n) + 1 });
        foreach (var r in ranks)
            Console.WriteLine($"{r.Score}점 : {r.Rank}등");

        void Swap(ref int a, ref int b) => (a, b) = (b, a);

        // 기존 Algorithm
        int[] data = { 3, 2, 1, 5, 4 };
        int N = data.Length;
        for (int i = 0; i < N - 1; ++i)
        {
            for (int j = i + 1; j < N; ++j)
            {
                if (data[i] > data[j])
                {
                    Swap(ref data[i], ref data[j]);
                }
            }
        }
        foreach (var n in data)
            Console.Write($"{n}\t");
        Console.WriteLine();

        // LINQ 방식
        Array.Sort(data);   // 1안
        foreach (var n in data)
            Console.Write($"{n}\t");
        Console.WriteLine("or");
        var sort = data.OrderBy(n => n).ToArray();  // 2안
        foreach (var n in sort)
            Console.Write($"{n}\t");
        Console.WriteLine();

        // Serach 알고리즘  (순차/이진 탐색) => 처음부터 끝까지/정렬된 데이터를 절반으로 나누어 탐색
        // [이진 탐색]
        data = [1, 3, 5, 7, 9];
        N = data.Length;
        int search = 3, index = -1;
        bool flag = false;  // 찾으면 true, 못찾으면 false

        int low = 0, high = N - 1;
        while (low <= high)
        {
            int mid = (low + high) / 2;
            if (data[mid] == search)
            {   // 찾으면 플래그, 인덱스 저장후 종료
                flag = true;
                index = mid;
                break;
            }
            if (data[mid] > search) // 찾을 데이터가 작으면 왼쪽 영역으로 이동
                high = mid - 1;
            else    // 찾을 데이터가 크면 오른쪽으로 이동
                low = mid + 1;
            if (flag)
                Console.WriteLine($"{search}를 인덱스 {index}에서 찾았습니다.");
            else
                Console.WriteLine($"{search}를 찾지 못했습니다.");
        }

        // [Merge] 알고리즘
        int[] first = { 1, 3, 5 };
        int[] second = { 2, 4 };
        int M = first.Length;
        N = second.Length;
        int[] merge = new int[M + N];
        int x = 0, y = 0, k = 0;
        while (x < M && y < N)
        {
            if (first[x] < second[y])
                merge[k++] = first[x++];
            else
                merge[k++] = second[y++];
        }
        while (x < M)
            merge[k++] = first[x++];
        while (y < N)
            merge[k++] = second[y++];
        foreach (var n in merge)
            Console.Write($"{n}\t");
        Console.WriteLine();

        // LINQ 방식
        merge = (from o in first select o).Union(from t in second select t).OrderBy(n => n).ToArray();
        merge = first.Union(second).OrderBy(n => n).ToArray();
        foreach (var n in merge)
            Console.Write($"{n}\t");
        Console.WriteLine();

        // [Mode] 알고리즘
        int[] scores = { 1, 3, 4, 3, 5 };
        int[] indexes = new int[5 + 1];
        int max = int.MinValue, mode = default;

        for (int i = 0; i < scores.Length; ++i)
            indexes[scores[i]]++;
        for (int i = 0; i < indexes.Length; ++i)
        {
            if (indexes[i] > max)
            {
                max = indexes[i];
                mode = i;
            }
        }
        Console.WriteLine($"최빈값(문) : {mode} -> {max}번 나타남");
        var modes = scores.GroupBy(v => v).OrderByDescending(g => g.Count()).First();
        int modeCount = modes.Count();
        int frequency = modes.Key;
        Console.WriteLine($"최빈값(식) : {frequency} -> {modeCount}번 나타남");

        List<Record> GetAll()
        {
            return new List<Record>
            {
                new Record { Name = "RADIO", Quantity = 3 },
                new Record { Name = "TV", Quantity = 1 },
                new Record { Name = "RADIO", Quantity = 2 },
                new Record { Name = "DVD", Quantity = 4 },
            };
        }
        void PrintData(string message, List<Record> data)
        {
            Console.WriteLine(message);
            foreach (var d in data)
                Console.WriteLine($"{d.Name} : {d.Quantity}");
        }

        // [Group] 알고리즘
        List<Record> records = GetAll();
        List<Record> groups = new List<Record>();
        N = records.Count;

        for (int i = 0; i < N - 1; ++i)
        {
            for (int j = i + 1; j < N; ++j)
            {
                if (String.Compare(records[i].Name, records[j].Name) > 0)
                {
                    var t = records[i];
                    records[i] = records[j];
                    records[j] = t;
                }
            }
        }

        int subtotal = 0;
        for (int i = 0; i < N; ++i)
        {
            subtotal += records[i].Quantity;
            if ((i + 1) == N || (records[i].Name != records[i + 1].Name))
            {
                groups.Add(new Record { Name = records[i].Name, Quantity = subtotal });
                subtotal = 0;
            }
        }

        PrintData("[1] 정렬된 원본 데이터 : ", records);
        PrintData("[2] 그룹화된 데이터 : ", groups);

        PrintData("[3] LINQ 그룹화된 데이터 : ", records.GroupBy(r => r.Name).Select(g => new Record { Name = g.Key, Quantity = g.Sum(r => r.Quantity) }).ToList());
    }
    // [LINQ 기본 알고리즘 사용법] => 데이터를 처리하는 다양한 알고리즘을 LINQ로 구현하는 방법
    public static void UseLinqBasicAlgorithm()
    {
        // [Avgerage] 알고리즘
        Console.WriteLine("Average 알고리즘");
        int[] numbers = { 90, 65, 78, 50, 95 };
        int sum = 0, count = 0;
        foreach (var n in numbers)
        {
            if (n >= 80 && n <= 95)
            {
                sum += n;
                ++count;
            }
        }
        Console.WriteLine($"식 : {numbers.Where(d => d >= 80 && d <= 95).Average()}");
        Console.WriteLine($"문 : {sum / (double)count}");

        // Max 알고리즘
        Console.WriteLine("Max 알고리즘");
        numbers = [-2, -5, -3, -7, -1];
        int max = int.MinValue;
        foreach (var n in numbers)
            max = n > max ? n : max;
        Console.WriteLine($"식 : {numbers.Max()}");
        Console.WriteLine($"문 : {max}");

        // Min 알고리즘
        Console.WriteLine("Min 알고리즘");
        numbers = [0b0010, 0b1010, 0b1111, 0b0001, 0b0101];
        int min = int.MaxValue;
        foreach (var n in numbers)
        {
            if (n < min && n % 2 == 0)
                min = n;
        }
        Console.WriteLine($"식 : {numbers.Where(n => n % 2 == 0).Min()}");
        Console.WriteLine($"문 : {min}");
    }
    #endregion

    #region LINQ 사용법
    // [LINQ 쿼리식 사용법] => LINQ의 쿼리식을 사용하여 데이터를 처리하는 방법
    public static void UseLinqQuery()
    {
        var numbers = Enumerable.Range(1, 10);

        // 메서드 구문 방식
        numbers = numbers.Where(n => n % 2 == 0).OrderByDescending(n => n).ToArray();
        // 쿼리식 방식
        numbers = (from n in numbers
                   where n % 2 == 0
                   orderby n descending
                   select n).ToArray();

        // LINQ 쿼리식에서 집계 함수 사용법
        Console.WriteLine((from n in numbers
                           where n % 2 == 0
                           select n).Sum());
        Console.WriteLine((from n in numbers
                           where n % 2 == 0
                           select n).Count());
        Console.WriteLine((from n in numbers
                           where n % 2 == 0
                           select n).Average());
        Console.WriteLine((from n in numbers
                           where n % 2 == 0
                           select n).Max());
        Console.WriteLine((from n in numbers
                           where n % 2 == 0
                           select n).Min());
    }
    // [LINQ 확장 메서드 사용법] => LINQ의 확장 메서드를 사용하여 데이터를 처리하는 방법
    public static void UseLinqExtension()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        /* 람다 식 이란?
        람다 식은 다른 말로 화살표 함수(arrow function)라고도 함
        화살표 연산자 또는 람다 연산자(lambda operator)로 표현되는 => 연산자는 일반적으로 영어로는 ‘goes to’ 또는 ‘arrow’로 발음함
        우리말로 번역하면 ‘이동’이라는 의미
        람다 식은 이 두 가지 형태를 구분해서 식 람다(expression lambda)와 문 람다(statement lambda)로 표현하기도 함
        */

        // Where 함수 => 조건에 맞는 요소들을 필터링하는 함수
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        Console.WriteLine("짝수: " + string.Join(", ", evenNumbers));

        // Count 함수 => 조건에 맞는 요소의 개수를 세는 함수
        int count = numbers.Count(n => n % 2 == 0);
        Console.WriteLine($"짝수의 개수: {count}");

        // Average 함수 => 조건에 맞는 요소들의 평균을 계산하는 함수
        double average = numbers.Where(n => n > 5).Average();
        Console.WriteLine($"5보다 큰 수의 평균: {average:#,###.##}");

        // Max 함수 => 조건에 맞는 요소들 중 최대값을 반환하는 함수
        int max = numbers.Where(n => n % 2 != 0).Max();
        Console.WriteLine($"홀수 중 최대값: {max}");
        // Min 함수 => 조건에 맞는 요소들 중 최소값을 반환하는 함수
        int min = numbers.Where(n => n % 2 != 0).Min();
        Console.WriteLine($"홀수 중 최소값: {min}");

        // All 함수 => 모든 요소가 조건을 만족하는지 여부를 반환하는 함수
        bool allEven = numbers.All(n => n % 2 == 0);
        Console.WriteLine($"모든 수가 짝수인가? {allEven}");
        // Any 함수 => 하나 이상의 요소가 조건을 만족하는지 여부를 반환하는 함수
        bool anyEven = numbers.Any(n => n % 2 == 0);
        Console.WriteLine($"하나 이상의 수가 짝수인가? {anyEven}");

        // Take 함수 => 조건에 맞는 요소들 중 처음 n개를 반환하는 함수
        var firstThreeEven = numbers.Where(n => n % 2 == 0).Take(3);
        Console.WriteLine("처음 3개의 짝수: " + string.Join(", ", firstThreeEven));
        // Skip 함수 => 조건에 맞는 요소들 중 처음 n개를 건너뛰고 나머지를 반환하는 함수
        var skipTwoEven = numbers.Where(n => n % 2 == 0).Skip(2);
        Console.WriteLine("처음 2개의 짝수를 건너뛴 나머지 짝수: " + string.Join(", ", skipTwoEven));

        // Distinct 함수 => 중복된 요소를 제거하는 함수
        var distinctNumbers = numbers.Distinct();
        Console.WriteLine("중복 제거된 수: " + string.Join(", ", distinctNumbers));

        // OrderBy 함수 => 요소들을 오름차순으로 정렬하는 함수
        var orderedNumbers = numbers.OrderBy(n => n);
        Console.WriteLine("오름차순 정렬: " + string.Join(", ", orderedNumbers));
        // OrderByDescending 함수 => 요소들을 내림차순으로 정렬하는 함수
        var orderedDescendingNumbers = numbers.OrderByDescending(n => n);
        Console.WriteLine("내림차순 정렬: " + string.Join(", ", orderedDescendingNumbers));

        // 확장 메서드 체이닝 : 여러 개의 확장 메서드를 연속적으로 호출하여 데이터를 처리하는 방법
        var chainedResult = numbers.Where(n => n % 2 == 0).OrderByDescending(n => n).Take(3);
        Console.WriteLine("체인된 결과: " + string.Join(", ", chainedResult));

        // Single 함수 => 조건에 맞는 요소가 하나만 존재하는지 확인하고 그 요소를 반환하는 함수
        List<string> colors = new List<string> { "Red", "Green", "Blue" };
        string singleColor = colors.Single(c => c == "Green");
        Console.WriteLine($"조건에 맞는 단일 요소: {singleColor}");
        // SingleOrDefault 함수 => 조건에 맞는 요소가 하나만 존재하는지 확인하고 그 요소를 반환하거나, 없으면 기본값을 반환하는 함수
        string nullColor = colors.SingleOrDefault(c => c == "Yellow");
        Console.WriteLine($"조건에 맞는 요소가 없을 때 기본값: {nullColor ?? "null"}");

        // First 함수 => 조건에 맞는 첫 번째 요소를 반환하는 함수
        Console.WriteLine(colors.First(c => c == "Red"));
        // FirstOrDefault 함수 => 조건에 맞는 첫 번째 요소를 반환하거나, 없으면 기본값을 반환하는 함수
        Console.WriteLine(colors.FirstOrDefault(c => c == "Yellow") ?? "null");

        // Last 함수 => 조건에 맞는 마지막 요소를 반환하는 함수        
        Console.WriteLine(colors.Last(c => c == "Blue"));
        // LastOrDefault 함수 => 조건에 맞는 마지막 요소를 반환하거나, 없으면 기본값을 반환하는 함수
        Console.WriteLine(colors.LastOrDefault(c => c == "Yellow") ?? "null");

        // Select 함수 => 각 요소를 변환하여 새로운 시퀀스를 생성하는 함수
        var selectednums = numbers.Select(n => n * n);
        Console.WriteLine("각 요소의 제곱: " + string.Join(", ", selectednums));
        var names = new List<string> { "Alice", "Bob", "Charlie" };
        var nameObjects = names.Select(name => new { Name = name });    // 익명 타입으로 이름을 가진 객체 생성
        foreach (var obj in nameObjects)
            Console.WriteLine(obj.Name);

        // Foreach()로 반복 출력하기
        names.ForEach(n => Console.WriteLine(n));

        // Zip 함수 => 두 개의 시퀀스를 병합하여 새로운 시퀀스를 생성하는 함수
        var numbersAndNames = numbers.Zip(names, (first, second) => first + "-" + second);
        Console.WriteLine("숫자와 이름을 병합: " + string.Join(", ", numbersAndNames));

        // SelectMany 함수 => 각 요소를 변환하여 새로운 시퀀스를 생성하는 함수, 변환된 시퀀스들을 하나의 시퀀스로 평탄화하는 기능도 포함
        var nestedNumbers = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 }, new List<int> { 5, 6 } };
        var flattenedNumbers = nestedNumbers.SelectMany(n => n);
        Console.WriteLine("평탄화된 숫자: " + string.Join(", ", flattenedNumbers));

        // TakeWhile 함수 => 조건이 참인 동안 요소를 반환하는 함수
        var takeWhileResult = numbers.TakeWhile(n => n < 5);
        Console.WriteLine("5보다 작은 수: " + string.Join(", ", takeWhileResult));
        // SkipWhile 함수 => 조건이 참인 동안 요소를 건너뛰고 나머지를 반환하는 함수
        var skipWhileResult = numbers.SkipWhile(n => n < 5);
        Console.WriteLine("5보다 작은 수를 건너뛴 나머지: " + string.Join(", ", skipWhileResult));
    }
    #endregion

    #region Generic
    // [Dictionary] => 제네릭 컬렉션, 다양한 타입의 데이터를 저장할 수 있고 타입 안정성이 보장됨
    // (키-값 쌍  / 순서 없음 / Key 기반 검색 BigO(1) / 값 중복 허용, 키 중복 허용 안함)
    public static void UseDictionary()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();

        data.Add("cs", "C#");
        data.Add("aspx", "ASP.NET");

        data.Remove("aspx");
        data["cshtml"] = "ASP.NET MVC";

        try
        {
            data["cs"] = "CSharp";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        foreach (var item in data)
            Console.WriteLine($"{item.Key}는 {item.Value}의 확장자입니다.");

        Console.WriteLine(data["cs"]);

        try
        {
            Console.WriteLine(data["vb"]);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        if (data.TryGetValue("cs", out var csharp))
            Console.WriteLine(csharp);
        else
            Console.WriteLine("cs 키가 없습니다.");

        if (!data.ContainsKey("json"))
        {
            data.Add("json", "JSON");
            Console.WriteLine(data["json"]);
        }

        var values = data.Values;
        foreach (var v in values)
            Console.WriteLine($"{v}\t");
        Console.WriteLine();
    }
    // [Enumerable] => LINQ 알고리즘에서 사용되는 클래스, 다양한 시퀀스 연산을 제공
    public static void UseEnumerable()
    {
        var numbers = Enumerable.Range(1, 5);
        foreach (var n in numbers)
            Console.Write($"{n}\t");
        Console.WriteLine();

        var sameNumbers = Enumerable.Repeat(-1, 5);
        foreach (var n in sameNumbers)
            Console.Write($"{n}\t");
        Console.WriteLine();

        string str = string.Join(", ", Enumerable.Range(1, 5));
        Console.WriteLine(str);

        Enumerable.Range(1, 100).Where(i => i % 2 == 0).Reverse().Skip(10).Take(5);
        // => { 80, 78, 76, 74, 72 }
    }
    // [List<T>] => 제네릭 컬렉션, 다양한 타입의 데이터를 저장할 수 있고 타입 안정성이 보장됨
    // (Index기반 / 순서 있음 / 선형 검색BigO(n) / 값 중복 허용)
    public static void UseList()
    {
        List<int> iList = new List<int>();
        List<string> sList = new List<string>();

        iList.Add(1);
        sList.Add("Hello");
        iList.Add(2);
        sList.Add("World");
        iList.Add(3);

        foreach (var item in iList)
            Console.WriteLine(item);
        foreach (var item in sList)
            Console.WriteLine(item);
    }
    // [Queue<T>] => FIFO(First In First Out) 구조
    public static void UseQueueGeneric()
    {
        Queue<int> iQueue = new Queue<int>();
        iQueue.Enqueue(1);
        iQueue.Enqueue(2);
        iQueue.Enqueue(3);

        while (iQueue.Count > 0)
            Console.WriteLine(iQueue.Dequeue());

        Queue<string> sQueue = new Queue<string>();
        sQueue.Enqueue("Hello");
        sQueue.Enqueue("World");

        while (sQueue.Count > 0)
            Console.WriteLine(sQueue.Dequeue());
    }
    // [Stack<T>] => LIFO(Last In First Out) 구조
    public static void UseStackGeneric()
    {
        Stack<int> iStack = new Stack<int>();
        iStack.Push(1);
        iStack.Push(2);
        iStack.Push(3);

        while (iStack.Count > 0)
            Console.WriteLine(iStack.Pop());

        Stack<string> sStack = new Stack<string>();
        sStack.Push("Hello");
        sStack.Push("World");

        while (sStack.Count > 0)
            Console.WriteLine(sStack.Pop());
    }
    #endregion

    #region Collection 
    // [Hashtable] => 비제네릭 컬렉션, 다양한 타입의 데이터를 저장할 수 있지만 타입 안정성이 떨어짐
    // (키-값 쌍  / 순서 없음 / Key 기반 검색 BigO(1) / 값 중복 허용, 키 중복 허용 안함)
    public static void UseHashtable()
    {
        Hashtable hash = new Hashtable();

        hash[0] = "Zero";
        hash[1] = "One";
        hash[2] = "Two";

        Console.WriteLine(hash[0]);
        Console.WriteLine(hash[1]);
        Console.WriteLine(hash[2]);

        foreach (var key in hash.Keys)
            Console.WriteLine($"{key} : {hash[key]}");
    }
    // [ArrayList] => 비제네릭 컬렉션, 다양한 타입의 데이터를 저장할 수 있지만 타입 안정성이 떨어짐 
    // (Index기반 / 순서 있음 / 선형 검색BigO(n) / 값 중복 허용)
    public static void UseArrayList()
    {
        ArrayList list = new ArrayList();

        list.Add(1);
        list.Add("Two");
        list.Add(3.0);

        foreach (var item in list)
            Console.WriteLine(item);
    }
    // [Queue] => FIFO(First In First Out) 구조
    public static void UseQueue()
    {
        Queue queue = new Queue();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        while (queue.Count > 0)
            Console.WriteLine(queue.Dequeue());
    }
    // [Stack] => LIFO(Last In First Out) 구조
    public static void UseStack()
    {
        Stack stack = new Stack();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        while (stack.Count > 0)
            Console.WriteLine(stack.Pop());
    }
    #endregion

    // [예외 처리]
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