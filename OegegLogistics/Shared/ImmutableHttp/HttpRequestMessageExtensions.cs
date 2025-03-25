using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HttpRequestMessage = OegegLogistics.Shared.ImmutableHttp.HttpRequestMessage;

namespace OegegLogistics.Shared;

public static class HttpRequestMessageExtensions
{
    public static HttpRequestMessage WithUrl(this HttpRequestMessage request, Uri url)
    {
        return request with { RequestUri = url };
    }

    public static HttpRequestMessage WithMethod(this HttpRequestMessage request, HttpMethod method)
    {
        return request with { Method = method };
    }

    public static HttpRequestMessage ToPath(this HttpRequestMessage request, string path)
    {
        if (request.RequestUri?.Query != null || request.RequestUri?.Query != string.Empty)
            throw new InvalidOperationException("Cannot call ToPath after Query was provided!");
        return request with { RequestUri = new Uri(request.RequestUri + $"/{path}") };
    }

    public static HttpRequestMessage WithQueryString(this HttpRequestMessage request, string queryString)
    {
        return request with { RequestUri = new Uri(request.RequestUri + queryString) };
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

    public static HttpRequestMessage WithHeaders(this HttpRequestMessage request, IDictionary<string, string> headers)
    {
        return request with { Headers = headers};
    }

    public static async Task<T> ExecuteAsync<T>(this HttpRequestMessage request, HttpClient client,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage responseMessage = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);

        responseMessage.EnsureSuccessStatusCode();
        
        await using Stream stream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<T>(stream, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}