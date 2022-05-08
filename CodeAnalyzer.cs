using System.Text.RegularExpressions;

namespace InheritanceTree
{
    public class CodeAnalyzer
    {
        private string _fileName;
        private List<CppClass> _allClasses = new List<CppClass>();
        public CodeAnalyzer(string fileName)
        {
            _fileName = fileName;
        }
        public bool ParseFile()
        {
            try
            {
                if (!File.Exists(_fileName))
                {
                    Console.WriteLine("File does not exists!");
                    return false;
                }
                using (StreamReader sr = new StreamReader(_fileName))
                {
                    while (sr.Peek() >= 0)
                    {
                        string? currentLine = sr.ReadLine();
                        if (currentLine != null && _HasClassSyntax(currentLine))
                            _ParseClass(currentLine);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return false;
            }

        }
        private bool _HasClassSyntax(string lineToCheck)
        {
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
                        parentClass = new CppClass(parentName);
                        newClass = new CppClass(className);
                        newClass.AddParent(parentClass);
                        parentClass.AddChild(newClass);
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
                        _FindClass(className)!.AddParent(_FindClass(parentName)!);
                        _FindClass(parentName)!.AddChild(_FindClass(className)!);
                    }
                }
            }
        }
        private bool _CheckIfClassExists(string className)
        {
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
            foreach (var cppClass in _allClasses)
                if (cppClass._className == className)
                    return cppClass;
            return null;
        }
        public void printTree()
        {
            Console.WriteLine(_CheckNumberOfRoots());
        }
        private int _CheckNumberOfRoots()
        {
            int counter = 0;
            foreach (var cppClass in _allClasses)
            {
                if (cppClass.Parents.Count() == 0)
                    counter++;
            }
            return counter;
        }
    }
}