namespace PharmaCheck.Web.Models.Category;

public sealed record GetCategoriesFilterModel
{
    public string NameQuery { get; set; } = string.Empty;
}