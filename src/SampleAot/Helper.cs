using System;
using PuppeteerAot;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleAot
{
    public class Helper
    {
        public static async Task Run(IBrowser browser, string url,
            string cookieString, string jsCode, int timeDelay,
            string keyword = "今天天气怎么样？", Action<string> action = null)
        {

            await using var page = await browser.NewPageAsync();

            try
            {
                // 分割 cookie 字符串
                var cookieItems = cookieString.Split(';');

                foreach (var item in cookieItems)
                {
                    // 移除任何开始或结束的空白字符
                    var cookie = item.Trim();

                    // 分割 cookie 名称和值
                    var cookieParts = cookie.Split('=');
                    var cookieName = cookieParts[0].Trim();
                    var cookieValue = cookieParts[1].Trim();

                    // 创建 cookie 对象
                    var cookieParam = new CookieParam
                    {
                        Name = cookieName,
                        Value = cookieValue,
                        Domain = "metaso.cn" // 根据实际情况设置正确的域
                    };

                    // 设置 cookie
                    await page.SetCookieAsync(cookieParam);
                }
            }
            catch (Exception ex)
            {

            }


            await page.SetUserAgentAsync("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36 Edg/123.0.0.0");
            await page.GoToAsync(url);

            Stopwatch stopwatch = new Stopwatch();


            // 设置Console消息监听器来捕获从上述JavaScript注入的console.log输出
            page.Console += (sender, e) =>
            {
                //Console.WriteLine($"{e.Message.Text}");
            };

            // 监听 SSE 消息
            page.Response += async (sender, e) =>
            {

                if (e.Response.Request.ResourceType == ResourceType.EventSource)
                {

                }
            };


            // 等待textarea元素加载完成
            var textAreaSelector = "textarea"; // 修改为实际的选择器
            await page.WaitForSelectorAsync(textAreaSelector);

            // 在textarea中输入文字
            await page.TypeAsync(textAreaSelector, keyword);

            // 发送Enter命令
            await page.Keyboard.PressAsync("Enter");
            stopwatch.Start();

            await page.WaitForNavigationAsync(new NavigationOptions { WaitUntil = new[] { WaitUntilNavigation.DOMContentLoaded } });


            bool isOk = false;
            do
            {
                try
                {
                    await Task.Delay(20);

                    string monitorScript = "var targetNode = document.querySelector('" + jsCode + "');" +
                        " var config = { childList: true };" +
                        "var callback = function(mutationsList, observer) { };" +
                        " var observer = new MutationObserver(callback);" +
                        "observer.observe(targetNode, config); ";

                    await page.EvaluateExpressionAsync(monitorScript);
                    isOk = true;


                }
                catch (Exception ex)
                {

                }
            } while (!isOk);


            stopwatch.Stop();



            if (timeDelay <= 100)
            {
                timeDelay = 100;
            }

            if (action != null)
            {
                action($"关键字：{keyword} 总耗时：{stopwatch.ElapsedMilliseconds}ms ");
            }

            await Task.Delay(timeDelay);

        }
    }


}

