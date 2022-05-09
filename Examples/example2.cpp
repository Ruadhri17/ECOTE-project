class B;

class A {

    public:
    double length;
    double breadth;
    double height;
};

class AA : A {
    public:
    int ree;
}

class BB : public B {
    int bb;
}

class BBB : private BB {
    int bbb;
}

class B : public virtual A{
    int b;
}


class D;

class Badsf : protected virtual BB {

}

class DD : public D;

class DD2 : public D {

}

class DDD : private DD {
    
}

class D : protected AA {}