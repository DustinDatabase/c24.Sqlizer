namespace c24.Sqlizer.DirectoryValidation
{
    public interface IDirectoryValidationRule
    {
        void Validate(string directoryPath);
    }
}