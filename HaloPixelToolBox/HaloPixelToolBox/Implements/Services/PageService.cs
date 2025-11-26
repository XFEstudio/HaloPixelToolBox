using HaloPixelToolBox.Interface.Services;

namespace HaloPixelToolBox.Implements.Services;

public class PageService : IPageService
{
    Page? _page;
    public Page CurrentPage => _page ?? throw new InvalidOperationException("PageService has not been initialized. Please call Initialize() with a valid Page instance.");

    public event RoutedEventHandler? CurrentPageLoaded;

    public void Initialize(Page page)
    {
        _page = page;
        _page.Loaded += (s, e) => CurrentPageLoaded?.Invoke(s, e);
    }
}
