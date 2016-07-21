using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmParameterManager.DTO
{
    public class ResultDTO
    {
        public ResultDTO()
        {
            Success = true;
            Messages = new List<string>();
        }

        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public object Data { get; set; }
    }
}
