using Microsoft.AspNetCore.Components;

namespace MauiBlazorApp1.Shared;

public class TabView
{
    public string Title { get; set; }
    public RenderFragment Content { get; set; }
    public string Url { get; set; }
}