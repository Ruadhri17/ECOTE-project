class Base {
    int something;
}
class Base2 {

}

class Child : public virtual Base {
    private: 
    int test1;
    char test2;
    public:
    Child() {
        test1 = 0;
        test2 = '';
    }
    Child(int t1, char t2) {
        test1 = t1;
        test2 = t2;
    }
}

class AnotherClass : public Base, public Base2 {
    float another_var;

}

int main() {
    return 0;
}