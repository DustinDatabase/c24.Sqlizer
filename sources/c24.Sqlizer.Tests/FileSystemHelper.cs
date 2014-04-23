using System.IO;

namespace c24.Sqlizer.Tests
{
    public static class FileSystemHelper
    {
        public static string CreateTempWorkingDirectory()
        {
            var workingDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(workingDirectory);
            return workingDirectory;
        }

        public static void CreateSubdirectory(string baseDirectory, string subdirectoryName)
        {
            var subDirectory = Path.Combine(baseDirectory, subdirectoryName);

            Directory.CreateDirectory(subDirectory);
        }

        public static void CreateFile(string baseDirectory, string fileName)
        {
            var path = Path.Combine(baseDirectory, fileName);

            File.Create(path).Close();
        }

        public static void RemoveDirectory(string dir)
        {
            Directory.Delete(dir);
        }
    }
}