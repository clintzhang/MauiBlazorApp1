using MauiBlazorApp1.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Index = MauiBlazorApp1.Pages.Index;

namespace MauiBlazorApp1.Shared;

public class TabHelper {
    public delegate void TabChanged();

    public static List<TabView> TabViews = new();
    public static int TabIndex;

    public static TabChanged TabChangedEvent;
    public static TabChanged IndexChangedEvent;
    
    public static bool AddTabView(Type type, string title, string url) {
        var isNew = false;
        var tab = TabViews.Find(t => t.Url == url);
        if (tab == null) {
            tab = new TabView()
            {
                Title = title,
                Content = GetRenderFragment(type),
                Url = url
            };
            TabViews.Add(tab);
            isNew = true;
        }

        if (isNew) {
            TabChangedEvent?.Invoke();
        }

        TabIndex = TabViews.IndexOf(tab);
        IndexChangedEvent?.Invoke();

        return isNew;
    }


    public static void SetTabContent(int index, RenderFragment content) {
        TabViews[index].Content = content;
    }
    private static RenderFragment GetRenderFragment(Type type)
    {
        void Fragment(RenderTreeBuilder renderFragmentBuilder)
        {
            renderFragmentBuilder.OpenComponent(0, type);
            renderFragmentBuilder.CloseComponent();
        }

        return Fragment;
    }
}