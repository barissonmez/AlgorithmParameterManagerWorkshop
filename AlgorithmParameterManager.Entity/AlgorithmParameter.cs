using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmParameterManager.Entity
{
    public class AlgorithmParameter
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public int AlgorithmType { get; set; }
    }
}
