#ifndef TREE_HPP
#define TREE_HPP
#include <iostream>
#include <string>
#define COUNT 10
using namespace std;

template <typename Key, typename Info>
class Dictionary {
private:
    struct Node
    {
        Key key_of_node;
        Info info_of_node;
        Node *left;
        Node *right;
        int height;
        Node(Key k, Info i)
            {
                key_of_node = k;
                info_of_node = i;
                left = nullptr;
                right = nullptr;
                height = 1;
            }
        ~Node()
            {
                if(left != nullptr)
                    delete left;
                if(right != nullptr)
                    delete right;
            }
    };
    
    Node* root; //
    Node* rotate_left(Node* x); //
    Node* rotate_right(Node* x); //
    Node* insert(Node* x, Key add_key, Info add_info); // 
    Node* remove(Node* x, Key remove_key); //
    void print_tree(Node* x, int space) const; //
    void print_preorder(Node* x) const; //
    void print_postorder(Node* x) const; //
    void print_inorder(Node* x) const; //
    Node* minimal_value(Node* x) const; //
    int max_height(int a, int b) const; //
    int get_height(Node* x) const; //
    Node* copy_node(Node* x); //
    bool compare_node(Node* x, Node* y) const; //

public:
    Dictionary(); // //
    ~Dictionary(); // // 
    Dictionary(const Dictionary<Key, Info> &x); // // 
    Dictionary<Key, Info> &operator=(const Dictionary<Key, Info> &x); // // 
    void print_tree() const; // //
    void print_preorder() const; // //
    void print_postorder() const; // //
    void print_inorder() const; // //
    void insert(Key add_key, Info add_info); // //
    void remove(Key remove_key); // // 
    bool remove_all(); // // 
    bool operator ==(const Dictionary<Key, Info> &x) const; // // 
    bool operator !=(const Dictionary<Key, Info> &x) const; // //
    bool is_empty() const; // // 
    bool search(Node* x, Key find_key) const; //
};
template <typename Key, typename Info>
Dictionary<Key,Info>::Dictionary()
{
    root = nullptr;
}
template <typename Key, typename Info>
Dictionary<Key,Info>::~Dictionary()
{
    remove_all();
}
template <typename Key, typename Info>
Dictionary<Key,Info>::Dictionary(const Dictionary<Key, Info> &x)
{
    root = copy_node(x.root);
}
template <typename Key, typename Info>
Dictionary<Key, Info> &Dictionary<Key, Info>::operator=(const Dictionary<Key, Info> &x)
{
    if(this != &x)
        {
            this->remove_all();
            root = copy_node(x.root);
            return *this;
        }
    else
        {
            return *this;
        }
    
}
template <typename Key, typename Info>
bool Dictionary<Key, Info>::operator==(const Dictionary<Key, Info> &x) const
{
    if(compare_node(root, x.root) == true)
        return true;
    else
        return false;
}
template <typename Key, typename Info>
bool Dictionary<Key, Info>::operator!=(const Dictionary<Key, Info> &x) const
{
    
    if(compare_node(root, x.root) == true)
        return false;
    else
        return true;
}
template <typename Key, typename Info>
typename Dictionary<Key, Info>::Node* Dictionary<Key, Info>:: rotate_left(Node* x)
{

            Node *temp;
            temp = x->right;
            x->right = temp->left;
            temp->left = x;

            x->height = 1 + max_height(get_height(x->left), get_height(x->right));
            temp->height = 1 + max_height(get_height(temp->left), get_height(temp->right));
            return temp;
}
template <typename Key, typename Info>
typename Dictionary<Key, Info>::Node* Dictionary<Key, Info>:: rotate_right(Node* x)
{
            Node *temp; 
            temp = x->left; 
            x->left = temp->right;
            temp->right = x;

            x->height = 1 + max_height(get_height(x->left), get_height(x->right));
            temp->height = 1 + max_height(get_height(temp->left), get_height(temp->right));
            
            return temp;
}
template <typename Key, typename Info>
int Dictionary<Key, Info>:: max_height(int a, int b) const
{
    if(a > b)
        return a;
    else
        return b;
    
}
template <typename Key, typename Info>
int Dictionary<Key, Info>:: get_height(Node *x) const
{
    if(x == nullptr)
        return 0;
    else
        return x->height;   
}
template <typename Key, typename Info>
typename Dictionary<Key, Info>::Node* Dictionary<Key, Info>:: minimal_value(Node *x) const
{
    Node *temp = x;
    while(temp->left != nullptr)
        temp = temp->left;
    return temp;
}
template <typename Key, typename Info>
typename Dictionary<Key, Info>::Node* Dictionary<Key, Info>::insert(Node* x, Key add_key, Info add_info)
{
    if(x == nullptr)
        x = new Node(add_key, add_info); 
    else if(x->key_of_node > add_key)
        x->left = insert(x->left, add_key, add_info);
    else if(x->key_of_node < add_key)
        x->right = insert(x->right, add_key, add_info);
    else
        return x;    
    x->height = 1 + max_height(get_height(x->left), get_height(x->right));
    int balance = get_height(x->left) - get_height(x->right);
    if(balance > 1)
        {
            if(x->left->key_of_node > add_key)
                x = rotate_right(x);
            else
                {
                    x->left = rotate_left(x->left);
                    x = rotate_right(x);
                }
        }
    else if(balance < -1) 
        {
            if(x->right->key_of_node < add_key)
                x = rotate_left(x);
            else
                {
                    x->right = rotate_right(x->right);
                    x = rotate_left(x);
                }
        }    
    return x;    
}
template <typename Key, typename Info>
typename Dictionary<Key, Info>::Node* Dictionary<Key, Info>::remove(Node* x, Key remove_key)
{
    if(x == nullptr)
        return nullptr;
    if(x->key_of_node > remove_key)
        x->left = remove(x->left, remove_key);
    else if(x->key_of_node < remove_key)
        x->right = remove(x->right, remove_key);
    else if(x->left != nullptr && x->right != nullptr)
        {
            Node *temp = minimal_value(x->right);
            x->key_of_node = temp->key_of_node;
            x->info_of_node = temp->info_of_node;
            x->right = remove(x->right, x->key_of_node);
        }
    else
        {
            Node *temp = x->left ? x->left : x->right;
            if(temp == nullptr)
            {
                temp = x;
                x = nullptr;
            }
            else
                *x = *temp;
            delete temp;
        }
    if(x == nullptr)
        return x;
    x->height = 1 + max_height(get_height(x->left), get_height(x->right));
    int balance = get_height(x->left) - get_height(x->right);
    if(balance > 1)
        {
            if(x->left->key_of_node > remove_key)
                x = rotate_right(x);
            else
                {
                    x->left = rotate_left(x->left);
                    x = rotate_right(x);
                }
        }
    else if(balance < -1) 
        {
            if(x->right->key_of_node < remove_key)
                x = rotate_left(x);
            else
                {
                    x->right = rotate_right(x->right);
                    x = rotate_left(x);
                }
        }    
    return x;    
}
template <typename Key, typename Info>
bool Dictionary<Key, Info>::remove_all()
{
    if(root == nullptr)
        return false;
    else
    {
        delete root;
        root = nullptr;
        return true;
    }
    
}
template <typename Key, typename Info>
void Dictionary<Key, Info>::print_tree(Node* x, int space) const
{
    if(x != nullptr)
        {
            space += COUNT;
            print_tree(x->right, space);
            cout << endl;
            for(int i = COUNT; i < space; i++)
                cout << " ";
            cout << x->key_of_node << "/" << x->info_of_node << "\n";
            print_tree(x->left, space);
        }
}
template <typename Key, typename Info>
void Dictionary<Key, Info>::print_postorder(Node* x) const
{
    if(x != nullptr)
        {
            print_postorder(x->left);
            print_postorder(x->right);
            cout << x->key_of_node << "/" << x->info_of_node << "  ";
        }
}
template <typename Key, typename Info>
void Dictionary<Key, Info>::print_preorder(Node* x) const
{
    if(x != nullptr)
        {
            cout << x->key_of_node << "/" << x->info_of_node << "  ";
            print_preorder(x->left);
            print_preorder(x->right);
        }
}
template <typename Key, typename Info>
void Dictionary<Key, Info>::print_inorder(Node* x) const
{
    if(x != nullptr)
        {
            print_inorder(x->left);
            cout << x->key_of_node << "/" << x->info_of_node << "  ";
            print_inorder(x->right);
        }
}
template <typename Key, typename Info>
void Dictionary<Key,Info>::print_tree() const
{
    print_tree(root, 0);
}
template <typename Key, typename Info>
void Dictionary<Key,Info>::print_inorder() const
{
    print_inorder(root);
}
template <typename Key, typename Info>
void Dictionary<Key,Info>::print_preorder() const
{
    print_preorder(root);
}
template <typename Key, typename Info>
void Dictionary<Key,Info>::print_postorder() const
{
    print_postorder(root);
}
template <typename Key, typename Info>
void Dictionary<Key,Info>::insert(Key add_key, Info add_info)
{
    root = insert(root, add_key, add_info);
}
template <typename Key, typename Info>
void Dictionary<Key,Info>::remove(Key remove_key)
{
    root = remove(root, remove_key);
}
template <typename Key, typename Info>
bool Dictionary<Key, Info>::is_empty() const
{
    if(root == nullptr)
        return true;
    else
        return false;
}
template <typename Key, typename Info>
bool Dictionary<Key, Info>::search(Node* x, Key search_key) const
{
    if(x == nullptr)
        return false;
    if(search_key == x->key_of_node)
        return true;
    bool res1 = search(x->left, search_key);
    if(res1 == true)
        return true;
    bool res2 = search(x->right, search_key);
    return res2;    
}
template <typename Key, typename Info>
typename Dictionary<Key, Info>::Node* Dictionary<Key, Info>::copy_node(Node* x)
{
    if( x != nullptr)
        {
            Node *left = copy_node(x->left);
            Node *right = copy_node(x->right);
            Node *add = new Node(x->key_of_node, x->info_of_node);
            add->height = x->height;
            add->left = left;
            add->right = right;
            return add;
        }
    else
        {
            return nullptr;
        }
}
template <typename Key, typename Info>
bool Dictionary<Key, Info>::compare_node(Node* x, Node *y) const
{
    if(x == nullptr || y == nullptr)
        return false;
    else    
        {
        if(x->key_of_node == y->key_of_node && x->info_of_node == y->info_of_node)
            return true;
        else
            return false;
        bool res1 = compare_node(x->left, y->left);
        if(res1 == false)
            return false;
        bool res2 = compare_node(x->right, y->right);
        return res2;
        }
}
#endif