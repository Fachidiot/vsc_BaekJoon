#include "ConsoleUtils.hpp"
#include <iostream>
#include <termios.h>
#include <unistd.h>
#include <fcntl.h>

void clearScreen()
{
    // ANSI 이스케이프 코드를 사용하여 화면을 지우고 커서를 (1,1)로 이동
    std::cout << "\033[2J\033[1;1H";
}

void setCursorPosition(int x, int y)
{
    // ANSI 이스케이프 코드를 사용하여 커서 위치를 제어
    // 터미널 좌표는 (row, column)이며 1부터 시작하므로 y+1, x+1을 사용
    std::cout << "\033[" << y + 1 << ";" << x + 1 << "H";
}

char getch_nonblocking()
{
    struct termios oldt, newt;
    int ch;
    int oldf;

    // 현재 터미널 설정을 가져옴
    tcgetattr(STDIN_FILENO, &oldt);
    newt = oldt;
    // 정규 모드(canonical)와 에코(echo) 비활성화
    newt.c_lflag &= ~(ICANON | ECHO);
    // 새 설정 적용
    tcsetattr(STDIN_FILENO, TCSANOW, &newt);
    oldf = fcntl(STDIN_FILENO, F_GETFL, 0);
    // 논블로킹(non-blocking)으로 설정
    fcntl(STDIN_FILENO, F_SETFL, oldf | O_NONBLOCK);

    ch = getchar();

    // 터미널 설정을 원래대로 복원
    tcsetattr(STDIN_FILENO, TCSANOW, &oldt);
    fcntl(STDIN_FILENO, F_SETFL, oldf);

    if (ch != EOF)
    {
        return ch;
    }

    return 0; // 입력이 없는 경우
}