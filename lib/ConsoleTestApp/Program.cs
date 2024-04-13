using PuppeteerSharp;

namespace ConsoleTestApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync().ConfigureAwait(false);
            using (var browser = (await Puppeteer.LaunchAsync(
                new LaunchOptions
                {
                    Headless = false,
                }).ConfigureAwait(false)))
            {
                using var page = await browser.NewPageAsync().ConfigureAwait(false);
                await page.GoToAsync("http://www.baidu.com").ConfigureAwait(false);
            }


        }
    }
}
