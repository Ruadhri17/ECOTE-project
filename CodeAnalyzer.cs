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
                foreach (var cppClass in _allClasses)
                    Console.WriteLine(cppClass._className);
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
            // split line, remove additional whitespaces and replace symbols ("{", "," and ";") that are useless in analizys.
            string[] keywords = lineToParse.Replace("{", "").Replace(";", "").Replace(",","").Trim().Split(' ');
            string className = keywords[1];
            
            if (keywords.Count() == 2)
                if (!_CheckIfClassExists(className))
                {
                    _allClasses.Add(new CppClass(className, null));
                    return;
                }
            
            string parentName = keywords[^1];
            CppClass? parentClass = null;
            CppClass? newClass = null;
            if (keywords.Count() > 6)

            if (!_CheckIfClassExists(className))
                if (!_CheckIfClassExists(parentName))
                {
                    parentClass = new CppClass(parentName, null);
                    newClass = new CppClass(className, parentClass);
                    parentClass.AddChild(newClass);
                    _allClasses.Add(parentClass);
                    _allClasses.Add(newClass);
                }
                else
                {                
                    newClass = new CppClass(className, _FindClass(parentName));
                    _FindClass(parentName)!.AddChild(newClass);
                    _allClasses.Add(newClass);
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
    }
}