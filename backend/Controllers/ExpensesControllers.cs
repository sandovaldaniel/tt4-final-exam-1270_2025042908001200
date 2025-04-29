namespace Backend.Controllers
{
    using Backend.Data;
    using Backend.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpensesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseItem>>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        // GET: /Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseItem>> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null) return NotFound();
            return expense;
        }

        // POST: /Expenses
        [HttpPost]
        public async Task<ActionResult<ExpenseItem>> CreateExpense(ExpenseItem expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        // PUT: /Expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, ExpenseItem expense)
        {
            if (id != expense.Id) return BadRequest();

            _context.Entry(expense).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null) return NotFound();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
