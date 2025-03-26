using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace OegegLogistics.Shared.ImmutableHttp;

public sealed record HttpRequestMessage(
        HttpMethod Method,
        Uri? RequestUri = null,
        Version Version = null,
        HttpVersionPolicy VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        HttpContent? Content = null,
        IDictionary<string, string>? Headers = null,
        HttpRequestOptions? Options = null)
        : IDisposable
{
        public static HttpRequestMessage Empty = new HttpRequestMessage();
        
        public static Version DefaultRequestVersion => HttpVersion.Version11;
        public static HttpVersionPolicy DefaultVersionPolicy => HttpVersionPolicy.RequestVersionOrLower;

        private const int MessageAlreadySent = 1;
        private const int MessageIsRedirect = 2;
        private const int MessageDisposed = 4;

        private int _sendStatus = 0;

        public HttpRequestMessage(HttpMethod method, string? requestUri)
            : this(method, string.IsNullOrEmpty(requestUri) ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute))
        {
        }

        public HttpRequestMessage()
            : this(HttpMethod.Get, (Uri?)null)
        {
        }

        public bool WasSentByHttpClient() => (_sendStatus & MessageAlreadySent) != 0;
        public bool WasRedirected() => (_sendStatus & MessageIsRedirect) != 0;
        public bool IsDisposed => (_sendStatus & MessageDisposed) != 0;

        public bool MarkAsSent() => Interlocked.CompareExchange(ref _sendStatus, MessageAlreadySent, 0) == 0;
        public void MarkAsRedirected() => _sendStatus |= MessageIsRedirect;

        public void Dispose()
        {
            if (!IsDisposed)
            {
                _sendStatus |= MessageDisposed;
                Content?.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Method: {Method}, RequestUri: '{RequestUri.ToString() ?? "<null>"}', Version: {Version}");
            sb.Append($", Content: {(Content == null ? "<null>" : Content.GetType().ToString())}");
            sb.AppendLine(", Headers:");
            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    sb.AppendLine($"  {header.Key}: {string.Join(", ", header.Value)}");
                }
            }
            return sb.ToString();
        }

        // ✅ Implicit conversion operator for seamless use with HttpClient
        public static implicit operator System.Net.Http.HttpRequestMessage(HttpRequestMessage request)
        {
            var message = new System.Net.Http.HttpRequestMessage(request.Method, request.RequestUri)
            {
                Version = request.Version ?? DefaultRequestVersion,
                VersionPolicy = request.VersionPolicy,
                Content = request.Content
            };

            if (request.Headers != null)
            {
                foreach (var header in request.Headers)
                {
                    message.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            if (request.Options != null)
            {
                foreach (var option in request.Options)
                {
                    var key = new HttpRequestOptionsKey<object>(option.Key);
                    message.Options.Set(key, option.Value);
                }
            }

            return message;
        }
    }