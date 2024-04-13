using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuppeteerSharp.Helpers
{
    /// <summary>
    /// 下载进度条.
    /// </summary>
    /// <param name="totalFileSize">总文件大小.</param>
    /// <param name="totalBytesDownloaded">已下载文件大小.</param>
    /// <param name="percentageDownloaded">整体的进度.</param>
    public delegate void DownloadProgressHandler(long? totalFileSize, long totalBytesDownloaded, double? percentageDownloaded);
}
