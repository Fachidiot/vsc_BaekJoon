#include "CircleManager.hpp"
#include <iostream>
using namespace std;

CircleManager::CircleManager()
{
    cout << "생성하고자 하는 원의 개수 : ";
    cin >> size;
    if (size <= 0)
        return;
    pArray = new Circle[size];
}

CircleManager::~CircleManager()
{
    if (pArray != nullptr)
        delete[] pArray;
}

void CircleManager::input()
{
    int radius;
    for (int i = 0; i < size; i++)
    {
        cout << "원" << i + 1 << " : ";
        cin >> radius;
        pArray[i].setRadius(radius);
    }
}

void CircleManager::decide()
{
    int count = 0;
    Circle *p = pArray;
    for (int i = 0; i < size; i++)
    {
        cout << p->getArea() << ' ';
        if (p->getArea() >= 100 && p->getArea() <= 200)
            count++;
        p++;
    }
    cout << endl
         << "면적이 100에서 200사이인 원의 개수는 " << count << endl;
}

void CircleManager::run()
{
    input();
    decide();
}