using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MauiBlazorApp1.Shared;

public partial class Tabs
{
    private MudTabs _mudTabs;
    public List<TabView> TabViews { get; set; }
    private int _index = -1;

    private bool _updatedIndex = false;
    private bool _updatedContent = false;

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        TabViews = TabHelper.TabViews;
        TabHelper.TabDataChangedEvent = null;
        TabHelper.IndexChangedEvent = null;
        TabHelper.TabDataChangedEvent += TabDataChangedEvent;
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
    private void TabDataChangedEvent()
    {
        StateHasChanged();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (_updatedIndex)
        {
            _index = TabHelper.TabIndex;
            _mudTabs.ActivatePanel(TabViews[_index].Id);
            StateHasChanged();
            _updatedIndex = false;
            return;
        }

        if (_updatedContent) {
            //_mudTabs.ActivatePanel(TabViews[_index].Id);
            //StateHasChanged();
            _updatedContent = false;
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
        //_index = _mudTabs.ActivePanelIndex;
        //ChildContent = TabViews[_mudTabs.ActivePanelIndex].Content;
        //StateHasChanged();
    }

    private async void CloseTabCallback(MudTabPanel panel) {
        var index = TabHelper.RemoveTabView((Guid)panel.ID);
        if (index == _index && TabViews.Count > index) {
            //_updatedContent = true;
            _mudTabs.ActivatePanel(TabViews[index].Id);
            await panel.DisposeAsync();
            //StateHasChanged();
        }
    }

    private void ActivePanelIndexChanged() {
        Console.WriteLine($"index={_index}");
        Console.WriteLine($"activeIndex={_mudTabs.ActivePanelIndex}");
    }
}