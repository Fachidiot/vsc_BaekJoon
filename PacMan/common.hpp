#pragma once

enum class Direction
{
    Left = 0,
    Right = 1,
    Up = 2,
    Down = 3
};

enum class MapType
{
    NONE = -1,
    WALL = 0,
    POINT = 1,
    PORTAL = 2,
    WAY = 3,
    PLAYER = 4,
    GHOST1 = 7,
    GHOST2 = 8,
    GHOST3 = 9,
};