using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
public class RequiredIfStringEqualsAttribute : RequiredAttribute
{
    private string DependantPropertyName { get; set; }
    private string DependantValue { get; }

    public RequiredIfStringEqualsAttribute(string dependantPropertyName, string dependantValue)
    {
        DependantPropertyName = dependantPropertyName;
        DependantValue = dependantValue;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        object instance = context.ObjectInstance;
        Type type = instance.GetType();

        var dependantPropertyValue = type?.GetProperty(DependantPropertyName)?.GetValue(instance)?.ToString();
        if(dependantPropertyValue is not null && dependantPropertyValue.Equals(DependantValue)
        && string.IsNullOrWhiteSpace(value as string))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}