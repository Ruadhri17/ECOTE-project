using InheritanceTree;

String? fileName;

while(true) 
{
    Console.Write("Enter file name: ");
    fileName = Console.ReadLine();
    if(fileName != null && fileName.Contains(".cpp")) 
        break;
    else
        Console.WriteLine("Input is empty or does not contain .cpp file!");
}
Console.Clear();

CodeAnalyzer codeAnalyzer = new CodeAnalyzer(fileName);

if (!codeAnalyzer.ParseFile())
    return;
codeAnalyzer.printTree();