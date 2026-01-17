#pragma once
#include "common.hpp"
#include "Map.h"
#include "Ghost.h" // Forward declaration might be better if headers are complex
#include <vector>

class Ghost; // Forward declaration to avoid circular dependency

class Player
{
private:
    int x, y;
    int point;
    int life;
    Direction direction;
    Point startPoint;

public:
    Player(int p, int l, Direction d);

    void initPoint();
    void processInput();
    void move(Map &map);
    void printPlayer() const;
    bool detectGhost(int ghostX, int ghostY) const;

    // Getters and Setters
    int getX() const { return x; }
    int getY() const { return y; }
    int getPoint() const { return point; }
    int getLife() const { return life; }
    void setLife(int newLife) { life = newLife; }
    void setStartPoint(Point sp) { startPoint = sp; }
};