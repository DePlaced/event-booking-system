using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Dapper;
using SignData.DatabaseLayer;
using SignData.ModelLayer;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AccessTest
{
    [TestClass]
    public class SignAccessTests
    {
        [TestMethod]
        public void GetSignById_ExistingId_ReturnsSign()
        {
            // Arrange
            string cString = "Data Source=localhost;Initial Catalog=SignRental;Persist Security Info=True;User ID=sa;Password=VeryStr0ngP@ssw0rd";
            ISignAccess signAccess = new SignAccess(cString);
            int signId = 1;

            // Act
            var result = signAccess.GetSignById(signId);

            // Assert
            Assert.IsNotNull(result);

        }
    }
}
