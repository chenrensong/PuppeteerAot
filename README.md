# PuppeteerAot

[![NuGet](https://buildstats.info/nuget/PuppeteerAot)][NugetUrl]

[NugetUrl]: https://www.nuget.org/packages/PuppeteerAot/

PuppeteerAot is fork from [PuppeteerAot](https://github.com/hardkoded/puppeteer-sharp) and support for AOT compilation. 

## Why do we create this library?

* Using PupperSharp and publishing AOT in net 8.0 environment, the program cannot execute well.

## What modifications have been made?

1. Net Standard 2.0 to Net 8.0
2. WebClient to HttpClient 
3. NewtonSoft.Json to System.Text.Json 

## Prerequisites

* As PuppeteerAot is a Net 8.0 library, the minimum platform versions are .Net 8.0. [Read more](https://docs.microsoft.com/en-us/dotnet/standard/net-standard).
* If you have issues running Chrome on Linux, the Puppeteer repo has a [great troubleshooting guide](https://github.com/puppeteer/puppeteer/blob/master/docs/troubleshooting.md).
* X-server is required on Linux.

## How to Contribute and Provide Feedback

Some of the best ways to contribute are to try things out file bugs and fix issues.

If you have an issue or a question:

* File a [new issue](https://github.com/chenrensong/PuppeteerAot/issues/new).

## Usage

## Take screenshots

<!-- snippet: ScreenshotAsync -->
<a id='snippet-ScreenshotAsync'></a>
```cs
using var browserFetcher = new BrowserFetcher();
await browserFetcher.DownloadAsync();
await using var browser = await Puppeteer.LaunchAsync(
    new LaunchOptions { Headless = true });
await using var page = await browser.NewPageAsync();
await page.GoToAsync("http://www.google.com");
await page.ScreenshotAsync(outputFile);
```
<sup><a href='https://github.com/hardkoded/puppeteer-sharp/blob/master/lib/PuppeteerAot.Tests/ScreenshotTests/PageScreenshotTests.cs#L53-L61' title='Snippet source file'>snippet source</a> | <a href='#snippet-ScreenshotAsync' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

You can also change the view port before generating the screenshot

<!-- snippet: SetViewportAsync -->
<a id='snippet-SetViewportAsync'></a>
```cs
await Page.SetViewportAsync(new ViewPortOptions
{
    Width = 500,
    Height = 500
});
```
<sup><a href='https://github.com/hardkoded/puppeteer-sharp/blob/master/lib/PuppeteerAot.Tests/ScreenshotTests/ElementHandleScreenshotTests.cs#L13-L19' title='Snippet source file'>snippet source</a> | <a href='#snippet-SetViewportAsync' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

### Generate PDF files

<!-- snippet: PdfAsync -->
<a id='snippet-PdfAsync'></a>
```cs
using var browserFetcher = new BrowserFetcher();
await browserFetcher.DownloadAsync();
await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
await using var page = await browser.NewPageAsync();
await page.GoToAsync("http://www.google.com"); // In case of fonts being loaded from a CDN, use WaitUntilNavigation.Networkidle0 as a second param.
await page.EvaluateExpressionHandleAsync("document.fonts.ready"); // Wait for fonts to be loaded. Omitting this might result in no text rendered in pdf.
await page.PdfAsync(outputFile);
```
<sup><a href='https://github.com/hardkoded/puppeteer-sharp/blob/master/lib/PuppeteerAot.Tests/PageTests/PdfTests.cs#L23-L33' title='Snippet source file'>snippet source</a> | <a href='#snippet-PdfAsync' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

### Inject HTML

<!-- snippet: SetContentAsync -->
<a id='snippet-SetContentAsync'></a>
```cs
await using var page = await browser.NewPageAsync();
await page.SetContentAsync("<div>My Receipt</div>");
var result = await page.GetContentAsync();
```
<sup><a href='https://github.com/hardkoded/puppeteer-sharp/blob/master/lib/PuppeteerAot.Tests/PageTests/SetContentTests.cs#L14-L20' title='Snippet source file'>snippet source</a> | <a href='#snippet-SetContentAsync' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

### Evaluate Javascript

<!-- snippet: Evaluate -->
<a id='snippet-Evaluate'></a>
```cs
await using var page = await browser.NewPageAsync();
var seven = await page.EvaluateExpressionAsync<int>("4 + 3");
var someObject = await page.EvaluateFunctionAsync<dynamic>("(value) => ({a: value})", 5);
Console.WriteLine(someObject.a);
```
<sup><a href='https://github.com/hardkoded/puppeteer-sharp/blob/master/lib/PuppeteerAot.Tests/QuerySelectorTests/ElementHandleQuerySelectorEvalTests.cs#L16-L21' title='Snippet source file'>snippet source</a> | <a href='#snippet-Evaluate' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

### Wait For Selector

```cs
using (var page = await browser.NewPageAsync())
{
    await page.GoToAsync("http://www.spapage.com");
    await page.WaitForSelectorAsync("div.main-content")
    await page.PdfAsync(outputFile));
}
```

### Wait For Function

```cs
using (var page = await browser.NewPageAsync())
{
    await page.GoToAsync("http://www.spapage.com");
    var watchDog = page.WaitForFunctionAsync("()=> window.innerWidth < 100");
    await page.SetViewportAsync(new ViewPortOptions { Width = 50, Height = 50 });
    await watchDog;
}
```

### Connect to a remote browser

```cs
var options = new ConnectOptions()
{
    BrowserWSEndpoint = $"wss://www.externalbrowser.io?token={apikey}"
};

var url = "https://www.google.com/";

using (var browser = await PuppeteerAot.Puppeteer.ConnectAsync(options))
{
    using (var page = await browser.NewPageAsync())
    {
        await page.GoToAsync(url);
        await page.PdfAsync("wot.pdf");
    }
}
```

