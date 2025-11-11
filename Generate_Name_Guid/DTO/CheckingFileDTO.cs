using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generate_Name_Guid.DTO
{

    public class ResponseCheckingFileDTO
    {
        public bool IsExist { get; set; }
        public string? Message { get; set; }
    };
}
