class Circle
{
    static int numOfCircles;
    int radius;

public:
    Circle(const Circle &circle);
    Circle();
    Circle(int r);
    ~Circle();
    int getRadius() { return radius; }
    void setRadius(int r) { radius = r; }
    double getArea();
    static int getNumOfCircles() { return numOfCircles; }
};