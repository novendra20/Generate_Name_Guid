using Generate_Name_Guid.DTO;
using System.Text.Json;
using OfficeOpenXml;
using System.ComponentModel;

namespace Generate_Name_Guid
{
    public static class GenerateFile
    {
        public static ResponseGenerateFileDTO ProsesGenerate(RequestGenerateFileDTO request)
        {
            var result = new ResponseGenerateFileDTO
            {
                IsSuccess = true,
                Message = String.Empty
            };

            List<string> ListFile = new List<string>();

            if (string.IsNullOrWhiteSpace(request.DestinationPathFile) || string.IsNullOrWhiteSpace(request.DestinatonJsonFile))
            {
                var missing = new List<string>();
                if (string.IsNullOrWhiteSpace(request.DestinationPathFile))
                {
                    missing.Add("Destination Path File Belum di set");
                }
                if (string.IsNullOrWhiteSpace(request.DestinatonJsonFile))
                {
                    missing.Add("Destinaton Json File Belum di set");
                }

                result.IsSuccess = false;
                result.Message = string.Join(", ", missing);
                return result;
            }

            if (!Directory.Exists(request.DestinationPathFile))
            {
                Directory.CreateDirectory(request.DestinationPathFile);
            }

            if (!Directory.Exists(request.DestinatonJsonFile))
            {
                Directory.CreateDirectory(request.DestinatonJsonFile);
            }

            var destinatonPathFile = Path.GetFullPath(request.DestinationPathFile);
            var destinationJsonFile = Path.GetFullPath(request.DestinatonJsonFile);
            var pathCombinePathFile = string.Empty;
            var pathCombineJsonFile = string.Empty;

            var date =  DateTime.Today.Date.ToString("yyyyMMdd");
            int i = 1, j = 1;
            while (true)
            {
                pathCombinePathFile = Path.Combine(destinatonPathFile, date, i.ToString());
                if (!Directory.Exists(pathCombinePathFile))
                {
                    Directory.CreateDirectory(pathCombinePathFile);
                    break;
                }
                i++;
            }

            while (true)
            {
                pathCombineJsonFile = Path.Combine(destinationJsonFile, date, j.ToString());
                if (!Directory.Exists(pathCombineJsonFile))
                {
                    Directory.CreateDirectory(pathCombineJsonFile);
                    break;
                }
                j++;
            }

            foreach (var v in request.PathFile)
            {
                var originalName = Path.GetFileName(v);
                var extension = Path.GetExtension(v);

                for(int z = 0; z < request.Total; z++)
                {
                    List<FilePath> tempFile = new List<FilePath>();
                    var guidName = Guid.NewGuid().ToString("N");
                    var newFileName = $"{guidName}{extension}";
                    var pathPrefix = $"{request.PrefixFolder}/{newFileName}";
                    var pathCombineDestination = Path.Combine(pathCombinePathFile, newFileName);
                    var jsonFile = new FilePath
                    {
                        OriginalName = originalName,
                        Filename = pathPrefix
                    };
                    File.Copy(v, pathCombineDestination, false);
                    tempFile.Add(jsonFile);

                    var jsonResult = JsonSerializer.Serialize(tempFile, new JsonSerializerOptions
                    {
                        WriteIndented = false
                    });

                    ListFile.Add(jsonResult);

                }
            }

            int name = 1;
            var pathCombineJsonFileFinal = string.Empty;
            while (true)
            {
                var listJsonName = $"Generate_{date}_{i}_{name}.xlsx";
                pathCombineJsonFileFinal = Path.Combine(pathCombineJsonFile, listJsonName);
                var fi = new FileInfo(pathCombineJsonFileFinal);
                if (!fi.Exists)
                {
                    break;
                }
                name++;
            }

            ExcelPackage.License.SetNonCommercialPersonal("Novendra");

            using (var excel = new ExcelPackage())
            {
                var ws = excel.Workbook.Worksheets.Add("GeneratedFiles");
                ws.Cells[1, 1].Value = "FilePaths";

                int row = 2;
                foreach (var filePath in ListFile)
                {
                    ws.Cells[row, 1].Value = filePath;
                    row++;
                }

                if (ws.Dimension != null)
                {
                    ws.Cells[ws.Dimension.Address].AutoFitColumns();
                }

                var fileExcel = new FileInfo(pathCombineJsonFileFinal);
                excel.SaveAs(fileExcel);
            }

            return result;
        }
    }
}
