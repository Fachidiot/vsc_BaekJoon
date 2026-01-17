#pragma once

// 화면을 깨끗하게 지웁니다.
void clearScreen();

// 지정된 (x, y) 좌표로 커서를 이동시킵니다.
void setCursorPosition(int x, int y);

// 키를 누르는 즉시 문자를 입력받는 함수 (non-blocking input).
char getch_nonblocking();