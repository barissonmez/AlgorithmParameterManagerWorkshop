using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AlgorithmParameterManager.DTO;

namespace AlgorithmParameterManager.UI.Web.Models
{
    public class ParameterManagementRequestModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Enums.DataType Type { get; set; }
        public string Value { get; set; }
        public Enums.AlgorithmType AlgorithmType { get; set; }
    }
}