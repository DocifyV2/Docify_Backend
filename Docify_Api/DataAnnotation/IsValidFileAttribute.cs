using System.ComponentModel.DataAnnotations;

namespace Docify_Api.DataAnnotation
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IsValidFileAttribute : ValidationAttribute
    {
        public List<string> AllowedExtensions { get; set; }

        public IsValidFileAttribute(string fileExtensions)
        {
            AllowedExtensions = fileExtensions
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(ext => '.' + ext.Trim())
                .ToList();
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            var file = value as IFormFile;

            var fileExtension = Path.GetExtension(file.FileName);

            return AllowedExtensions.Any(e => e == fileExtension);
        }
    }
}
