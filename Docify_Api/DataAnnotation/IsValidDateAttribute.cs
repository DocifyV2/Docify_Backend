using System.ComponentModel.DataAnnotations;

namespace Docify_Api.DataAnnotation
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IsValidDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            //Check if date is not past today
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date <= DateTime.Now && date >= new DateTime(1900, 1, 1);
            }
            
            return false;
        }
    }
}
