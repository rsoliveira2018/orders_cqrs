using Core.Entities;

namespace Infrastructure.Seed;

public static class SeedValues
{
    public static readonly List<Customer> CUSTOMERS =
        [
            new() { Name = "Joelho Bichado", Email = "joelho_bichado@gmail.com", Phone = "(00) 900000000" },
            new() { Name = "Cotovelo Pontiagudo", Email = "cotovelo_pontiagudo@gmail.com", Phone = "(00) 900000000" },
        ];

    public static readonly List<Product> PRODUCTS =
        [
            new() { Name = "Jujuba Carioca", Price = 2.39M },
            new() { Name = "Garrafa Invertida", Price = 75.29M },
            new() { Name = "Mala Sem Alça", Price = 548.99M }
        ];
}
