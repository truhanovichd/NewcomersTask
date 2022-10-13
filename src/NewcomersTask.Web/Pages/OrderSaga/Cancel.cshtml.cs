using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewcomersTask.Web.Models;

namespace NewcomersTask.Web.Pages.OrderSaga;

public class Cancel : PageModel
{
    private readonly ILogger<Cancel> _logger;

    public Cancel(ILogger<Cancel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public CancelOrderRequest Model { get; set; }

    public void OnPost(CancelOrderRequest report)
    {
    }
}
