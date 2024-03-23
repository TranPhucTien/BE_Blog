using System.ComponentModel.DataAnnotations;

namespace Blog.Models.Validations;

public class CurrentDateOrLaterAttribute : ValidationAttribute
{
    private readonly string _name;

    public CurrentDateOrLaterAttribute(string name)
    {
        _name = name;
    }

    
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        DateTime currentDate = DateTime.Now.Date;
        DateTime publishedAt = ((DateTime)value).Date;

        if (publishedAt >= currentDate)
        {
            return ValidationResult.Success;
        }

        string propertyName = validationContext.DisplayName;
        string errorMessage = $"{propertyName} phải lớn hơn hoặc bằng ngày hiện tại ({currentDate:d}).";

        return new ValidationResult(errorMessage);
    }

}