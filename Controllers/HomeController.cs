using Microsoft.AspNetCore.Mvc;
using MyFi.Models;
using System.Diagnostics;

namespace MyFi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpenseService _expenseService = new ExpenseService();
        private readonly IncomeService _incomeService = new IncomeService();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Income> incomes = _incomeService.GetAllIncome();
            List<Expense> expenses = _expenseService.GetAllExpenses();

            IncomeExpensiveCombination iec = new IncomeExpensiveCombination(expenses, incomes);


            return View(iec);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ExpenseService 
    {
        public List<Expense> GetAllExpenses()
        {
            // Lista de gastos
            List<Expense> expenses = new List<Expense>
            {
                new Expense { Id = 1, Name = "Comida", Date = new DateTime(2024, 04, 09), Amount = 5000, Automatic = false, Color = "red" },
                new Expense { Id = 2, Name = "Transporte", Date = new DateTime(2024, 04, 10), Amount = 30000, Automatic = true, Color = "Bblue" },
                new Expense { Id = 3, Name = "Facturas", Date = new DateTime(2024, 04, 16), Amount = 10000, Automatic = false, Color = "green" },
                new Expense { Id = 4, Name = "Entretenimiento", Date = new DateTime(2024, 04, 03), Amount = 2000, Automatic = true, Color = "yellow" },
                new Expense { Id = 5, Name = "Ropa", Date = new DateTime(2024, 04, 05), Amount = 7000, Automatic = false, Color = "orange" },
                new Expense { Id = 6, Name = "Salud", Date = new DateTime(2024, 04, 13), Amount = 6000, Automatic = true, Color = "purple" },
                new Expense { Id = 7, Name = "Educación", Date = new DateTime(2024, 04, 01), Amount = 800, Automatic = false, Color = "pink" },
                new Expense { Id = 8, Name = "Hogar", Date = new DateTime(2024, 04, 11), Amount = 90, Automatic = true, Color = "brown" },
                new Expense { Id = 9, Name = "Impuestos", Date = new DateTime(2024, 04, 10), Amount = 1100, Automatic = false, Color = "gray" },
                new Expense { Id = 10, Name = "Otros", Date = new DateTime(2024, 04, 15), Amount = 400, Automatic = true, Color = "cyan" }

             };

            return expenses;
        }
    }

    public class IncomeService
    {
        public List<Income> GetAllIncome()
    {
        // Lista de ingresos
        List<Income> incomes = new List<Income>
        {
            new Income { Id = 11, Name = "Salario", Date = new DateTime(2024, 04, 10), Amount = 2000, Automatic = true, Color = "greeen" },
            new Income { Id = 12, Name = "Bonificación", Date = new DateTime(2024, 04, 09), Amount = 5000, Automatic = false, Color = "blue" },
            new Income { Id = 13, Name = "Venta de activos", Date = new DateTime(2024, 04, 11), Amount = 3000, Automatic = true, Color = "red" },
            new Income { Id = 14, Name = "Inversiones", Date = new DateTime(2024, 04, 13), Amount = 800, Automatic = false, Color = "yellow" },
            new Income { Id = 15, Name = "Préstamo", Date = new DateTime(2024, 04, 05), Amount = 4000, Automatic = true, Color = "orange" },
            new Income { Id = 16, Name = "Regalo", Date = new DateTime(2024, 04, 08), Amount = 100, Automatic = false, Color = "purple" },
            new Income { Id = 17, Name = "Reembolso", Date = new DateTime(2024, 04, 02), Amount = 2000, Automatic = true, Color = "pink" },
            new Income { Id = 18, Name = "Comisión", Date = new DateTime(2024, 04, 11), Amount = 60000, Automatic = false, Color = "brown" },
            new Income { Id = 19, Name = "Subsidio", Date = new DateTime(2024, 04, 15), Amount = 35000, Automatic = true, Color = "gray" },
            new Income { Id = 20, Name = "Intereses", Date = new DateTime(2024, 04, 16), Amount = 25000, Automatic = false, Color = "cyan" }

        };

        return incomes;
    }
}

public class Expense
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int Amount { get; set; }
    public bool Automatic { get; set; }
    public string Color { get; set; }
}

    public class Income
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public bool Automatic { get; set; }
        public string Color { get; set; }
    }
}