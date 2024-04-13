using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PuppeteerAot.Helpers
{
    /// <summary>
    /// PuppeteerHttpClient.
    /// </summary>
    public class PuppeteerHttpClient
    {
        /// <summary>
        /// 下载进度条
        /// </summary>
        public event DownloadProgressHandler DownloadProgressChanged;

        /// <summary>
        /// 设置代理.
        /// </summary>
        public IWebProxy WebProxy
        {
            get; set;
        }

        /// <summary>
        /// 是否可以下载.
        /// </summary>
        /// <param name="url">url.</param>
        /// <returns>true or false.</returns>
        public async Task<bool> CanDownloadAsync(string url)
        {
            using var httpClientHandler = CreateHttpClient();

            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                using (HttpResponseMessage result = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                {
                    return result.StatusCode == HttpStatusCode.PartialContent;
                }
            }
        }

        /// <summary>
        /// 下载文件.
        /// </summary>
        /// <param name="downloadUrl">downloadUrl.</param>
        /// <param name="destinationFilePath">destinationFilePath.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>Task.</returns>
        public async Task DownloadFileAsync(string downloadUrl, string destinationFilePath, CancellationToken cancellationToken = default)
        {
            using var httpClientHandler = CreateHttpClient();

            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                using (var response = await client.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();

                    long? totalDownloadSize = null;
                    if (response.Content.Headers.Contains("Content-Length"))
                    {
                        totalDownloadSize = response.Content.Headers.ContentLength;
                    }

                    long totalBytesDownloaded = 0;
                    using (var contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var fileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                    {
                        var buffer = new byte[4096];
                        int bytesRead;
                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
                            totalBytesDownloaded += bytesRead;
                            var percentageDownloaded = totalDownloadSize.HasValue ?
                                (double)totalBytesDownloaded / totalDownloadSize.Value * 100 :
                                (double?)null;
                            DownloadProgressChanged?.Invoke(totalDownloadSize, totalBytesDownloaded, percentageDownloaded);
                        }

                        await fileStream.FlushAsync().ConfigureAwait(false);
                    }
                }
            }
        }

        private HttpClientHandler CreateHttpClient()
        {
            var httpClientHandler = new HttpClientHandler();
            if (WebProxy != null)
            {
                httpClientHandler.Proxy = WebProxy;
                httpClientHandler.UseProxy = true;
            }

            return httpClientHandler;
        }
    }
}
