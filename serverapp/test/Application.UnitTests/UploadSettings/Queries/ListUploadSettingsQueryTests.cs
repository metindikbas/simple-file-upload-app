using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using SimpleFileUpload.Application.Common.Interfaces;
using SimpleFileUpload.Application.UploadSettings.Queries.ListUploadSettings;
using SimpleFileUpload.Domain.Entities;
using SimpleFileUpload.Domain.Enums;
using Xunit;

namespace SimpleFileUpload.Application.UnitTests.UploadSettings.Queries
{
    public class ListUploadSettingsQueryTests
    {
        [Fact]
        public async Task ShouldListUploadSettingsQueryReturnsSettings()
        {
            // Arrange
            var setting1 = new UploadSetting(UploadSettingKeys.AllowedContentTypes, "content1");
            var setting2 = new UploadSetting(UploadSettingKeys.MaxAllowedSingleFileSizeInBytes, "content2");
            var uploadSettings = new List<UploadSetting> {setting1, setting2};
            var dbSet = TestHelpers.CreateMockDbSet(uploadSettings);

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(x => x.UploadSettings).Returns(dbSet);

            var handler = new ListUploadSettingsQueryHandler(dbContextMock.Object);
            var query = new ListUploadSettingsQuery();
            
            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            
            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }
    }
}