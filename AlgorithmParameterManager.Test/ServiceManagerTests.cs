using System;
using System.Linq;
using AlgorithmParameterManager.DTO;
using AlgorithmParameterManager.Entity;
using AlgorithmParameterManager.ServiceManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmParameterManager.DataManager;
using System.Collections.Generic;

namespace AlgorithmParameterManager.Test
{
    [TestClass]
    public class ServiceManagerTests
    {
        private readonly AlgorithmParameterManagerService _serviceManager = null;
        private readonly DataManager.DataManager _dataManager = null;

        public ServiceManagerTests()
        {
            _serviceManager = new AlgorithmParameterManagerService();
            _dataManager = new DataManager.DataManager();
        }

        [TestMethod]
        public void Service_Should_Get_All_Algorithm_Parameters()
        {
            var result = _serviceManager.GetAllAlgorithmParameters();

            var parameters = (List<AlgorithmParameterDTO>)result.Data;

            Assert.IsTrue(result.Success && parameters.Count > 0);

        }

        [TestMethod]
        public void Service_Should_Get_Algorithm_Parameter_By_ID()
        {
            var parameterData = _dataManager.GetAllAlgorithmParameters().FirstOrDefault();

            var result = _serviceManager.GetAllAlgorithmParameterByID(parameterData.ID);

            var parameter = (AlgorithmParameterDTO)result.Data;

            Assert.IsTrue(result.Success && parameter != null);

        }

        [TestMethod]
        public void Service_Should_Create_Algorithm_Parameter()
        {
            var algorithmParameterDTO = new AlgorithmParameterDTO
            {
                Name = "Test Parameter 11",
                Description = "Test Description 11",
                Type = Enums.DataType.Integer,
                Value = "Test Value 11",
                AlgorithmType = Enums.AlgorithmType.MD
            };

            var result = _serviceManager.CreateAlgorithmParameters(algorithmParameterDTO);

            Assert.IsTrue(result.Success);

        }

        [TestMethod]
        public void Service_Should_Update_Algorithm_Parameter()
        {
            var parameterData = _dataManager.GetAllAlgorithmParameters().FirstOrDefault();

            var algorithmParameterDTO = new AlgorithmParameterDTO
            {
                Name = parameterData.Name + "---Updated",
                Description = parameterData.Description + "---Updated",
                Type = Enums.DataType.Bool,
                Value = parameterData.Value + "---Updated",
                AlgorithmType = Enums.AlgorithmType.RPL
            };

            var result = _serviceManager.UpdateAlgorithmParameters(algorithmParameterDTO, parameterData.ID);

            Assert.IsTrue(result.Success);

        }

        [TestMethod]
        public void Service_Should_Delete_Algorithm_Parameter()
        {
            var parameterData = _dataManager.GetAllAlgorithmParameters().FirstOrDefault();

            var result = _serviceManager.DeleteAlgorithmParameterByID(parameterData.ID);

            Assert.IsTrue(result.Success);
        }

    }
}
