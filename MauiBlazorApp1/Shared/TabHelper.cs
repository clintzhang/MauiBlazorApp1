using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
namespace MauiBlazorApp1.Shared;

public class TabHelper {
    public delegate void TabChanged();

    public static List<TabView> TabViews = new();
    public static int TabIndex;

    public static TabChanged TabDataChangedEvent;
    public static TabChanged IndexChangedEvent;
    
    public static bool AddTabView(Type type, string title, string url) {
        var isNew = false;
        var tab = TabViews.Find(t => t.Url == url);
        if (tab == null) {
            tab = new TabView()
            {
                Title = title,
                Content = GetRenderFragment(type),
                Url = url,
                Id = Guid.NewGuid()
            };
            TabViews.Add(tab);
            isNew = true;
        }

        if (isNew) {
            TabDataChangedEvent?.Invoke();
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
           // renderFragmentBuilder.SetKey(Guid.NewGuid());
            renderFragmentBuilder.CloseComponent();
        }

        return Fragment;
    }

    public static int RemoveTabView(Guid id) {
        var tabView = TabViews.FirstOrDefault(x => x.Id == id);
        var index = TabViews.IndexOf(tabView);
        if (tabView != null)
        {
            TabViews.Remove(tabView);
        }

        return index;
    }
}