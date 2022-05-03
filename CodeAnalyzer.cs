using System.Text.RegularExpressions;

namespace InheritanceTree
{
    public class CodeAnalyzer
    {
        private String _fileName;
        
        public CodeAnalyzer(String fileName) 
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
                        String? currentLine = sr.ReadLine();
                        Console.WriteLine("siema");
                        if (currentLine != null && HasClassSyntax(currentLine))
                            Console.WriteLine(currentLine);
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
        private bool HasClassSyntax(String lineToCheck) 
        {
            Regex classSyntax = new Regex(@"\s+class\s*(\w+)\s*:?\s*(public|private|protected)?\s*(\w+)");
            if (classSyntax.IsMatch(lineToCheck))
                return true;
            return false; 
        }
    }
}