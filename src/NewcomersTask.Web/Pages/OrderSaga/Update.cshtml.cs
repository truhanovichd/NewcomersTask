using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewcomersTask.Web.Pages;

public class Update : PageModel
{
    private readonly ILogger<Update> _logger;

    public Update(ILogger<Update> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}