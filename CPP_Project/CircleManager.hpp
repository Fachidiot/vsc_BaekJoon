#include "Circle.hpp"

class CircleManager
{
    Circle *pArray = nullptr;
    int size = 0;
    void input();
    void decide();

public:
    CircleManager();
    ~CircleManager();
    void run();
};