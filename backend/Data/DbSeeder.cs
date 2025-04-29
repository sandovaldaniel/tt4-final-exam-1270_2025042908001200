namespace Backend.Data
{
    using Microsoft.EntityFrameworkCore;
    using Backend.Models;

    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Verifica si la tabla de Expenses ya contiene datos, evitando duplicaciones
            if (!context.Expenses.Any()) // Cambiar 'ExpenseItems' a 'Expenses'
            {
                // Adiciona algunos gastos de ejemplo
                var expenses = new List<ExpenseItem>
                {
                    new ExpenseItem { Title = "Grocery", Category = "Food", Amount = 50.75m, Date = DateTime.Now.AddDays(-10) },
                    new ExpenseItem { Title = "Rent", Category = "Housing", Amount = 1200.00m, Date = DateTime.Now.AddDays(-5) },
                    new ExpenseItem { Title = "Utilities", Category = "Bills", Amount = 150.00m, Date = DateTime.Now.AddDays(-8) },
                    new ExpenseItem { Title = "Internet", Category = "Bills", Amount = 60.00m, Date = DateTime.Now.AddDays(-15) },
                    new ExpenseItem { Title = "Transportation", Category = "Travel", Amount = 75.00m, Date = DateTime.Now.AddDays(-12) },
                    new ExpenseItem { Title = "Entertainment", Category = "Leisure", Amount = 40.00m, Date = DateTime.Now.AddDays(-7) },
                    new ExpenseItem { Title = "Medical", Category = "Health", Amount = 200.00m, Date = DateTime.Now.AddDays(-30) },
                    new ExpenseItem { Title = "Dining Out", Category = "Food", Amount = 25.00m, Date = DateTime.Now.AddDays(-2) },
                    new ExpenseItem { Title = "Subscription", Category = "Services", Amount = 15.00m, Date = DateTime.Now.AddDays(-1) },
                    new ExpenseItem { Title = "Shopping", Category = "Shopping", Amount = 100.00m, Date = DateTime.Now.AddDays(-20) }
                };

                context.Expenses.AddRange(expenses); // Cambiar 'ExpenseItems' a 'Expenses'
                context.SaveChanges();
            }
        }
    }
}

