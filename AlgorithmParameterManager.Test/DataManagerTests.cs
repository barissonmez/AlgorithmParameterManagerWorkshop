using System;
using System.Linq;
using AlgorithmParameterManager.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmParameterManager.DataManager;
using System.Collections.Generic;

namespace AlgorithmParameterManager.Test
{
    [TestClass]
    public class DataManagerTests
    {
        private readonly DataManager.DataManager _dataManager = null;

        public DataManagerTests()
        {
            _dataManager = new DataManager.DataManager();
        }

        [TestMethod]
        public void Should_Create_Algorithm_Parameter()
        {
            var algorithmParameter = new AlgorithmParameter
                {
                    Name = "Test Parameter 1",
                    Description = "Test Description",
                    Type = 1,
                    Value = "Test Value",
                    AlgorithmType = 2
                };

            

            var result = _dataManager.CreateAlgorithmParameter(algorithmParameter);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Should_Update_Algorithm_Parameter()
        {
            var parameter = _dataManager.GetAllAlgorithmParameters().FirstOrDefault();

            parameter.Name = "Test Parameter 1-Updated";
            parameter.Description = "Test Description-Updated";
            parameter.Type = 0;
            parameter.Value = "Test Value-Updated";
            parameter.AlgorithmType = 1;

            var result = _dataManager.UpdateAlgorithmParameter(parameter, parameter.ID);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Should_Get_All_Algorithm_Parameters()
        {

            var result = _dataManager.GetAllAlgorithmParameters();

            Assert.IsTrue(result.Count > 0);

        }

        [TestMethod]
        public void Should_Return_Proper_Algorithm_Parameter_By_ID()
        {
            var parameter = _dataManager.GetAllAlgorithmParameters().FirstOrDefault();

            var result = _dataManager.GetAlgorithmParameterByID(parameter.ID);

            Assert.IsTrue(result.ID == parameter.ID);

        }

        [TestMethod]
        public void Should_Delete_Proper_Algorithm_Parameter_By_ID()
        {

            var algorithmParameter = new AlgorithmParameter
            {
                Name = "Test Parameter",
                Description = "Test Description",
                Type = 0,
                Value = "Test Value",
                AlgorithmType = 1
            };

            _dataManager.CreateAlgorithmParameter(algorithmParameter);

            var parameter = _dataManager.GetAllAlgorithmParameters().FirstOrDefault();

            var result = _dataManager.DeleteAlgorithmParameter(parameter.ID);

            Assert.IsTrue(result);

        }
    }
}
