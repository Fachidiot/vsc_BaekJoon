using System;
using System.IO;

class FastReader
{
    private const int BufferSize = 65536; // 64KB 단위로 바이트를 뭉텅이로 퍼옵니다.
    private byte[] buffer = new byte[BufferSize];   // 저장할 버퍼.
    private int head = 0, tail = 0;

    // StreamReader를 버리고, 가장 원초적인 Stream사용
    private Stream stream = Console.OpenStandardInput();

    private byte Read()
    {
        if (head == tail)   // 모두다 읽었을때 & 초기.
        {
            head = 0;
            tail = stream.Read(buffer, 0, BufferSize);  // 버퍼에 저장해주고 tail값 저장.
            if (tail == 0) return 0;
        }
        return buffer[head++];  // 제일 앞의 값 리턴.
    }

    public int NextInt()
    {
        byte c = Read(); // 글자를 하나씩(byte 단위로) 읽어온다.

        // 1. 공백(' ')이나 줄바꿈('\n')이 나오면 다 무시하고 다음 글자를 찾기.
        while (c <= ' ')
        {
            if (c == 0) return 0; // 파일의 끝(EOF)이면 종료
            c = Read();
        }

        // 2. 음수 처리 (마이너스 기호가 있으면 체크해둠)
        bool minus = false;
        if (c == '-')
        {
            minus = true;
            c = Read();
        }

        // 3. 진짜 숫자를 조립하는 마법의 구간
        int ret = 0;
        while (c > ' ')
        {
            // 아스키코드표에서 '0'을 빼서 실제 숫자로 만들고, 자릿수를 올려가며 더함
            ret = ret * 10 + (c - '0');
            c = Read();
        }

        return minus ? -ret : ret;
    }
}