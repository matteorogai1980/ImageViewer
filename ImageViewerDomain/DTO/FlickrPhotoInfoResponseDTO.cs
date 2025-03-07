using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImageViewerDomain.DTO;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class FlickrPhotoInfoResponseDTO
    {
        [JsonPropertyName("photo")]
        public PhotoInfoDTO PhotoInfo { get; set; }
        [JsonPropertyName("stat")]
        public string Status { get; set; }
    }

    public class Comments
    {
        [JsonPropertyName("_content")]
        public string Content { get; set; }
    }

    public class Dates
    {
         public string taken { get; set; }
         
    }

    public class Editability
    {
        public int cancomment { get; set; }
        public int canaddmeta { get; set; }
    }

    public class Gift
    {
        public bool gift_eligible { get; set; }
        public List<string> eligible_durations { get; set; }
        public bool new_flow { get; set; }
    }

    public class Notes
    {
        public List<object> note { get; set; }
    }

    public class Owner
    {
        public string nsid { get; set; }
        public string username { get; set; }
        public string realname { get; set; }
        public object location { get; set; }
        public string iconserver { get; set; }
        public int iconfarm { get; set; }
        public string path_alias { get; set; }
        public Gift gift { get; set; }
    }

    public class People
    {
        public int haspeople { get; set; }
    }

    public class PhotoInfoDTO
    {
        public string id { get; set; }
        public string secret { get; set; }
        public string server { get; set; }
        public int farm { get; set; }
        public string dateuploaded { get; set; }
        public int isfavorite { get; set; }
        public string license { get; set; }
        public string safety_level { get; set; }
        public int rotation { get; set; }
        public string originalsecret { get; set; }
        public string originalformat { get; set; }
        public Owner owner { get; set; }
        public Title title { get; set; }
        public Description description { get; set; }
        public Visibility visibility { get; set; }
        public Dates? dates { get; set; }
        public string views { get; set; }
        public Editability editability { get; set; }
        public Publiceditability publiceditability { get; set; }
        public Usage usage { get; set; }
        public Comments comments { get; set; }
        public Notes notes { get; set; }
        public People people { get; set; }
        public Urls urls { get; set; }
        public string media { get; set; }
    }

    public class Publiceditability
    {
        public int cancomment { get; set; }
        public int canaddmeta { get; set; }
    }

    public class Tag
    {
        public string id { get; set; }
        public string author { get; set; }
        public string authorname { get; set; }
        public string raw { get; set; }
        public string _content { get; set; }
        public int machine_tag { get; set; }
    }

    public class Tags
    {
        public List<Tag> tag { get; set; }
    }

    public class Url
    {
        public string type { get; set; }
        public string _content { get; set; }
    }

    public class Urls
    {
        public List<Url> url { get; set; }
    }

    public class Usage
    {
        public int candownload { get; set; }
        public int canblog { get; set; }
        public int canprint { get; set; }
        public int canshare { get; set; }
    }

    public class Visibility
    {
        public int ispublic { get; set; }
        public int isfriend { get; set; }
        public int isfamily { get; set; }
    }

