using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImageViewerDomain.DTO;

public class PhotoDTO
{
    public string? Id { get; set; }
    public string? Owner { get; set; }
    public string? Secret { get; set; }
    public string? Server { get; set; }
    public int Farm { get; set; }
    public string? Title { get; set; }
    public int IsPublic { get; set; }
    public int IsFriend { get; set; }
    public int IsFamily { get; set; }
}

public class PhotosDTO
{
    public int Page { get; set; }
    public int Pages { get; set; }
    [JsonPropertyName("perpage")]
    public int PageCount { get; set; }
    public int Total { get; set; }
    
    [JsonPropertyName("photo")]
    public List<PhotoDTO>? PhotoList { get; set; }
}

public class FlickrPhotoResponseDTO
{
    public PhotosDTO? Photos { get; set; }
    [JsonPropertyName("stat")]
    public string? Status { get; set; }
}