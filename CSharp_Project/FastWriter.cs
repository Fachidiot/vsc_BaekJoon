/// <summary>
/// 빠른 출력을 위한 커스텀 Stream writer
/// 기존 StreamWriter는 int를 출력하기 위해 char배열로 바꿈. (내부적으로 ToString()과 유사한 작업 발생)
/// 해당 char배열을 다시 UTF-8 byte로 인코딩해서 버퍼에 넣음.
/// 위 작업을 반복문에서 실행시 병목이 발생하기 때문에 이를 해결 하기위한 커스텀 Stream헬퍼
/// </summary>

class FastWriter
{
    private const int BufferSize = 65536;
    private byte[] buffer = new byte[BufferSize];
    private int pos = 0;
    private Stream stream = Console.OpenStandardOutput();
    private byte[] temp = new byte[10]; // 자릿수 쪼개기용 재사용 버퍼 (가비지 0)

    public void WriteIntAndSpace(int n)
    {
        if (n == 0)
        {
            WriteByte((byte)'0');
            WriteByte((byte)' ');
            return;
        }

        int tempPos = 0;
        // 1. 숫자를 10으로 나누면서 끝자리부터 쪼개서 ASCII byte로 변환 (예: 123 -> '3', '2', '1')
        while (n > 0)
        {
            temp[tempPos++] = (byte)((n % 10) + '0');
            n /= 10;
        }

        // 2. 뒤집힌 순서를 다시 원래대로 출력 버퍼에 집어넣음
        for (int i = tempPos - 1; i >= 0; i--)
        {
            WriteByte(temp[i]);
        }

        // 3. 마지막에 공백 추가
        WriteByte((byte)' ');
    }

    private void WriteByte(byte b)
    {
        if (pos == BufferSize) Flush();
        buffer[pos++] = b;
    }

    public void Flush()
    {
        if (pos > 0)
        {
            stream.Write(buffer, 0, pos);
            pos = 0;
        }
    }
}