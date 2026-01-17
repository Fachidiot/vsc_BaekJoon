#pragma once
#include "common.hpp"
#include <vector>
#include <string>

// Represents a position with x and y coordinates.
struct Point
{
    int x;
    int y;
};

class Map
{
private:
    int width;
    int height;
    int maxPoint;
    std::vector<std::vector<int>> map;

public:
    Map();
    void readMap(const std::string &filePath);
    void printMap() const;
    Point getPoint(int value) const;

    // Getters
    int getWidth() const { return width; }
    int getHeight() const { return height; }
    int getMaxPoint() const { return maxPoint; }
    const std::vector<std::vector<int>> &getMapData() const { return map; }
    std::vector<std::vector<int>> &getMapData() { return map; }
};

// Utility function replacing the StringMap class
void readAndWriteFile(const std::string &path);
