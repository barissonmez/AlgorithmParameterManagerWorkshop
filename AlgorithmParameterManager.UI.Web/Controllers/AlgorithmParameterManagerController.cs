using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlgorithmParameterManager.DTO;
using AlgorithmParameterManager.ServiceManager;
using AlgorithmParameterManager.UI.Web.Models;

namespace AlgorithmParameterManager.UI.Web.Controllers
{
    public class AlgorithmParameterManagerController : Controller
    {
        private readonly AlgorithmParameterManagerService _serviceManager = null;

        public AlgorithmParameterManagerController()
        {
            _serviceManager = new AlgorithmParameterManagerService();
        }

        // GET: /AlgorithmParameterManager/

        public ActionResult Index()
        {
            var algorithmTypes =  Enum.GetNames(typeof (Enums.AlgorithmType)).ToDictionary(name => (int) Enum.Parse(typeof (Enums.AlgorithmType), name));

            return View(algorithmTypes);
        }

        public ActionResult ParameterManagement(int? type)
        {
            var viewModel = new ParameterManagementListViewModel();

            var algorithmParametersData = _serviceManager.GetAllAlgorithmParameters();

            if (algorithmParametersData.Success)
            {
                var algorithmParameters = ((List<AlgorithmParameterDTO>) algorithmParametersData.Data).Where(a=> (int)a.AlgorithmType == type);

                if (algorithmParameters.Any())
                {
                    foreach (var algorithmParameter in algorithmParameters)
                    {
                        viewModel.ParameterManagements.Add(new ParameterManagementViewModel
                        {
                            ID = algorithmParameter.ID,
                            Name = algorithmParameter.Name,
                            Description = algorithmParameter.Description,
                            Type = algorithmParameter.Type,
                            Value = algorithmParameter.Value,
                            AlgorithmType = algorithmParameter.AlgorithmType
                        });
                    }
                }
                else
                {
                    viewModel.Success = false;
                    viewModel.Messages.Add("There is no data.");
                }
                
            }
            else
            {
                viewModel.Success = false;
                viewModel.Messages = algorithmParametersData.Messages;
            }

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult Save(ParameterManagementRequestModel model)
        {
            var resultModel = new ResponseViewModel();

            var validationErrors = new List<string>();

            ValidateName(model.Name, ref validationErrors);
            ValidateValue(model.Value, model.Type, ref validationErrors);

            if (validationErrors.Count > 0)
            {
                resultModel.Success = false;
                resultModel.Messages = validationErrors;
            }
            else
            {
                var originalData = _serviceManager.GetAllAlgorithmParameterByID(model.ID);
                if (originalData.Success)
                {
                    var parameter = (AlgorithmParameterDTO)originalData.Data;

                    parameter.Name = model.Name;
                    parameter.Value = model.Value;

                    var result = _serviceManager.UpdateAlgorithmParameters(parameter, model.ID);

                    if (!result.Success)
                    {
                        resultModel.Success = false;
                        resultModel.Messages = result.Messages;
                    }
                }
                else
                {
                    resultModel.Success = false;
                    resultModel.Messages = originalData.Messages;
                }

            }

            return Json(resultModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new ResponseViewModel();

            var isDeleted= _serviceManager.DeleteAlgorithmParameterByID(id);

            if (!isDeleted.Success)
            {
                result.Success = false;
                result.Messages = isDeleted.Messages;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(ParameterManagementRequestModel model)
        {
            var result = new ResponseViewModel();

            var validationErrors= ValidateModel(model);

            if (validationErrors.Count > 0)
            {
                result.Success = false;
                result.Messages = validationErrors;
            }
            else
            {
                var parameterDTO = new AlgorithmParameterDTO
                {
                    Name = model.Name,
                    Description = model.Description,
                    Type = model.Type,
                    Value = model.Value,
                    AlgorithmType = model.AlgorithmType
                };

                var creationResult = _serviceManager.CreateAlgorithmParameters(parameterDTO);

                if (!creationResult.Success)
                {
                    result.Success = false;
                    result.Messages = creationResult.Messages;
                }

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<string> ValidateModel(ParameterManagementRequestModel model)
        {
            var validationErrors = new List<string>();

            ValidateName(model.Name, ref validationErrors);
            ValidateDescription(model.Description, ref validationErrors);
            ValidateType(model.Type, ref validationErrors);
            ValidateValue(model.Value, model.Type, ref validationErrors);
            ValidateAlgorithmType(model.AlgorithmType, ref validationErrors);

            return validationErrors;

        }

        private void ValidateAlgorithmType(Enums.AlgorithmType dataAlgorithmType, ref List<string> validationErrors)
        {
            Enums.AlgorithmType algorithmType;
            var isAlgorithmTypeParsed = Enum.TryParse(dataAlgorithmType.ToString(), out algorithmType);

            if (!isAlgorithmTypeParsed || !Enum.IsDefined(typeof(Enums.AlgorithmType), dataAlgorithmType))
                validationErrors.Add("Algorithm Type is invalid.");
        }


        private void ValidateValue(string dataValue, Enums.DataType dataType, ref List<string> validationErrors)
        {
            if (string.IsNullOrEmpty(dataValue))
                validationErrors.Add("Value field should not be null or empty.");
            else
            {
                if (dataType == Enums.DataType.Integer)
                {
                    int value;
                    var isValueInteger = int.TryParse(dataValue, out value);

                    if (!isValueInteger)
                        validationErrors.Add("Value field should be typed as Integer.");

                }
                else if (dataType == Enums.DataType.Decimal)
                {
                    decimal value;
                    var isValueDecimal = decimal.TryParse(dataValue, out value);

                    if (!isValueDecimal)
                        validationErrors.Add("Value field should be typed as Decimal.");
                }
                else if (dataType == Enums.DataType.Bool)
                {
                    bool value;
                    var isValueBoolean = bool.TryParse(dataValue, out value);

                    if (!isValueBoolean)
                        validationErrors.Add("Value field should be typed as Boolean.");
                }
                else if (dataType == Enums.DataType.DateTime)
                {
                    string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt", 
                                       "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss", 
                                       "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt", 
                                       "M/d/yyyy h:mm", "M/d/yyyy h:mm", 
                                       "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};

                    DateTime value;
                    var isValueDateTime = DateTime.TryParseExact(dataValue, formats, new CultureInfo("en-US"),
                                                                 DateTimeStyles.None, out value);

                    if (!isValueDateTime)
                        validationErrors.Add("Value field should be typed as DateTime.");
                }

            }
        }

        private void ValidateType(Enums.DataType dataType, ref List<string> validationErrors)
        {
            Enums.DataType type;
            var isTypeParsed = Enum.TryParse(dataType.ToString(), out type);

            if (!isTypeParsed || !Enum.IsDefined(typeof(Enums.DataType), dataType))
                validationErrors.Add("Type is invalid.");
        }

        private void ValidateDescription(string description, ref List<string> validationErrors)
        {
            if (string.IsNullOrEmpty(description))
                validationErrors.Add("Description field should not be null or empty.");

        }

        private void ValidateName(string name, ref List<string> validationErrors)
        {
            if (string.IsNullOrEmpty(name))
                validationErrors.Add("Name field should not be null or empty.");
            else if (name.Length > 50)
                validationErrors.Add("Name field should not be longer than 50 characters.");
        }
    }
}
