using System.Threading.Tasks;
using NgTemplate.API.DTOs.Enums;
using NgTemplate.API.Repositories;
using NgTemplate.API.Services;
using NSubstitute;
using Xunit;

namespace NgTemplate.UnitTests
{
    public class DemoServiceUnitTest
    {
        [Fact]
        public async Task Delete_Demo_Should_Return_NotFound_If_Demo_Doesnt_Exist()
        {
             //Arrange 
            var mockDemoRepo = Substitute.For<IDemoRepository>();
            mockDemoRepo.DeleteAsync(Arg.Any<int>()).Returns(ResourceOperationResult.NotFound);
            var service = new DemoService(mockDemoRepo);
            //Act
            var result = await service.DeleteDemo(999);

            //Assert
            Assert.Equal(ResourceOperationResult.NotFound, result);
        }

        [Fact]
        public async Task Delete_Demo_Should_Return_Success_If_Demo_Exist()
        {
             //Arrange 
            var mockDemoRepo = Substitute.For<IDemoRepository>();
            mockDemoRepo.DeleteAsync(Arg.Any<int>()).Returns(ResourceOperationResult.Success);
            var service = new DemoService(mockDemoRepo);
            //Act
            var result = await service.DeleteDemo(100);

            //Assert
            Assert.Equal(ResourceOperationResult.Success, result);
        }

        [Fact]
        public async Task Update_Demo_Should_Return_Updated_Object_If_Update_Succeeds()
        {
             //Arrange 
            var mockDemoRepo = Substitute.For<IDemoRepository>();
            mockDemoRepo.UpdateAsync(Arg.Any<API.Entities.Demo>()).Returns(GetUpdatedDemoEntity());
            var service = new DemoService(mockDemoRepo);
            //Act
            var result = await service.UpdateDemo(new API.DTOs.Demo{ Id = 101, Name ="Updated", Code = "ABC" });
            var updatedObject = result.Data as API.DTOs.Demo;
            //Assert
            Assert.NotNull(updatedObject);
            Assert.Equal("Updated", updatedObject.Name);
        }

        [Fact]
        public async Task Update_Demo_Should_Return_Error_Message_Object_If_Update_Fails()
        {
             //Arrange 
            var mockDemoRepo = Substitute.For<IDemoRepository>();
            mockDemoRepo.UpdateAsync(Arg.Any<API.Entities.Demo>()).Returns((API.Entities.Demo)null);
            var service = new DemoService(mockDemoRepo);
            //Act
            var result = await service.UpdateDemo(new API.DTOs.Demo{ Id = 101, Name ="Updated", Code = "ABC" });
            //Assert
            Assert.NotNull(result);
            Assert.Equal("Updating the resource failed.", result.Message);
        }

        private API.DTOs.Demo GetUpdatedDemo()
        {
            return new API.DTOs.Demo { Id = 101, Name ="Updated", Code = "ABC" };
        }

        private API.Entities.Demo GetUpdatedDemoEntity()
        {
            return new API.Entities.Demo{Id = 101, Name ="Updated", Code = "ABC"};
        }
    }
}
