﻿using Microsoft.JSInterop;
using System.Threading;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Download
{
    public class DownloadMap
    {

        public string Id { get; set; }
        public string ApiQueryId { get; set; }
        public string FileType { get; set; }
        public IJSObjectReference JsObjectReference { get; set; }
        public bool DownloadInProgress { get; set; }
        public Task DownloadTask { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public DownloadApiItem LastApiItem { get; set; }

    }
}
