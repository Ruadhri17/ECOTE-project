#include <iostream>
#include <string>

#include "ring.hpp"

using namespace std;

int main()
{
    //TEST OF SPLIT FUNCTION//
    Ring<int, int> Source;
    Ring<int, int> Result1;
    Ring<int, int> Result2;
    for(int i = 1; i <= 12; i++)
       {
       Source.push_back(i, i);
       Result1.push_back(i, i);
       }
    cout << "Source: " << endl;
    Source.print();
    
    split(Source, false, Result1, 3, 2, Result2, 2, 4);
    
    cout << "Result1:" << endl;
    Result1.print();
    
    cout << "Result2:" << endl;
    Result2.print();
    cout << endl << endl;
    
    Ring<int, int> Ring1;
    //INSERTION TEST 1//
    Ring1.insert(6,6,2); // won't execute because there is no such occurance of key
    Ring1.push_back(4,4);
    Ring1.push_front(1,1);
    Ring1.insert(1,5,1);
    Ring1.push_back(3,3);
    
    //PRINT TEST//
    Ring1.print();
    
    cout << endl << endl;
    
    //REMOVAL TEST//
    Ring1.pop_front();
    Ring1.pop_back();
    Ring1.remove(1,2); // there is no such case because only one Key of this type left
    
    //PRINT TEST//
    cout << "Ring1:" << endl;
    Ring1.print();
    
    //COPY CONSTRUCTOR//
    Ring<int, int> Ring2 = Ring1;
    Ring<int, int> *Ring3 = &Ring1;

    cout << "Ring2:" << endl;
    Ring2.print();
    
    cout << "Ring3:" << endl;
    Ring3->print();
    cout << endl << endl;

    //EQUALITY TEST 1//
    if(Ring1 == Ring2)
        cout << "Ring1 and Ring2 are equal!" << endl;

    //REMOVAL TEST 2//
    Ring1.remove_all();

    //EQUALITY TEST 2//

    if(Ring1 != Ring2)
        cout << "Ring1 and Ring2 are not equal!" << endl;

    cout << "Ring1:" << endl;
    Ring1.print();
    
    cout << "Ring2:" << endl;
    Ring2.print();
    
    cout << "Ring3:" << endl;
    Ring3->print();
    
    cout << endl << endl;

    //INSERTION TEST 2//
    Ring1.push_front(1,1);
    Ring1.push_back(2,4);
    Ring1.push_front(3,3);
    Ring1.insert(2,2,1);

    cout << "Ring1:" << endl;
    Ring1.print();

    //REMOVAL TEST 3//
    Ring1.remove_all_occurences(2);
    cout << "Ring1:" << endl;
    Ring1.print();

    cout << endl << endl;

    return 0;
}