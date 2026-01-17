#include <iostream>
#include "Circle.hpp"

using namespace std;

int Circle::numOfCircles = 0;

Circle::Circle()
{
    int r = 1;
    ++numOfCircles;
    // Circle(1);
}

Circle::Circle(const Circle &circle)
{
    this->radius = circle.radius;
    cout << "복사 생성자 실행, radius = " << radius << '\n';
}

Circle::Circle(int r)
{
    radius = r;
    ++numOfCircles;
    // cout << "생성자 실행 radius = " << radius << endl;
}

Circle::~Circle()
{
    --numOfCircles;
    // cout << "소멸자 실행 radius = " << radius << endl;
}

double Circle::getArea()
{
    return 3.14f * radius * radius;
}