namespace PharmaCheck.Web.Models.CategoryType;

public sealed record GetProductTypesFilter
{
    public int Page { get; set; }
    public string NameQuery { get; set; } = string.Empty;
}