using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmParameterManager.DTO;
using AlgorithmParameterManager.Entity;

namespace AlgorithmParameterManager.ServiceManager
{
    public class AlgorithmParameterManagerService
    {
        private readonly DataManager.DataManager _dataManager = null;

        public AlgorithmParameterManagerService()
        {
            _dataManager = new DataManager.DataManager();
        }

        public ResultDTO GetAllAlgorithmParameterByID(int ID)
        {
            var result = new ResultDTO();

            var algorithmParameter = _dataManager.GetAlgorithmParameterByID(ID);

            if (algorithmParameter == null)
            {
                result.Success = false;
                result.Messages.Add("Cannot find expected record.");
            }
            else
            {
                var parameter = new AlgorithmParameterDTO
                    {
                        ID = algorithmParameter.ID,
                        Name = algorithmParameter.Name,
                        Description = algorithmParameter.Description,
                        Type = (Enums.DataType) algorithmParameter.Type,
                        Value = algorithmParameter.Value,
                        AlgorithmType = (Enums.AlgorithmType) algorithmParameter.AlgorithmType
                    };

                result.Data = parameter;
            }
            

            return result;
        }

        public ResultDTO GetAllAlgorithmParameters()
        {
            var result = new ResultDTO();

            var dataResult = _dataManager.GetAllAlgorithmParameters();

            if (dataResult.Count > 0)
            {

                var parameters = dataResult.Select(algorithmParameter => new AlgorithmParameterDTO
                    {
                        ID = algorithmParameter.ID,
                        Name = algorithmParameter.Name,
                        Description = algorithmParameter.Description,
                        Type = (Enums.DataType) algorithmParameter.Type,
                        Value = algorithmParameter.Value,
                        AlgorithmType = (Enums.AlgorithmType) algorithmParameter.AlgorithmType
                    }).ToList();

                result.Data = parameters;
            }
            else
            {
                result.Success = false;
                result.Messages.Add("There is no data.");
            }

            return result;

        }

        public ResultDTO CreateAlgorithmParameters(AlgorithmParameterDTO parameterDTO)
        {
            var result = new ResultDTO();

            var parameterEntity = new AlgorithmParameter
                {
                    ID = parameterDTO.ID,
                    Name = parameterDTO.Name,
                    Description = parameterDTO.Description,
                    Type = (int)parameterDTO.Type,
                    Value = parameterDTO.Value,
                    AlgorithmType = (int)parameterDTO.AlgorithmType
                };

            var isCreated = _dataManager.CreateAlgorithmParameter(parameterEntity);

            if (isCreated)
            {
                result.Messages.Add("Algorithm Parameter has been created successfuly.");
            }
            else
            {
                result.Success = false;
                result.Messages.Add("An unexpected problem has been occured. Please try again.");
            }

            return result;
        }

        public ResultDTO UpdateAlgorithmParameters(AlgorithmParameterDTO parameterDTO, int ID)
        {
            var result = new ResultDTO();

            var existingParameter = _dataManager.GetAlgorithmParameterByID(ID);

            if (existingParameter != null)
            {
                var parameterEntity = new AlgorithmParameter
                {
                    Name = parameterDTO.Name,
                    Description = parameterDTO.Description,
                    Type = (int)parameterDTO.Type,
                    Value = parameterDTO.Value,
                    AlgorithmType = (int)parameterDTO.AlgorithmType
                };

                var isUpdated = _dataManager.UpdateAlgorithmParameter(parameterEntity, ID);

                if (isUpdated)
                {
                    result.Messages.Add("Algorithm Parameter has been updated successfuly.");
                }
                else
                {
                    result.Success = false;
                    result.Messages.Add("An unexpected problem has been occured. Please try again.");
                }

            }
            else
            {
                result.Success = false;
                result.Messages.Add("Cannot find expected record.");
            }

            return result;
        }

        public ResultDTO DeleteAlgorithmParameterByID(int ID)
        {
            var result = new ResultDTO();

            var existingParameter = _dataManager.GetAlgorithmParameterByID(ID);

            if (existingParameter != null)
            {

                var isDeleted = _dataManager.DeleteAlgorithmParameter(ID);

                if (isDeleted)
                {
                    result.Messages.Add("Algorithm Parameter has been deleted successfuly.");
                }
                else
                {
                    result.Success = false;
                    result.Messages.Add("An unexpected problem has been occured. Please try again.");
                }
            }
            else
            {
                result.Success = false;
                result.Messages.Add("Cannot find expected record.");
            }

            return result;
        }
    }
}
