using System.Text.Json;
using System.Web;
using Commons.Models;
using ImageViewerDataLayer.Converters;
using ImageViewerDomain.DTO;
using ImageViewerDomain.Helpers;
using ImageViewerDomain.Models;
using ImageViewerDomain.Services;

namespace ImageViewerDataLayer.Services;

public class FlickrApiServices(IApiHelper apiHelper, HttpClient httpClient, FlickrPhotoConverter photoConverter, FlickrGalleryConverter galleryConverter, FlickrPhotoInfoConverter photoInfoConverter) : IFlickrApiServices
{
     
    public async Task<ResponseModel<List<IPhoto>>> SearchPhotosByText(string text, int pageIndex, int itemsPerPage)
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        var endpoint = apiHelper.BaseUrl + apiHelper.FlickrSearchPhotosEndpoint;
        object[] myValues = { text, itemsPerPage.ToString(), pageIndex.ToString() }; 
        endpoint = string.Format(endpoint, myValues); 
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            
        try
        {
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();
                        var responseSuccess = JsonSerializer.Deserialize<FlickrPhotoResponseDTO>(json, options);
                        if (responseSuccess is { Status: "ok" })
                        {
                            if (responseSuccess.Photos.Page <= responseSuccess.Photos.Pages)
                            {
                                //Se sto chiedendo entro il numero di pagine allora ritorno il risultato
                                return new ResponseModel<List<IPhoto>>()
                                {
                                    Metadata = new Metadata()
                                    {
                                        Result = true
                                    },
                                    StatusCode = response.StatusCode,
                                    Payload = photoConverter.toEntities(responseSuccess.Photos.PhotoList)
                                };
                            }
                            else
                            {
                                //Altrimenti ritorno lista vuota
                                return new ResponseModel<List<IPhoto>>()
                                {
                                    Metadata = new Metadata()
                                    {
                                        Result = true
                                    },
                                    StatusCode = response.StatusCode,
                                    Payload = []
                                };
                            }
                            
                        }
                        var responseError = JsonSerializer.Deserialize<FlickrErrorResponseDTO>(json, options);
                        return new ResponseModel<List<IPhoto>>()
                        {
                            Metadata = new Metadata()
                            {
                                Result = false,
                                ErrorMessage = responseError.Message
                            },
                            StatusCode = response.StatusCode
                        };
                    }
                }
                return new ResponseModel<List<IPhoto>>()
                {
                    Metadata = new Metadata()
                    {
                        Result = false,
                        ErrorMessage = "Server Error"
                    },
                    StatusCode = response.StatusCode
                };
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<IPhoto>>()
            {
                Metadata = new Metadata()
                {
                    ErrorMessage = ex.Message,
                    Result = false
                }
            };
        }
    }

    public async Task<ResponseModel<IPhotoDetails>> GetPhotoInfo(string photoId, string photoSecret)
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        var endpoint = apiHelper.BaseUrl + apiHelper.FlickrPhotosInfoEndpoint;
        object[] myValues = { photoId, photoSecret }; 
        endpoint = string.Format(endpoint, myValues); 
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            
        try
        {
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();
                        var responseSuccess = JsonSerializer.Deserialize<FlickrPhotoInfoResponseDTO>(json, options);
                        if (responseSuccess is { Status: "ok" })
                        {
                            return new ResponseModel<IPhotoDetails>()
                            {
                                Metadata = new Metadata()
                                {
                                    Result = true
                                },
                                StatusCode = response.StatusCode,
                                Payload = photoInfoConverter.toEntity(responseSuccess.PhotoInfo)
                            };
                        }
                        var responseError = JsonSerializer.Deserialize<FlickrErrorResponseDTO>(json, options);
                        return new ResponseModel<IPhotoDetails>()
                        {
                            Metadata = new Metadata()
                            {
                                Result = false,
                                ErrorMessage = responseError.Message
                            },
                            StatusCode = response.StatusCode
                        };
                    }
                }
                return new ResponseModel<IPhotoDetails>()
                {
                    Metadata = new Metadata()
                    {
                        Result = false,
                        ErrorMessage = "Server Error"
                    },
                    StatusCode = response.StatusCode
                };
        }
        catch (Exception ex)
        {
            return new ResponseModel<IPhotoDetails>()
            {
                Metadata = new Metadata()
                {
                    ErrorMessage = ex.Message,
                    Result = false
                }
            };
        }
    }

    public async Task<ResponseModel<List<IGallery>>> SearchGalleriesByUsername(string username, int pageIndex, int itemsPerPage)
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        var endpoint = apiHelper.BaseUrl + apiHelper.FlickrGalleriesEndpoint;
        object[] myValues = { username, itemsPerPage.ToString(), pageIndex.ToString() }; 
        endpoint = string.Format(endpoint, myValues); 
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            
        try
        {
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();
                        var responseSuccess = JsonSerializer.Deserialize<FlickrGalleriesResponseDTO>(json, options);
                        if (responseSuccess is { Status: "ok" })
                        {
                            if (int.Parse(responseSuccess.Galleries.Page) <= responseSuccess.Galleries.Pages)
                            {
                                //Se sto chiedendo entro il numero di pagine allora ritorno il risultato
                                return new ResponseModel<List<IGallery>>()
                                {
                                    Metadata = new Metadata()
                                    {
                                        Result = true
                                    },
                                    StatusCode = response.StatusCode,
                                    Payload = galleryConverter.toEntities(responseSuccess.Galleries.GalleryList)
                                };
                            }
                            else
                            {
                                //Altrimenti ritorno lista vuota
                                return new ResponseModel<List<IGallery>>()
                                {
                                    Metadata = new Metadata()
                                    {
                                        Result = true
                                    },
                                    StatusCode = response.StatusCode,
                                    Payload = []
                                };
                            }

                        }
                        var responseError = JsonSerializer.Deserialize<FlickrErrorResponseDTO>(json, options);
                        return new ResponseModel<List<IGallery>>()
                        {
                            Metadata = new Metadata()
                            {
                                Result = false,
                                ErrorMessage = responseError.Message
                            },
                            StatusCode = response.StatusCode
                        };
                    }
                }
                return new ResponseModel<List<IGallery>>()
                {
                    Metadata = new Metadata()
                    {
                        Result = false,
                        ErrorMessage = "Server Error"
                    },
                    StatusCode = response.StatusCode
                };
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<IGallery>>()
            {
                Metadata = new Metadata()
                {
                    ErrorMessage = ex.Message,
                    Result = false
                }
            };
        }
    }

    public async Task<ResponseModel<List<IPhoto>>> SearchPhotosOfGallery(string galleryId, int pageIndex, int itemsPerPage)
    {
       var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        var endpoint = apiHelper.BaseUrl + apiHelper.FlickrSearchPhotosEndpoint;
        object[] myValues = { galleryId, itemsPerPage.ToString(), pageIndex.ToString() }; 
        endpoint = string.Format(endpoint, myValues); 
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            
        try
        {
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();
                        var responseSuccess = JsonSerializer.Deserialize<FlickrPhotoResponseDTO>(json, options);
                        if (responseSuccess is { Status: "ok" })
                        {
                            return new ResponseModel<List<IPhoto>>()
                            {
                                Metadata = new Metadata()
                                {
                                    Result = true
                                },
                                StatusCode = response.StatusCode,
                                Payload = photoConverter.toEntities(responseSuccess.Photos.PhotoList)
                            };
                        }
                        var responseError = JsonSerializer.Deserialize<FlickrErrorResponseDTO>(json, options);
                        return new ResponseModel<List<IPhoto>>()
                        {
                            Metadata = new Metadata()
                            {
                                Result = false,
                                ErrorMessage = responseError.Message
                            },
                            StatusCode = response.StatusCode
                        };
                    }
                }
                return new ResponseModel<List<IPhoto>>()
                {
                    Metadata = new Metadata()
                    {
                        Result = false,
                        ErrorMessage = "Server Error"
                    },
                    StatusCode = response.StatusCode
                };
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<IPhoto>>()
            {
                Metadata = new Metadata()
                {
                    ErrorMessage = ex.Message,
                    Result = false
                }
            };
        }
    }
}