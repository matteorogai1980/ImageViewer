using ImageViewerDomain.Helpers;

namespace ImageViewer.Helpers;

public class ProdApiHelper : IApiHelper
{
    public string BaseUrl => "https://api.flickr.com/services/";
    public string FlickrApiKey => "255ac8fdac4726aa339fa1c2161b9e5b";
    public string FlickrApiSecret => "c2c1dbb234cd7d15";
    public string FlickrSearchPhotosEndpoint => "rest/?method=flickr.photos.search&api_key="+FlickrApiKey+"&format=json&content_type=1&media=photos&text={0}&per_page={1}&page={2}&nojsoncallback=true";
    public string FlickrPhotosInfoEndpoint => "rest/?method=flickr.photos.getInfo&api_key="+FlickrApiKey+"&format=json&photo_id={0}&secret={1}&nojsoncallback=true";
    public string FlickrGalleriesEndpoint => "rest/?method=flickr.galleries.getList&api_key="+FlickrApiKey+"&format=json&user_id={0}&per_page={1}&page={2}&nojsoncallback=true";
    public string FlickrGalleriesPhotosEndpoint => "rest/?method=flickr.galleries.getPhotos&api_key="+FlickrApiKey+"&format=json&gallery_id={0}&per_page={1}&page={2}&nojsoncallback=true";
}