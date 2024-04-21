namespace PharmaCheck.Web.Models.Category;

public sealed record GetCategoriesFilterModel
{
    public int Page { get; set; }
    public string NameQuery { get; set; } = string.Empty;
}