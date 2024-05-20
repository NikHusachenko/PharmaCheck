namespace PharmaCheck.Web.Models.CategoryType;

public sealed record UpdateCategoryNameModel
{
    public string NewName { get; set; } = string.Empty;
}