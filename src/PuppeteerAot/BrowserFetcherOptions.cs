using System;
using System.Threading;
using System.Threading.Tasks;

namespace PuppeteerAot
{
    /// <summary>
    /// Browser fetcher options used to construct a <see cref="BrowserFetcher"/>.
    /// </summary>
    public class BrowserFetcherOptions
    {
        /// <summary>
        /// A custom download delegate.
        /// </summary>
        /// <param name="downloadUrl">downloadUrl.</param>
        /// <param name="destinationFilePath">destinationFilePath.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A Task that resolves when the download finishes.</returns>
        public delegate Task CustomFileDownloadAction(string downloadUrl, string destinationFilePath, CancellationToken cancellationToken = default);

        /// <summary>
        /// Product. Defaults to Chrome.
        /// </summary>
        public SupportedBrowser Browser { get; set; } = SupportedBrowser.Chrome;

        /// <summary>
        /// Platform. Defaults to current platform.
        /// </summary>
        public Platform? Platform { get; set; }

        /// <summary>
        /// A path for the downloads folder. Defaults to [root]/.local-chromium, where [root] is where the project binaries are located.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// A download host to be used. Defaults to https://storage.googleapis.com.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets the default or a custom download delegate.
        /// </summary>
        public CustomFileDownloadAction CustomFileDownload { get; set; }
    }
}
