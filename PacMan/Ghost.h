#pragma once
#include "common.hpp"
#include "Map.h"
#include <random>

class Ghost
{
private:
    int x, y;
    Point startPoint;
    Direction direction;
    Direction prevDirection;
    static const int viewDistance = 6;

    std::mt19937 random_engine;

    struct DirectionInfo
    {
        Direction direction;
        bool isPlayerVisible;
        bool isAvailable;
    };

    DirectionInfo getDirectionInfo(const Map &map, Direction dir, int playerX, int playerY);
    MapType getMapTypeInDirection(const Map &map, Direction dir, int distance, int playerX, int playerY);
    Direction getNextDirection(const Map &map, int playerX, int playerY);

public:
    Ghost();
    void initPoint();
    void move(const Map &map, int playerX, int playerY);
    void printGhost() const;

    // Getters and Setters
    int getX() const { return x; }
    int getY() const { return y; }
    void setStartPoint(Point sp) { startPoint = sp; }
};