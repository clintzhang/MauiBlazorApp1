using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MauiBlazorApp1.Shared;

public partial class Tabs
{
    private MudTabs _mudTabs;
    public List<TabView> TabViews { get; set; }
    private int _index = -1;

    private bool _updatedIndex = false;

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        TabViews = TabHelper.TabViews;
        TabHelper.TabChangedEvent = null;
        TabHelper.IndexChangedEvent = null;
        TabHelper.TabChangedEvent += TabChangedEvent;
        TabHelper.IndexChangedEvent += IndexChangedEvent;
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        //if (TabViews.Count > 0 && _index>-1 && TabViews[_index].Content == null) {
        //    TabHelper.SetTabContent(_index, ChildContent);
        //}
    }

    private void IndexChangedEvent()
    {
        if (_index != TabHelper.TabIndex) {
            _updatedIndex = true;
            StateHasChanged();
        }
    }
    private void TabChangedEvent()
    {
        StateHasChanged();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (_updatedIndex)
        {
            //_mudTabs.ActivatePanel(_index);
            _index = TabHelper.TabIndex;
            StateHasChanged();
            _updatedIndex = false;
            return;
        }

        //if (_index == _mudTabs.ActivePanelIndex && _index > -1)
        //{
        //    if (TabViews.Count > 0 && TabViews[_index].Content == null)
        //    {
        //        NavigationManager.NavigateTo(TabViews[_index].Url);
        //        TabHelper.SetTabContent(_index, ChildContent);
        //        StateHasChanged();
        //    }
        //}

    }

    private void ActiveTabChanged(object id)
    {
        // _index = _mudTabs.ActivePanelIndex;
        //ChildContent = TabViews[_mudTabs.ActivePanelIndex].Content;
        //StateHasChanged();
    }

    private void CloseTabCallback(MudTabPanel panel)
    {
        var tabView = TabViews.FirstOrDefault(x => x.Title is { } && x.Title == (string)panel.ID);
        if (tabView != null)
        {
            TabViews.Remove(tabView);
            StateHasChanged();
        }
    }
}