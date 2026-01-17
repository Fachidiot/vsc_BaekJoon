#include "Map.h"
#include <fstream>
#include <iostream>

Map::Map() : width(0), height(0), maxPoint(0) {}

void Map::readMap(const std::string &filePath)
{
    std::ifstream file(filePath);
    std::string line;
    std::vector<std::string> lines;

    if (!file.is_open())
    {
        std::cerr << "Error: Could not open map file at " << filePath << std::endl;
        return;
    }

    while (std::getline(file, line))
    {
        if (!line.empty() && line.back() == '\r')
        {
            line.pop_back();
        }
        lines.push_back(line);
    }
    file.close();

    if (lines.empty())
        return;

    height = lines.size();
    width = lines[0].size() - 3;
    map.assign(height, std::vector<int>(width));

    int pointCount = 0;
    for (int h = 0; h < height; ++h)
    {
        for (int w = 0; w < width; ++w)
        {
            map[h][w] = lines[h][w] - '0'; // Convert char to int
            if (map[h][w] == static_cast<int>(MapType::POINT))
            {
                pointCount++;
            }
        }
    }
    maxPoint = pointCount;
}

Point Map::getPoint(int value) const
{
    for (int h = 0; h < height; ++h)
    {
        for (int w = 0; w < width; ++w)
        {
            if (map[h][w] == value)
            {
                return {w, h};
            }
        }
    }
    return {-1, -1}; // Not found
}

void Map::printMap() const
{
    for (int h = 0; h < height; ++h)
    {
        for (int w = 0; w < width; ++w)
        {
            switch (static_cast<MapType>(map[h][w]))
            {
            case MapType::WALL:
                std::cout << "# ";
                break;
            case MapType::POINT:
                std::cout << ". ";
                break;
            case MapType::WAY:
            case MapType::PORTAL:
            case MapType::PLAYER:
            case MapType::GHOST1:
            case MapType::GHOST2:
            case MapType::GHOST3:
                std::cout << "  ";
                break;
            default:
                // std::cout << map[h][w];
                break;
            }
        }
        std::cout << std::endl;
    }
}

void readAndWriteFile(const std::string &path)
{
    std::ifstream file(path);
    if (!file.is_open())
    {
        std::cerr << "Error opening file: " << path << std::endl;
        return;
    }
    std::string line;
    while (std::getline(file, line))
    {
        std::cout << line << std::endl;
    }
    file.close();
}