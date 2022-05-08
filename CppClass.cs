namespace InheritanceTree
{
    public class CppClass
    {
        public string _className { get; private set; }
        public CppClass? _parent { get; private set; }
        private List<CppClass> _children = new List<CppClass>();
        public void AddChild(CppClass child)
        {
            _children.Add(child);
        }
        public IList<CppClass> Children { get { return _children; } }
        public CppClass(string className, CppClass? parent) 
        {
            _className = className;
            _parent = parent;
        }
    }
}