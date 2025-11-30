namespace HaloPixelToolBox.Interface.Services;

public interface IPageService
{
    event RoutedEventHandler? CurrentPageLoaded;
    Page CurrentPage { get; }

    void Initialize(Page page);
}