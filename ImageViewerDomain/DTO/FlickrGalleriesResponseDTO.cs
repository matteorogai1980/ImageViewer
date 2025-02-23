using System.Text.Json.Serialization;

namespace ImageViewerDomain.DTO;

public class FlickrGalleriesResponseDTO
{
    public Galleries Galleries { get; set; }
    [JsonPropertyName("stat")]
    public string? Status { get; set; }
}

public class Galleries
{
    public int Total { get; set; }
    [JsonPropertyName("per_page")]
    public int PageCount { get; set; }
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
    public int Page { get; set; }
    public int Pages { get; set; }
    [JsonPropertyName("gallery")]
    public List<GalleryDTO> GalleryList { get; set; }
}

public class GalleryDTO
{
    public string Id { get; set; }
    [JsonPropertyName("gallery_id")]
    public string GalleryId { get; set; }
    public string Url { get; set; }
    public string Owner { get; set; }
    public string Username { get; set; }
    public string IconServer { get; set; }
    public int IconFarm { get; set; }
    public string primary_photo_id { get; set; }
    public string date_create { get; set; }
    public string date_update { get; set; }
    public int count_photos { get; set; }
    public int count_videos { get; set; }
    public int count_total { get; set; }
    public int count_views { get; set; }
    public int count_comments { get; set; }
    public Title title { get; set; }
    public Description description { get; set; }
    public object sort_group { get; set; }
    public string primary_photo_server { get; set; }
    public int primary_photo_farm { get; set; }
    public string primary_photo_secret { get; set; }
}

public class Title
{
    [JsonPropertyName("_content")]
    public string Content { get; set; }
}

public class Description
{
    [JsonPropertyName("_content")]
    public string Content { get; set; }
}