using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using Hoo.Service.Controllers;
using HooService.Common;
using Moq;
using NUnit.Framework.Internal;
using Hoo.Service.Services;
using Microsoft.Extensions.Logging;

public static class FixtureFactory
{
    public static Fixture Create(int recursionDepth = 2, bool configureMembers = true, bool configureThrowingRecursionBehavior = true)
    {
        Fixture fixture = new Fixture();
        
        fixture.Customize((ICustomization)new AutoMoqCustomization()
        {
            ConfigureMembers = configureMembers
        });
        
        if (configureThrowingRecursionBehavior)
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList<ThrowingRecursionBehavior>().ForEach((Action<ThrowingRecursionBehavior>)(throwingRecursionBehavior => fixture.Behaviors.Remove((ISpecimenBuilderTransformation)throwingRecursionBehavior)));
        
        fixture.Behaviors.Add((ISpecimenBuilderTransformation)new OmitOnRecursionBehavior(recursionDepth));
        
        return fixture;
    }
}

namespace Hoo.UnitTests.Controllers
{
    public class FileControllerTests
    {
        private IFixture _fixture;
        private Mock<IFileProviderService> _fileProviderServiceStub;
        private Mock<IFileThumbnailProviderService> _fileThumbnailProviderServiceStub;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _fileProviderServiceStub = _fixture.Freeze<Mock<IFileProviderService>>();
            _fileThumbnailProviderServiceStub = _fixture.Freeze<Mock<IFileThumbnailProviderService>>();
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
    }
}
