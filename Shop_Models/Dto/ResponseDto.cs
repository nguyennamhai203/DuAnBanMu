using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto
{
    public class ResponseDto
    {
        public object? Content { get; set; }
        public bool IsSuccess { get; set; } = true;
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "Thành công";
        public int Count { get; set; } = 0;
        public int TotalPage { get; set; } = 0;
    }
}
