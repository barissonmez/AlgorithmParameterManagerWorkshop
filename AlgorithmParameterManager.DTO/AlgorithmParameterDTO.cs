using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmParameterManager.DTO
{
    public class AlgorithmParameterDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Enums.DataType Type { get; set; }
        public string Value { get; set; }
        public Enums.AlgorithmType AlgorithmType { get; set; }
    }
}
