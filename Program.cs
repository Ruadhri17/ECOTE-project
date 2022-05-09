using InheritanceTree;

String? fileName;

// UI asking for file name
while(true) 
{
    Console.Write("Enter file name or file directory: ");
    fileName = Console.ReadLine();
    if(fileName != null && fileName.Contains(".cpp")) 
        break;
    else
        Console.WriteLine("Input is empty or does not contain .cpp file!");
}

Console.Clear();

CodeAnalyzer codeAnalyzer = new CodeAnalyzer(fileName);

// leave the program if file parsing failed
if (!codeAnalyzer.ParseFile())
    return;
// printing tree
codeAnalyzer.CreateTree();