namespace Generate_Name_Guid.DTO
{
    public class RequestGenerateFileDTO
    {
        public string? PrefixFolder { get; set; }
        public List<string>? PathFile { get; set; }
        public string? DestinationPathFile { get; set; }
        public string? DestinatonJsonFile { get; set; }
        public int Total { get; set; }
    }
    public class ResponseGenerateFileDTO
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }

    public class FilePath
    {
        public string? OriginalName { get; set; }
        public string? Filename { get; set; }
    }
}
