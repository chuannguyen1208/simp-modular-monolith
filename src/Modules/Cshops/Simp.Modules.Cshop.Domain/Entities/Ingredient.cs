using Simp.Shared.Abstractions.Primitives;

namespace Simp.Modules.Cshop.Domain.Entities;
public class Ingredient : AggregateRoot
{
    protected Ingredient(Guid id, string name, string stockName, int stockLevel) : base(id)
    {
        Name = name;
        StockName = stockName;
        StockLevel = stockLevel;
    }

    public string Name { get; private set; }
    public string StockName { get; private set; }
    public int StockLevel { get; private set; }

    public static Ingredient Create(string name, string stockName, int stockLevel)
    {
        return new Ingredient(Guid.NewGuid(), name, stockName, stockLevel);
    }
}
