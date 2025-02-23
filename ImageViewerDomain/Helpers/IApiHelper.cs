namespace ImageViewerDomain.Helpers;

public interface IApiHelper
{
    public string BaseUrl { get; }
    
    public string FlickrApiKey { get; }
    public string FlickrApiSecret { get; }
    
    public string FlickrSearchPhotosEndpoint { get; }
    public string FlickrPhotosInfoEndpoint { get; }
    public string FlickrGalleriesEndpoint { get; }
    public string FlickrGalleriesPhotosEndpoint { get; }

}