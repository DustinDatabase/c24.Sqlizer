using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using c24.Sqlizer.Exceptions;

namespace c24.Sqlizer.DirectoryValidation.Rules
{
    public class FilesOrderValidationRule : IDirectoryValidationRule
    {
        public void Validate(string directoryPath)
        {
            var regex = new Regex(@"^(?<index>[0-9]+).+\.sql$");

            var indexes = new DirectoryInfo(directoryPath)
                .GetFiles()
                .Select(f =>
                {
                    var match = regex.Match(f.Name);

                    var index = match.Groups["index"].Value;

                    return int.Parse(index);
                })
                .ToList();
            
            if (indexes.Count < 2)
            {
                return;
            }

            indexes.Sort();
            
            for (var i = 0; i < indexes.Count - 1; i++)
            {
                var diff = indexes[i + 1] - indexes[i];

                if (diff != 1)
                {
                    throw new WrongOrderException("There is a gap between scripts. Files should be contiguous");
                }
            }            
        }
    }
}