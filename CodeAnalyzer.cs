using System.Text.RegularExpressions;

namespace InheritanceTree
{
    public class CodeAnalyzer
    {
        private string _fileName;
        //container for all classes in program
        private List<CppClass> _allClasses = new List<CppClass>();
        public CodeAnalyzer(string fileName)
        {
            _fileName = fileName;
        }
        public bool ParseFile()
        {
            // open and read file using streamreader
            try
            {
                using (StreamReader sr = new StreamReader(_fileName))
                {
                    // read line by line
                    while (sr.Peek() >= 0)
                    {
                        string? currentLine = sr.ReadLine();
                        if (currentLine != null && _HasClassSyntax(currentLine))
                            _ParseClass(currentLine);
                        if (!_ValidateClasses())
                        {
                            Console.WriteLine("Error: {0} file is not compilable!", _fileName);
                            return false;
                        }         
                    }
                }
                return true;
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine(String.Format("Error: {0} file not found!", _fileName));
                return false;
            }
            catch (Exception)
            {
                Console.Error.WriteLine(String.Format("Error: {0} file could not be processed!", _fileName));
                return false;
            }

        }
        private bool _HasClassSyntax(string lineToCheck)
        {
            // find all classes using regex
            Regex classSyntax = new Regex(@"\s*class\s*(\w+)\s*\:?\s*(public|private|protected)?(\s*virtual)?\s*(\w+)", RegexOptions.IgnoreCase);
            if (classSyntax.IsMatch(lineToCheck))
                return true;
            return false;
        }
        private void _ParseClass(string lineToParse)
        {
            // split line, remove additional sybmols and words, leave only class names
            List<string> keywords = lineToParse
                                .Replace("{", "")
                                .Replace("}", "")
                                .Replace(";", "")
                                .Replace(",", "")
                                .Replace(":", "")
                                .Replace("class", "")
                                .Replace("virtual", "")
                                .Replace("public", "")
                                .Replace("protected", "")
                                .Replace("private", "")
                                .Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            string className = keywords.First();
            keywords.RemoveAt(0);
            
            // Case without inheritance
            if (keywords.Count() == 0)
                if (!_CheckIfClassExists(className))
                {
                    _allClasses.Add(new CppClass(className));
                    return;
                }
            List<string> parentsNames = keywords;
            CppClass? parentClass = null;
            CppClass? newClass = null;
            
            // Case with inheritnance and multiple inheritance
            foreach (var parentName in parentsNames)
            {
                parentClass = null;
                newClass = null;
                if(!_CheckIfClassExists(className))
                {
                    if (!_CheckIfClassExists(parentName))
                    {
                        // Both child and parent do not exist
                        // Create new instances of class
                        parentClass = new CppClass(parentName);
                        newClass = new CppClass(className);
                        // fill information about parents and children
                        newClass.AddParent(parentClass);
                        parentClass.AddChild(newClass);
                        // add both classes to class container
                        _allClasses.Add(parentClass);
                        _allClasses.Add(newClass);
                    }
                    else
                    {
                        // Child does not exists but parent does
                        newClass = new CppClass(className);
                        
                        newClass.AddParent(_FindClass(parentName)!);
                        _FindClass(parentName)!.AddChild(newClass);
                        
                        _allClasses.Add(newClass);
                    }
                }
                else
                {
                    if (!_CheckIfClassExists(parentName))
                    {
                        // Child exists but parent does not
                        parentClass = new CppClass(parentName);
                        
                        _FindClass(className)!.AddParent(parentClass);
                        parentClass.AddChild(_FindClass(className)!);
                        
                        _allClasses.Add(parentClass);
                    }
                    else
                    {
                        // Both child and parent exist 
                        _FindClass(className)!.AddParent(_FindClass(parentName)!);
                        _FindClass(parentName)!.AddChild(_FindClass(className)!);
                    }
                }
            }
        }
        private bool _CheckIfClassExists(string className)
        {
            // check if class with given name was already created
            if (_allClasses.Count == 0)
                return false;
            foreach (var cppClass in _allClasses)
            {
                if (cppClass._className == className)
                    return true;
            }
            return false;
        }
        private CppClass? _FindClass(string className)
        {
            // return class with given name
            foreach (var cppClass in _allClasses)
                if (cppClass._className == className)
                    return cppClass;
            return null;
        }
        private bool _ValidateClasses() 
        {
            foreach (var cppClass in _allClasses)
            {
                if (!_TraverseThroughChildren(cppClass._children, cppClass._className))
                    return false;
            }
            return true;
        }
        private bool _TraverseThroughChildren(List<CppClass> children, string className)
        {
            if (children.Count == 0)
                return true;
            foreach (var child in children)
            {
                if (child._className == className)
                    return false;
                if (!_TraverseThroughChildren(child._children, className))
                    return false;
            }
            return true;
        }
        public void CreateTree()
        {
            // file without classes
            if (_allClasses.Count() == 0)
            {
                Console.WriteLine("Given file has no classes!");
                return;
            }
            
            if (_FindClassesWithNoParent().Count() == 1)
                // case when there is only one class that has no parents
                _PrintTree(_FindClassesWithNoParent().First(), "", true);
            else
            {
                // case when there are multiple classes without parents
                CppClass dummyClass = new CppClass("Root");
                dummyClass.AddChildren(_FindClassesWithNoParent());
                _PrintTree(dummyClass, "", true);
            }
        }
        private List<CppClass> _FindClassesWithNoParent()
        {
            // function looks for classes that does not inherit from other classes
            List<CppClass> noParents = new List<CppClass>();
            foreach (var cppClass in _allClasses)
            {
                if (cppClass.Parents.Count() == 0)
                    noParents.Add(cppClass);
            }
            return noParents;
        }
        private static void _PrintTree(CppClass node, string indent, bool last)
        {
            // console representation of tree (created recursively)
            Console.WriteLine(indent + "+- " + node._className);
            indent += last ? "   " : "|  ";
            for (int i = 0; i < node.Children.Count; i++)
                _PrintTree(node.Children[i], indent, i == node.Children.Count - 1);
        }
    }
}