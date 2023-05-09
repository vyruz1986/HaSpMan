using System.ComponentModel.DataAnnotations;

namespace Web.Validators;

[AttributeUsage(AttributeTargets.Property)]
public class RequiredIfStringIsNullOrWhitespace : RequiredAttribute
{
    private string DependantPropertyName { get; set; }

    public RequiredIfStringIsNullOrWhitespace(string dependantPropertyName)
    {
        DependantPropertyName = dependantPropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        var instance = context.ObjectInstance;
        var type = instance.GetType();

        var dependantPropertyValue = type?.GetProperty(DependantPropertyName)?.GetValue(instance)?.ToString();

        return string.IsNullOrWhiteSpace(value as string) && string.IsNullOrWhiteSpace(dependantPropertyValue)
            ? new ValidationResult(ErrorMessage)
            : ValidationResult.Success;
    }
}