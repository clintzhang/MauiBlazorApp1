using Microsoft.AspNetCore.Components;

namespace MauiBlazorApp1.Shared;

public partial class NavMenu {
    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public TabHelper TabHelper { get; set; }

    protected override void OnInitialized() {
        base.OnInitialized();
        
    }

    protected override void OnAfterRender(bool firstRender) {
        base.OnAfterRender(firstRender);
        if (firstRender) {
            NavClick(typeof(Pages.Index));
        }
        
    }

    private void NavClick(Type type) {
        var url = type == typeof(Pages.Index) ? "/" : type.Name;
        TabHelper.AddTabView(type, type.Name, url);
    }
}