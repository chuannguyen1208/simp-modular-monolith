using System.ComponentModel.DataAnnotations;

namespace Simp.Modules.Shops.Domain.Entities;
public class Ingredient
{
    protected Ingredient(string name, string stockName, int stockLevel)
    {
        Name = name;
        StockName = stockName;
        StockLevel = stockLevel;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string StockName { get; private set; }

    public int StockLevel { get; private set; }

    [Timestamp]
    public byte[] Version { get; private set; } = [];

    public static Ingredient Create(
        string name,
        int stockLevel,
        string stockName)
    {
        var entity = new Ingredient(name, stockName, stockLevel);
        return entity;
    }

    public void Update(string name, int stockLevel, string stockName)
    {
        Name = name;
        StockLevel = stockLevel;
        StockName = stockName;
    }

    public void Subtract(int stockLevel)
    {
        if (StockLevel < stockLevel)
        {
            return;
        }

        StockLevel -= stockLevel;
    }
}
