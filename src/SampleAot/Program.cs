
using PuppeteerAot;
using SampleAot;

string _url = "https://metaso.cn/";

string _cookie = "";

string _jsCode = "div.markdown-body.MuiBox-root.css-0";


try
{
    using var browserFetcher = new BrowserFetcher();
    await browserFetcher.DownloadAsync().ConfigureAwait(false);
    using (var browser = (await Puppeteer.LaunchAsync(
        new LaunchOptions
        {
            Headless = false,
        }).ConfigureAwait(false)))
    {
        //await using var page = await browser.NewPageAsync();
        //await page.GoToAsync("https://www.baidu.com");
        //await page.ScreenshotAsync(Path.Combine(AppContext.BaseDirectory, "1.jpg"));
        await Helper.Run(browser, _url, _cookie, _jsCode, 3000, "今天天气怎么样", ((s) =>
        {
            Console.WriteLine(s);
        }));
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.ReadLine();



