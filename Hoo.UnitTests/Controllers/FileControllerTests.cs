using NUnit.Framework;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using Hoo.Service.Common;
using Hoo.Service.Controllers;
using Moq;
using NUnit.Framework.Internal;
using Hoo.Service.Services;
using Microsoft.Extensions.Logging;
using Hoo.Service.Models;
using NUnit.Framework.Legacy;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.UnitTests.Controllers
{
    public class FileControllerTests
    {
        private IFixture _fixture;
        private Mock<IFileProviderService> _fileProviderServiceStub;
        private Mock<IFileThumbnailProviderService> _fileThumbnailProviderServiceStub;

        private FileThumbnailModel _fileThumbnailModelStub;
        private Guid _fileIdStub;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _fileProviderServiceStub = _fixture.Freeze<Mock<IFileProviderService>>();
            _fileThumbnailProviderServiceStub = _fixture.Freeze<Mock<IFileThumbnailProviderService>>();

            _fileThumbnailModelStub = _fixture.Create<FileThumbnailModel>();
            _fileIdStub = _fixture.Create<Guid>();
        }

        [Theory]
        public async Task GetFiles_FileProviderService_ExecutedOnce()
        {
            // Arrange
            var fileControllerSut = _fixture.Create<FileController>();

            // Act
            var _ = await fileControllerSut.GetFiles();

            // Assert
            _fileProviderServiceStub.Verify(mock => mock.GetFilesAsync(), Times.Once);
        }

        [Theory]
        public async Task GetFileCount_FileProviderService_ExecutedOnce()
        {
            // Arrange
            var fileControllerSut = _fixture.Create<FileController>();

            // Act
            var _ = await fileControllerSut.GetFileCount();

            // Assert
            _fileProviderServiceStub.Verify(mock => mock.GetFilesAsync(), Times.Once);
        }

        [Theory]
        public async Task GetFiles_GetFirstPage_ReturnsValidPage()
        {
            // Arrange
            var files = Enumerable.Repeat(_fixture.Create<FileItemModel>(), 100).ToArray();

            _fileProviderServiceStub.Setup(mock => mock.GetFilesAsync()).ReturnsAsync(files);
            
            var fileControllerSut = _fixture.Create<FileController>();

            // Act
            var result = await fileControllerSut.GetFiles();

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var actualResult = ((result as OkObjectResult).Value) as FileItemPageResponseModel;

            ClassicAssert.AreEqual(0, actualResult.PageIndex);
            ClassicAssert.AreEqual(100, actualResult.ItemCount);
            ClassicAssert.AreEqual(files, actualResult.Files);
        }

        [Theory]
        public async Task GetFiles_GetFirstPage_ReturnsOnlyFirstPage()
        {
            // Arrange
            var files = Enumerable.Repeat(_fixture.Create<FileItemModel>(), 10000).ToArray();

            _fileProviderServiceStub.Setup(mock => mock.GetFilesAsync()).ReturnsAsync(files);

            var fileControllerSut = _fixture.Create<FileController>();

            // Act
            var result = await fileControllerSut.GetFiles();

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var actualResult = ((result as OkObjectResult).Value) as FileItemPageResponseModel;

            ClassicAssert.AreEqual(0, actualResult.PageIndex);
            ClassicAssert.AreEqual(100, actualResult.ItemCount);
            ClassicAssert.AreEqual(files.Take(100), actualResult.Files);
        }

        [Theory]
        [TestCase(1, 100)]
        [TestCase(3, 25)]
        [TestCase(99, 100)]
        [TestCase(999, 100)]
        public async Task GetFiles_GetAnyPage_ReturnsOnlyRequestedPage(int pageIndex, int itemsPerPage)
        {
            // Arrange
            var files = Enumerable.Repeat(_fixture.Create<FileItemModel>(), 10000).ToArray();
            var expected = files.Skip(pageIndex * itemsPerPage).Take(itemsPerPage);

            _fileProviderServiceStub.Setup(mock => mock.GetFilesAsync()).ReturnsAsync(files);

            var fileControllerSut = _fixture.Create<FileController>();

            // Act
            var result = await fileControllerSut.GetFiles(pageIndex, itemsPerPage);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var actualResult = ((result as OkObjectResult).Value) as FileItemPageResponseModel;

            ClassicAssert.AreEqual(pageIndex, actualResult.PageIndex);
            ClassicAssert.AreEqual(expected.Count(), actualResult.ItemCount);
            ClassicAssert.AreEqual(expected, actualResult.Files);
        }

        [Theory]
        public async Task GetFileThumbnail_FileThumbnailProviderService_ExecutedOnce()
        {
            // Arrange
            var fileControllerSut = _fixture.Create<FileController>();

            _fileThumbnailProviderServiceStub.Setup(mock => mock.GetFileThumbnailAsync(It.IsAny<Guid>()))
                .ReturnsAsync(_fileThumbnailModelStub);

            // Act
            var _ = await fileControllerSut.GetFileThumbnail(_fileIdStub);

            // Assert
            _fileThumbnailProviderServiceStub.Verify(mock => mock.GetFileThumbnailAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        public async Task GetFileThumbnail_ReturnsExpectedValue()
        {
            // Arrange
            var fileControllerSut = _fixture.Create<FileController>();

            _fileThumbnailProviderServiceStub.Setup(mock => mock.GetFileThumbnailAsync(_fileIdStub))
                .ReturnsAsync(_fileThumbnailModelStub);

            // Act
            var result = await fileControllerSut.GetFileThumbnail(_fileIdStub);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var actualResult = ((result as OkObjectResult).Value) as FileThumbnailResponseModel;

            ClassicAssert.AreEqual(_fileThumbnailModelStub.FileId, actualResult.FileId);
            ClassicAssert.AreEqual(_fileThumbnailModelStub.ThumbnailUrl, actualResult.ThumbnailUrl);
        }

        [Theory]
        public async Task GetFileThumbnail_FileDoesNotExist_ReturnsNull()
        {
            // Arrange
            var fileControllerSut = _fixture.Create<FileController>();

            _fileThumbnailProviderServiceStub.Setup(mock => mock.GetFileThumbnailAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<FileThumbnailModel>(null));

            // Act
            var result = await fileControllerSut.GetFileThumbnail(_fileIdStub);

            // Assert
            ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
