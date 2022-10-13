// <copyright file="Create.cshtml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewcomersTask.Web.Pages;

public class Create : PageModel
{
    private readonly ILogger<Create> _logger;

    public Create(ILogger<Create> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
