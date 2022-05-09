#include <iostream>
using namespace std;

class root {

    public:
    double length;
    double breadth;
    double height;
};

class child : root {

}

class childchild : virtual child {


class childchildchild : public virtual childchild {

}

class anotherRoot;

class seconndchild : protected virtual root;


class yetanotherRoot {

}

class childchildsequel : private childchild {

}

class childofsth : protected yetanotherRoot {
    public:
    int sth;
}