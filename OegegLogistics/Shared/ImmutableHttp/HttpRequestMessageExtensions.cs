using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OegegLogistics.ViewModels.Enums;
using HttpRequestMessage = OegegLogistics.Shared.ImmutableHttp.HttpRequestMessage;

namespace OegegLogistics.Shared;

public static class HttpRequestMessageExtensions
{
    public static HttpRequestMessage WithUrl(this HttpRequestMessage request, Uri url)
    {
        return request with { RequestUri = url };
    }

    public static HttpRequestMessage Endpoint(this HttpRequestMessage request, string endpoint)
    {
        return request with {RequestUri = new Uri(request.RequestUri + $"/{endpoint}")};
    }
    
    public static HttpRequestMessage Method(this HttpRequestMessage request, HttpMethod method)
    {
        return request with { Method = method };
    }

    public static HttpRequestMessage Path(this HttpRequestMessage request, string path)
    {
        if (!string.IsNullOrWhiteSpace(request.RequestUri?.Query))
            throw new InvalidOperationException("Cannot call ToPath after Query was provided!");
        return request with { RequestUri = new Uri(request.RequestUri + $"/{path}") };
    }

    public static HttpRequestMessage PageSize(this HttpRequestMessage request, uint pageSize)
    {
        UriBuilder uriBuilder = new UriBuilder(request.RequestUri);
        string existing = uriBuilder.Query;

        if (!string.IsNullOrWhiteSpace(existing))
            existing += "&";
        uriBuilder.Query = existing + $"pageSize={pageSize}";
        return request with { RequestUri = uriBuilder.Uri };
    }

    public static HttpRequestMessage PageNumber(this HttpRequestMessage request, uint pageNumber)
    {
        UriBuilder uriBuilder = new UriBuilder(request.RequestUri);
        string existing = uriBuilder.Query;
        
        if(!string.IsNullOrWhiteSpace(existing))
            existing += "&";
        uriBuilder.Query = existing + $"pageNumber={pageNumber}";

        return request with { RequestUri = uriBuilder.Uri };
    }

    public static HttpRequestMessage VehicleType(this HttpRequestMessage request, VehicleType vehicleType)
    {
        UriBuilder uriBuilder = new UriBuilder(request.RequestUri);
        string existing = uriBuilder.Query;
        
        if(!string.IsNullOrWhiteSpace(existing))
            existing += "&";
        uriBuilder.Query = existing + $"vehicleType={vehicleType}";

        return request with { RequestUri = uriBuilder.Uri };
    }

    public static async Task<HttpRequestMessage> Content(this HttpRequestMessage request, object content)
    {
        if(request.Method == HttpMethod.Get)
            throw new InvalidOperationException("Cannot add Content when HttpMethod is Get!");
        
        using MemoryStream memoryStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(memoryStream, content).ConfigureAwait(false);
        memoryStream.Seek(0, SeekOrigin.Begin);
        
        using StreamContent streamContent = new StreamContent(memoryStream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        
        return request with { Content = streamContent };
    }

    public static HttpRequestMessage Headers(this HttpRequestMessage request, IDictionary<string, string> headers)
    {
        return request with { Headers = headers};
    }

    public static HttpRequestMessage Authorization(this HttpRequestMessage request, string token)
    {
        IDictionary<string, string> headers = request.Headers ?? new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {token}");
        
        return request with { Headers = headers};
    }

    public static async Task<T> ExecuteAsync<T>(this HttpRequestMessage request, HttpClient client,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage responseMessage = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);

        responseMessage.EnsureSuccessStatusCode();
        
        await using Stream stream = await responseMessage.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<T>(stream, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
    
    public static async Task ExecuteAsync(this HttpRequestMessage request, HttpClient client,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage responseMessage = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);

        responseMessage.EnsureSuccessStatusCode();
    }
}