class Base {
    int something;
}

class Child : public Child {
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

class AnotherClass {
    float another_var;

}

int main() {
    return 0;
}