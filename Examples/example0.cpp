class Vehicle {
  public:
    Vehicle()
    {
      cout << "This is a Vehicle\n";
    }
};
 

class FourWheeler {
  public:
    FourWheeler()
    {
      cout << "This is a 4 wheeler Vehicle\n";
    }
};
 

class Car : public Vehicle, public FourWheeler {
 
};

class A : public C {

}
class B {

}
class C : public A, public B {

}
class D: public B, public A { 

}

class Example1 {

}
class Example2 : private class Example1 {

}
class Example3 : protected class Example2 {

}
class Alone {

}
class AnotherAlone {

}

int main() {
     
    Car obj;
    return 0;
}