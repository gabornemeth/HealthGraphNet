using NUnit.Framework;
using System.Threading.Tasks;

namespace HealthGraphNet.Tests.Integration
{
    [TestFixture()]
    public class UsersEndpointTest : ClientSetup
    {        
        #region Tests

        [Test]
        public async Task GetUser_NotOptionalProperiesPresent()
        {
            //Arrange
            var userRequest = new UsersEndpoint(Client);
            //Act
            var user = await userRequest.GetUser();
            //Assert
            Assert.IsTrue(user.UserID != default(int));
            Assert.IsTrue(!string.IsNullOrEmpty(user.Profile));
            Assert.IsTrue(!string.IsNullOrEmpty(user.Settings));
            Assert.IsTrue(!string.IsNullOrEmpty(user.FitnessActivities));
            Assert.IsTrue(!string.IsNullOrEmpty(user.StrengthTrainingActivities));
            Assert.IsTrue(!string.IsNullOrEmpty(user.BackgroundActivities));
            Assert.IsTrue(!string.IsNullOrEmpty(user.Sleep));
            Assert.IsTrue(!string.IsNullOrEmpty(user.Nutrition));
            Assert.IsTrue(!string.IsNullOrEmpty(user.Weight));
            Assert.IsTrue(!string.IsNullOrEmpty(user.GeneralMeasurements));
            Assert.IsTrue(!string.IsNullOrEmpty(user.Diabetes));
            Assert.IsTrue(!string.IsNullOrEmpty(user.Records));
            Assert.IsTrue(!string.IsNullOrEmpty(user.Team));        
        }

        #endregion
    }
}