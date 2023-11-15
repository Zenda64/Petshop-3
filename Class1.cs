
using System;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
    // Create an instance of ProductLogic at the top of the file
    var productLogic = new ProductLogic();

    static void Main()
    {
        Console.WriteLine("Press 1 to add a product");
        Console.WriteLine("Press 2 to view a specific product");
        Console.WriteLine("Type 'exit' to quit");

        string userInput = Console.ReadLine();

        while (userInput.ToLower() != "exit")
        {
            if (userInput == "1")
            {
                Console.WriteLine("What type of product do you want to add? (CatFood/DogLeash/DryCatFood)");
                string productType = Console.ReadLine();

                if (productType.ToLower() == "catfood")
                {
                    CatFood catFood = new CatFood();
                    // User input for CatFood properties...
                    productLogic.AddProduct(catFood);
                    Console.WriteLine("CatFood added successfully!");
                }
                else if (productType.ToLower() == "dogleash")
                {
                    DogLeash dogLeash = new DogLeash();
                    // User input for DogLeash properties...
                    productLogic.AddProduct(dogLeash);
                    Console.WriteLine("DogLeash added successfully!");
                }
                else if (productType.ToLower() == "drycatfood")
                {
                    DryCatFood dryCatFood = new DryCatFood();
                    // User input for DryCatFood properties...
                    productLogic.AddProduct(dryCatFood);
                    Console.WriteLine("DryCatFood added successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid product type.");
                }
            }
            else if (userInput == "2")
            {
                Console.WriteLine("Enter the name of the DogLeash or DryCatFood you want to view:");
                string productName = Console.ReadLine();

                Product viewedProduct = productLogic.GetProductByName(productName);

                if (viewedProduct != null)
                {
                    Console.WriteLine(JsonSerializer.Serialize(viewedProduct));
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }

            Console.WriteLine("Press 1 to add a product");
            Console.WriteLine("Press 2 to view a specific product");
            Console.WriteLine("Type 'exit' to quit");
            userInput = Console.ReadLine();
        }
    }
}

class ProductLogic
{
    private List<Product> _products;
    private Dictionary<string, DogLeash> _dogLeashDict;
    private Dictionary<string, CatFood> _catFoodDict;
    private Dictionary<string, DryCatFood> _dryCatFoodDict;

    public ProductLogic()
    {
        _products = new List<Product>();
        _dogLeashDict = new Dictionary<string, DogLeash>();
        _catFoodDict = new Dictionary<string, CatFood>();
        _dryCatFoodDict = new Dictionary<string, DryCatFood>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);

        if (product is DogLeash)
        {
            DogLeash dogLeash = product as DogLeash;
            _dogLeashDict[dogLeash.Name] = dogLeash;
        }
        else if (product is CatFood)
        {
            CatFood catFood = product as CatFood;
            _catFoodDict[catFood.Name] = catFood;
        }
        else if (product is DryCatFood)
        {
            DryCatFood dryCatFood = product as DryCatFood;
            _dryCatFoodDict[dryCatFood.Name] = dryCatFood;
        }
    }

    public List<Product> GetAllProducts()
    {
        return _products;
    }

    public Product GetProductByName(string name)
    {
        if (_dogLeashDict.ContainsKey(name))
        {
            return _dogLeashDict[name];
        }
        else if (_catFoodDict.ContainsKey(name))
        {
            return _catFoodDict[name];
        }
        else if (_dryCatFoodDict.ContainsKey(name))
        {
            return _dryCatFoodDict[name];
        }
        else
        {
            return null;
        }
    }
}

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}

class CatFood : Product
{
    public bool KittenFood { get; set; }
}

class DryCatFood : CatFood
{
    public double Weight { get; set; }
}

class DogLeash : Product
{
    public int LengthInches { get; set; }
    public string Material { get; set; }
}