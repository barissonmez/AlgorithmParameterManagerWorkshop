using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgorithmParameterManager.UI.Web.Models
{
    public class ResponseViewModel
    {
        public ResponseViewModel()
        {
            Success = true;
            Messages = new List<string>();
        }

        public bool Success { get; set; }
        public List<string> Messages { get; set; }

    }
}