#include "Ghost.h"
#include <vector>
#include <algorithm>
#include <iostream>

void setCursorPosition(int x, int y); // Assuming defined elsewhere

Ghost::Ghost() : x(0), y(0), direction(Direction::Up), prevDirection(Direction::Up)
{
    // Seed the random number generator
    std::random_device rd;
    random_engine = std::mt19937(rd());
}

void Ghost::initPoint()
{
    x = startPoint.x;
    y = startPoint.y;
}

Ghost::DirectionInfo Ghost::getDirectionInfo(const Map &map, Direction dir, int playerX, int playerY)
{
    DirectionInfo info = {dir, false, false};

    for (int i = 1; i <= viewDistance; ++i)
    {
        MapType mapType = getMapTypeInDirection(map, dir, i, playerX, playerY);
        if (mapType == MapType::NONE || mapType == MapType::WALL)
        {
            break;
        }
        if (mapType == MapType::PLAYER)
        {
            info.isPlayerVisible = true;
        }
        info.isAvailable = true;
    }
    return info;
}

MapType Ghost::getMapTypeInDirection(const Map &map, Direction dir, int distance, int pX, int pY)
{
    int destX = x;
    int destY = y;
    switch (dir)
    {
    case Direction::Left:
        destX -= distance;
        break;
    case Direction::Right:
        destX += distance;
        break;
    case Direction::Up:
        destY -= distance;
        break;
    case Direction::Down:
        destY += distance;
        break;
    }

    if (destY < 0 || destX < 0 || destY >= map.getHeight() || destX >= map.getWidth())
    {
        return MapType::NONE;
    }

    if (destY == pY && destX == pX)
    {
        return MapType::PLAYER;
    }

    return static_cast<MapType>(map.getMapData()[destY][destX]);
}

Direction Ghost::getNextDirection(const Map &map, int playerX, int playerY)
{
    std::vector<DirectionInfo> directions;
    directions.push_back(getDirectionInfo(map, Direction::Up, playerX, playerY));
    directions.push_back(getDirectionInfo(map, Direction::Down, playerX, playerY));
    directions.push_back(getDirectionInfo(map, Direction::Left, playerX, playerY));
    directions.push_back(getDirectionInfo(map, Direction::Right, playerX, playerY));

    directions.erase(std::remove_if(directions.begin(), directions.end(),
                                    [](const DirectionInfo &d)
                                    { return !d.isAvailable; }),
                     directions.end());

    auto it = std::find_if(directions.begin(), directions.end(),
                           [](const DirectionInfo &d)
                           { return d.isPlayerVisible; });

    if (it != directions.end())
    {
        return it->direction;
    }

    if (directions.size() > 1)
    {
        directions.erase(std::remove_if(directions.begin(), directions.end(),
                                        [this](const DirectionInfo &d)
                                        { return d.direction == this->prevDirection; }),
                         directions.end());
    }

    if (directions.empty())
    {
        return direction; // No valid move, stay put
    }

    std::uniform_int_distribution<int> dist(0, directions.size() - 1);
    return directions[dist(random_engine)].direction;
}

void Ghost::move(const Map &map, int playerX, int playerY)
{
    direction = getNextDirection(map, playerX, playerY);

    switch (direction)
    {
    case Direction::Up:
        prevDirection = Direction::Down;
        y--;
        break;
    case Direction::Down:
        prevDirection = Direction::Up;
        y++;
        break;
    case Direction::Left:
        prevDirection = Direction::Right;
        x--;
        break;
    case Direction::Right:
        prevDirection = Direction::Left;
        x++;
        break;
    }
    // Note: Portal logic for ghosts was missing in the original, can be added here if needed.
}

void Ghost::printGhost() const
{
    setCursorPosition(x * 2, y + 1);
    switch (direction)
    {
    case Direction::Up:
        std::cout << "△";
        break;
    case Direction::Down:
        std::cout << "▽";
        break;
    case Direction::Left:
        std::cout << "◁";
        break;
    case Direction::Right:
        std::cout << "▷";
        break;
    }
}