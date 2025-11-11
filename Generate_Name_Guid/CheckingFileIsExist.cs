using Generate_Name_Guid.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generate_Name_Guid
{
    public static class CheckingFileIsExist
    {
        public static ResponseCheckingFileDTO FileIsExist(List<string>? request)
        {
            var result = new ResponseCheckingFileDTO
            {
                IsExist = true,
                Message = String.Empty
            };

            if (request == null || request.Count == 0)
            {
                result.IsExist = false;
                result.Message = "Path File tidak boleh kosong!";
                return result;
            }

            List<string> msg = new List<string>();

            foreach(var v in request)
            {
                var fi = new FileInfo(v);
                if (!fi.Exists)
                {
                    msg.Add($"{v} not found");
                }
            }

            if(msg.Count > 0)
            {
                result.IsExist = false;
                result.Message = string.Join(", ", msg);
            }

            return result;
        }
    }
}
