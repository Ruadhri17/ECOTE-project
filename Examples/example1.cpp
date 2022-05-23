#include <iostream>
using namespace std;

class Root {

    public: 
    double length;
    double breadth;
    double height;
};

class Child : root {

}

class ChildChild : virtual Child {


class ChildChildChild : public virtual ChildChild {

}

class AnotherRoot;

class SecondChild : protected virtual Root;


class YetAnotherRoot {

}

class ChildChildSequel : private ChildChild {

}

class ChildOfSth : protected YetAnotherRoot {
    public:
    int sth;
}