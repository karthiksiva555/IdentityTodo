using IdentityTodo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityTodo.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private ApplicationDbContext _context;

    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext ctx)
    {
        _logger = logger;
        _context = ctx;
    }

    [BindProperty(SupportsGet = true)]
    public bool ShowComplete { get; set; }
    public IEnumerable<TodoItem> TodoItems { get; set; }
    
    public void OnGet()
    {
        TodoItems = _context.TodoItems
            .Where(t => User.Identity != null && t.Owner == User.Identity.Name).OrderBy(t => t.Task);
        if (!ShowComplete) {
            TodoItems = TodoItems.Where(t => !t.Complete);
        }
        TodoItems = TodoItems.ToList();
    }
    
    public IActionResult OnPostShowComplete() {
        return RedirectToPage(new { ShowComplete });
    }
    
    public async Task<IActionResult> OnPostAddItemAsync(string task) {
        if (!string.IsNullOrEmpty(task)) {
            TodoItem item = new TodoItem {
                Task = task,
                Owner = User.Identity?.Name,
                Complete = false
            };
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage(new { ShowComplete });
    }
    public async Task<IActionResult> OnPostMarkItemAsync(long id) {
        TodoItem? item = _context.TodoItems.Find(id);
        if (item != null) {
            item.Complete = !item.Complete;
            await _context.SaveChangesAsync();
        }
        return RedirectToPage(new { ShowComplete });
    }
}
