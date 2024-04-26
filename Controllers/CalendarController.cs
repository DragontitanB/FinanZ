using Microsoft.AspNetCore.Mvc;
using MyFi.Models;
using System.Threading.Tasks;

namespace MyFi.Controllers
{
    public class CalendarController : Controller
    {
        private readonly EventService _eventService = new EventService();     // Servicio para operaciones relacionadas con Events


        // Constructor que recibe instancias de los servicios como dependencias
        /* public CalendarController(IEventService eventService)
         {
             _eventService = eventService;
         } */

        private readonly finanzasContext _context;

        public CalendarController(finanzasContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            List<Event> ViewModel = _eventService.GetAllViewModel();
            // Devuelve la vista con el modelo preparado
            return View(ViewModel);
        }
    }


    public class EventService
    {
        public List<Event> GetAllViewModel()
        {
            ExpenseService es = new ExpenseService();
            IncomeService iss = new IncomeService();


            // Lista de gastos
            List<Expense> expenses = es.GetAllExpenses();

            // Lista de ingresos
            List<Income> incomes = iss.GetAllIncome();


            List<Event> events = new List<Event>();

            foreach (var expense in expenses)
            {
                events.Add(new Event
                {
                    id = expense.Id,
                    name = expense.Name,
                    date = expense.Date,
                    amount = expense.Amount,
                    color = expense.Color,
                    automatic = expense.Automatic,
                    type = "gasto"
                });
            }

            foreach (var income in incomes)
            {
                events.Add(new Event
                {
                    id = income.Id,
                    name = income.Name,
                    date = income.Date,
                    amount = income.Amount,
                    color = income.Color,
                    automatic = income.Automatic,
                    type = "ingreso"

                });

            }

            return events;
        }
    }
}
