using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using SimpleFileUpload.Application.Common.Exceptions;
using SimpleFileUpload.Application.Common.Interfaces;
using SimpleFileUpload.Application.SimpleFiles.Commands.UploadFiles;
using SimpleFileUpload.Domain.Entities;
using SimpleFileUpload.Domain.Enums;
using Xunit;

namespace SimpleFileUpload.Application.UnitTests.SimpleFiles.Commands
{
    public class UploadFilesCommandTests
    {
        [Fact]
        public async Task ShouldUploadFilesCommandThrowsUnsupportedMediaTypeExceptionWhenUnsupportedImageUploaded()
        {
            // Arrange
            var uploadDate = new DateTime(2021, 5, 6);
            var mediaTypeSetting = new UploadSetting(UploadSettingKeys.AllowedContentTypes, "image/png");
            var uploadSettings = new List<UploadSetting> {mediaTypeSetting};
            var simpleFiles = new List<SimpleFile>();
            var filesDbSet = TestHelpers.CreateMockDbSet(simpleFiles);
            var uploadSettingsDbSet = TestHelpers.CreateMockDbSet(uploadSettings);

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(x => x.SimpleFiles).Returns(filesDbSet);
            dbContextMock.Setup(x => x.UploadSettings).Returns(uploadSettingsDbSet);

            var handler =
                new UploadFilesCommandHandler(dbContextMock.Object, TestHelpers.CreateDateTimeProvider(uploadDate));
            var query = new UploadFilesCommand
            {
                Files = new List<UploadFileDto>
                {
                    new("", "image/jpeg", new byte[5])
                }
            };

            //Act
            //Assert
            await Should.ThrowAsync<UnsupportedMediaTypeException>(handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldUploadFilesCommandThrowsMaximumSizeExceedsExceptionWhenUnsupportedImageUploaded()
        {
            // Arrange
            var uploadDate = new DateTime(2021, 5, 6);
            var fileSizeSetting = new UploadSetting(UploadSettingKeys.MaxAllowedSingleFileSizeInBytes, "1");
            var uploadSettings = new List<UploadSetting> {fileSizeSetting};
            var simpleFiles = new List<SimpleFile>();
            var filesDbSet = TestHelpers.CreateMockDbSet(simpleFiles);
            var uploadSettingsDbSet = TestHelpers.CreateMockDbSet(uploadSettings);

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(x => x.SimpleFiles).Returns(filesDbSet);
            dbContextMock.Setup(x => x.UploadSettings).Returns(uploadSettingsDbSet);

            var handler =
                new UploadFilesCommandHandler(dbContextMock.Object, TestHelpers.CreateDateTimeProvider(uploadDate));
            var query = new UploadFilesCommand
            {
                Files = new List<UploadFileDto>
                {
                    new("", "", new byte[5])
                }
            };

            //Act
            //Assert
            await Should.ThrowAsync<MaximumSizeExceedsException>(handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldUploadFilesCommandSuccessWhenSingleValidImageGiven()
        {
            // Arrange
            var uploadDate = new DateTime(2021, 5, 6);
            var mediaTypeSetting = new UploadSetting(UploadSettingKeys.AllowedContentTypes, "image/jpeg");
            var fileSizeSetting = new UploadSetting(UploadSettingKeys.MaxAllowedSingleFileSizeInBytes, "5");
            var uploadSettings = new List<UploadSetting> {mediaTypeSetting, fileSizeSetting};
            var simpleFiles = new List<SimpleFile>();
            var filesDbSet = TestHelpers.CreateMockDbSet(simpleFiles);
            var uploadSettingsDbSet = TestHelpers.CreateMockDbSet(uploadSettings);

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(x => x.SimpleFiles).Returns(filesDbSet);
            dbContextMock.Setup(x => x.UploadSettings).Returns(uploadSettingsDbSet);

            var handler =
                new UploadFilesCommandHandler(dbContextMock.Object, TestHelpers.CreateDateTimeProvider(uploadDate));
            var query = new UploadFilesCommand
            {
                Files = new List<UploadFileDto>
                {
                    new("", "image/jpeg", new byte[5])
                }
            };

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            dbContextMock.Verify(x => x.SimpleFiles.AddAsync(It.IsAny<SimpleFile>(), It.IsAny<CancellationToken>()),
                Times.Exactly(query.Files.Count));
            filesDbSet.Count().ShouldBe(query.Files.Count);
        }
        
        [Fact]
        public async Task ShouldUploadFilesCommandSuccessWhenMultipleValidImageGiven()
        {
            // Arrange
            var uploadDate = new DateTime(2021, 5, 6);
            var mediaTypeSetting = new UploadSetting(UploadSettingKeys.AllowedContentTypes, "image/jpeg,image/png");
            var fileSizeSetting = new UploadSetting(UploadSettingKeys.MaxAllowedSingleFileSizeInBytes, "5");
            var uploadSettings = new List<UploadSetting> {mediaTypeSetting, fileSizeSetting};
            var simpleFiles = new List<SimpleFile>();
            var filesDbSet = TestHelpers.CreateMockDbSet(simpleFiles);
            var uploadSettingsDbSet = TestHelpers.CreateMockDbSet(uploadSettings);

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(x => x.SimpleFiles).Returns(filesDbSet);
            dbContextMock.Setup(x => x.UploadSettings).Returns(uploadSettingsDbSet);

            var handler =
                new UploadFilesCommandHandler(dbContextMock.Object, TestHelpers.CreateDateTimeProvider(uploadDate));
            var query = new UploadFilesCommand
            {
                Files = new List<UploadFileDto>
                {
                    new("", "image/jpeg", new byte[5]),
                    new("", "image/png", new byte[3]),
                    new("", "image/jpeg", new byte[1])
                }
            };

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            dbContextMock.Verify(x => x.SimpleFiles.AddAsync(It.IsAny<SimpleFile>(), It.IsAny<CancellationToken>()),
                Times.Exactly(query.Files.Count));
            filesDbSet.Count().ShouldBe(query.Files.Count);
        }
    }
}