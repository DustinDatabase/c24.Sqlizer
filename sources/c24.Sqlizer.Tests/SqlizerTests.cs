using System;
using System.Collections.Generic;
using System.IO;
using c24.Sqlizer.DirectoryValidation;
using c24.Sqlizer.Infrastructure.Logging;
using c24.Sqlizer.ScriptsExecution;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace c24.Sqlizer.Tests
{
    [TestFixture]
    public class SqlizerTests
    {
        [Test]
        public void Sqlizer_Should_Throw_Argument_Exception_If_Script_Dir_Does_Not_Exists()
        {
            // arrange
            var dir = FileSystemHelper.CreateTempWorkingDirectory();

            FileSystemHelper.RemoveDirectory(dir);

            var directoryValidator = new Mock<IDirectoryValidator>();
            var scriptExecutor = new Mock<IScriptsExecutor>();
            var logger = new Mock<ILogger>();

            var sqlizer = new Sqlizer(directoryValidator.Object, scriptExecutor.Object, logger.Object);

            // act & assert
            Action action = () => sqlizer.RunDatabaseScripts(dir);

            action.ShouldThrow<ArgumentException>()
                .WithMessage(string.Format("Directory {0} does not exist", dir));
        }

        [Test]
        public void Sqlizer_Should_Order_Files_For_Execution()
        {
            // arrange
            var dir = FileSystemHelper.CreateTempWorkingDirectory();

            FileSystemHelper.CreateFile(baseDirectory: dir, fileName: "01_script.sql");
            FileSystemHelper.CreateFile(baseDirectory: dir, fileName: "03_script.sql");
            FileSystemHelper.CreateFile(baseDirectory: dir, fileName: "20_script.sql");
            FileSystemHelper.CreateFile(baseDirectory: dir, fileName: "02_script.sql");
            FileSystemHelper.CreateFile(baseDirectory: dir, fileName: "10_script.sql");
            FileSystemHelper.CreateFile(baseDirectory: dir, fileName: "15_script.sql");

            var actualResult = new List<string>();

            var directoryValidator = new Mock<IDirectoryValidator>();
            var scriptExecutor = new Mock<IScriptsExecutor>();
            scriptExecutor.Setup(s => s.Execute(It.IsAny<string>()))
                .Callback<string>(actualResult.Add);

            var logger = new Mock<ILogger>();

            var sqlizer = new Sqlizer(directoryValidator.Object, scriptExecutor.Object, logger.Object);
            
            // act 
            sqlizer.RunDatabaseScripts(dir);

            // assert
            actualResult.ShouldBeEquivalentTo(new List<string>
            {
                Path.Combine(dir, "01_script.sql"),
                Path.Combine(dir, "02_script.sql"),
                Path.Combine(dir, "03_script.sql"),
                Path.Combine(dir, "10_script.sql"),
                Path.Combine(dir, "15_script.sql"),
                Path.Combine(dir, "20_script.sql")
            });
        }
    }
}