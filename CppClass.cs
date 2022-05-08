namespace InheritanceTree
{
    public class CppClass
    {
        public string _className { get; private set; }
        private List<CppClass> _parents = new List<CppClass>();
        private List<CppClass> _children = new List<CppClass>();
        public void AddChild(CppClass child)
        {
            _children.Add(child);
        }
        public void AddChildren(List<CppClass> children)
        {
            _children.AddRange(children);
        }
        public void AddParent(CppClass parent)
        {
            _parents.Add(parent);
        }
        public IList<CppClass> Children { get { return _children; } }
        public IList<CppClass> Parents { get { return _parents; } }
        public CppClass(string className) 
        {
            _className = className;
        }
    }
}