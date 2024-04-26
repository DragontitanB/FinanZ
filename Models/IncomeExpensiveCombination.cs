
using MyFi.Controllers;

namespace MyFi.Models
{
    public class IncomeExpensiveCombination
    {
        public List<Expense> _expenses { get; set; }
        public List<Income> _incomes { get; set; }

        public IncomeExpensiveCombination(List<Expense> expenses, List< Income > income ) {
            _expenses = expenses;
            _incomes = income;
        
        }
    }
}
