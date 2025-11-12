using Generate_Name_Guid;
using Generate_Name_Guid.DTO;


var pathOutput = "C:\\Vendra\\OutputFIle\\File";
var jsonOutput = "C:\\Vendra\\OutputFIle\\JSON";
int count = 0;
List<string> pathFile = new List<string>();
string prefixFolder = string.Empty;

Console.WriteLine("Masukkan Jumlah Total Generate:");
if(!int.TryParse(Console.ReadLine(), out count))
{
    count = 0;
}

if(count == 0)
{
    Console.Error.WriteLine("Error: Count must be a positive integer.");
    return;
}

Console.WriteLine("Masukkan Prefix Folder:");
prefixFolder = Console.ReadLine() ?? string.Empty;

if(string.IsNullOrWhiteSpace(prefixFolder))
{
    Console.Error.WriteLine("Error: Prefix Folder cannot be empty.");
    return;
}

Console.WriteLine("Masukkan PathFile, lebih dari satu pisah dengan koma: Ex: \"C:\\Vendra\\Support\\ASOKA GRAHA SURYA (Batch 1).pdf\"");
pathOutput = Console.ReadLine() ?? string.Empty;

if (string.IsNullOrWhiteSpace(pathOutput))
{
    Console.Error.WriteLine("Error: PathFile cannot be empty.");
    return;
}

pathFile = pathOutput.Split(",").ToList();

var respons = CheckingFileIsExist.FileIsExist(pathFile);
if (!respons.IsExist)
{
    Console.Error.WriteLine($"Error: {respons.Message}");
    return;
}

var requestGenerate = new RequestGenerateFileDTO
{
    PathFile = pathFile,
    PrefixFolder = prefixFolder,
    DestinationPathFile = pathOutput,
    DestinatonJsonFile = jsonOutput,
    Total = count
};

var resultGenerate = GenerateFile.ProsesGenerate(requestGenerate);
if (!resultGenerate.IsSuccess)
{
    Console.Error.WriteLine($"Error: {resultGenerate.Message}");
    return;
}

Console.WriteLine($"Berhasil Dibuat total file: {pathFile.Count} Sebanyak per file: {count}");
return;



