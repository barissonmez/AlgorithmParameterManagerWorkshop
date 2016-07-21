using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlgorithmParameterManager.DTO;

namespace AlgorithmParameterManager.UI.Web.Models
{
    public class ParameterManagementListViewModel : ResponseViewModel
    {
        public ParameterManagementListViewModel()
        {
            ParameterManagements = new List<ParameterManagementViewModel>();
        }

        public List<ParameterManagementViewModel> ParameterManagements { get; set; }
    }
}