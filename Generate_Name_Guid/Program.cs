using Generate_Name_Guid;
using Generate_Name_Guid.DTO;


var pathOutput = "C:\\Vendra\\OutputFIle\\File";
var jsonOutput = "C:\\Vendra\\OutputFIle\\JSON";

args = ["12", "PMT", "C:\\Vendra\\Support\\ASOKA GRAHA SURYA (Batch 1).pdf"];

if (args.Length != 3)
{
    Console.WriteLine("Usage: Generate_Name_Guid <Count> <PrefixFolder> <SourceFile>");
    Console.WriteLine("@Example: dotnet run -- 2 PMT \"D:\\File\\Source.pdf\" ");
    return;
}

if (!int.TryParse(args[0], out var count) || count <= 0)
{
    Console.Error.WriteLine("Error: Count must be a positive integer.");
    return;
}

var prefixFolder = args[1];
var pathFile = args.Skip(2).ToList();

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



