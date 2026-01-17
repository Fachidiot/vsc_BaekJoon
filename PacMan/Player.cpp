#include "Map.h"
#include "Player.h"
#include "ConsoleUtils.hpp"
#include <iostream>

Player::Player(int p, int l, Direction d) : point(p), life(l), direction(d), x(0), y(0) {}

void Player::initPoint()
{
    x = startPoint.x;
    y = startPoint.y;
}

void Player::processInput()
{
    char key = getch_nonblocking();
    if (key == 0)
        return;

    switch (key)
    {
    case 'a':
    case 'A':
    case 75:
        direction = Direction::Left;
        break;
    case 'd':
    case 'D':
    case 77:
        direction = Direction::Right;
        break;
    case 'w':
    case 'W':
    case 72:
        direction = Direction::Up;
        break;
    case 's':
    case 'S':
    case 80:
        direction = Direction::Down;
        break;
    }
}

void Player::move(Map &map)
{
    switch (direction)
    {
    case Direction::Left:
        // 포탈 체크
        if (static_cast<MapType>(map.getMapData()[y][x]) == MapType::PORTAL)
        {
            x = map.getWidth() - 1;
            break; // switch 문 탈출
        }
        // 경계선 또는 벽 체크
        if (x <= 0 || static_cast<MapType>(map.getMapData()[y][x - 1]) == MapType::WALL)
        {
            break; // 이동하지 않고 switch 문 탈출
        }
        // 점수 아이템 체크
        if (static_cast<MapType>(map.getMapData()[y][x - 1]) == MapType::POINT)
        {
            point++;
            map.getMapData()[y][x - 1] = static_cast<int>(MapType::WAY);
        }
        // 모든 체크를 통과했으면 이동
        x--;
        break;

    case Direction::Right:
        // 포탈 체크
        if (static_cast<MapType>(map.getMapData()[y][x]) == MapType::PORTAL)
        {
            x = 0;
            break;
        }
        // 경계선 또는 벽 체크
        if (x >= map.getWidth() - 1 || static_cast<MapType>(map.getMapData()[y][x + 1]) == MapType::WALL)
        {
            break;
        }
        // 점수 아이템 체크
        if (static_cast<MapType>(map.getMapData()[y][x + 1]) == MapType::POINT)
        {
            point++;
            map.getMapData()[y][x + 1] = static_cast<int>(MapType::WAY);
        }
        x++;
        break;

    case Direction::Up:
        // 경계선 또는 벽 체크
        if (y <= 0 || static_cast<MapType>(map.getMapData()[y - 1][x]) == MapType::WALL)
        {
            break;
        }
        // 점수 아이템 체크
        if (static_cast<MapType>(map.getMapData()[y - 1][x]) == MapType::POINT)
        {
            point++;
            map.getMapData()[y - 1][x] = static_cast<int>(MapType::WAY);
        }
        y--;
        break;

    case Direction::Down:
        // 경계선 또는 벽 체크 (map.getHeight() - 1 이 올바른 경계입니다)
        if (y >= map.getHeight() - 1 || static_cast<MapType>(map.getMapData()[y + 1][x]) == MapType::WALL)
        {
            break;
        }
        // 점수 아이템 체크
        if (static_cast<MapType>(map.getMapData()[y + 1][x]) == MapType::POINT)
        {
            point++;
            map.getMapData()[y + 1][x] = static_cast<int>(MapType::WAY);
        }
        y++;
        break;
    }

    // ====================== DEBUGGING 출력 코드 ======================
    setCursorPosition(50, 1); // 디버그 정보를 화면 오른쪽 상단(가로 60, 세로 1)에 고정 출력
    std::cout << "[DEBUG] x=" << x << " mapW=" << map.getWidth();
    setCursorPosition(50, 2); // 디버그 정보를 화면 오른쪽 상단(가로 60, 세로 1)에 고정 출력
    std::cout << "[DEBUG] y=" << y << " mapH=" << map.getHeight();
    // ===============================================================
}

void Player::printPlayer() const
{
    setCursorPosition(x * 2, y + 1); // 콘솔 유틸리티 함수 사용
    switch (direction)
    {
    case Direction::Up:
        std::cout << "▲";
        break;
    case Direction::Down:
        std::cout << "▼";
        break;
    case Direction::Left:
        std::cout << "◀";
        break;
    case Direction::Right:
        std::cout << "▶";
        break;
    }
}

bool Player::detectGhost(int ghostX, int ghostY) const
{
    return x == ghostX && y == ghostY;
}