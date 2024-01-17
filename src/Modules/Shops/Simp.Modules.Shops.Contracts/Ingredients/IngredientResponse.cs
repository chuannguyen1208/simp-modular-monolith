namespace Simp.Modules.Shops.Contracts.Ingredients;
public record IngredientResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string StockName { get; set; }
    public int StockLevel { get; set; }
}
