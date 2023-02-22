﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Fileupload
{
    public class Fileuploadbase : ComponentBase, IDisposable
    {

        [Inject] protected IHttpClientFactory ihttpclientfactory { get; set; }
        [Inject] protected Fileuploadservice FileUploadService { get; set; }

        [Parameter] public Dictionary<string, string> FileUploadHeaders { get; set; }
        [Parameter] public string ApiUrl { get; set; }
        [Parameter] public string InputFileId { get; set; }
        [Parameter] public string DropzoneElementId { get; set; }

        public int maxFiles { get; set; } = 10;

        public event EventHandler<Dictionary<Guid, Fileuploadmodel>> OnUploadImagesEvent;
        public Dictionary<Guid, Fileuploadmodel> FileUploadModels = new Dictionary<Guid, Fileuploadmodel>();

        public string Output { get; set; }
        public float progresswidth { get; set; }
        public float progressnow { get; set; }
        public float progresstotal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.FileUploadService.FileUploadServiceExtension.OnDropEvent += OnFileUploadDropEventExecute;
            this.OnUploadImagesEvent += OnUploadImagesExecute;

            await base.OnInitializedAsync();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await this.FileUploadService.InitFileUploadDropzone(this.InputFileId, this.DropzoneElementId);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task FileUploadChange(InputFileChangeEventArgs e)
        {
            var imageFormat = "image/png";

            foreach (var iBrowserFile in e.GetMultipleFiles(this.maxFiles))
            {
                var previewImage = await iBrowserFile.RequestImageFileAsync(imageFormat, 100, 100);
                var bytes = new byte[previewImage.Size];
                await previewImage.OpenReadStream().ReadAsync(bytes);
                var imageDataUrl = $"data:{imageFormat};base64,{Convert.ToBase64String(bytes)}";

                var model = new Fileuploadmodel()
                {
                    Base64ImageUrl = imageDataUrl,
                    BrowserFile = iBrowserFile,
                };

                this.FileUploadModels.Add(Guid.NewGuid(), model);
            }
        }

        private void OnUploadImagesExecute(object sender, Dictionary<Guid, Fileuploadmodel> e)
        {
            Console.WriteLine("on upload images execute..");
        }

        public async void UploadImages_Clicked()
        {
            this.OnUploadImagesEvent.Invoke(this, this.FileUploadModels);
            await this.UploadFiles(this.FileUploadModels);
            this.FileUploadModels.Clear();
            this.StateHasChanged();
        }

        public void RemoveThumbnail_Clicked(Guid key)
        {
            this.FileUploadModels.Remove(key);
            this.StateHasChanged();
        }

        private async Task UploadFiles(Dictionary<Guid, Fileuploadmodel> models)
        {
            try
            {                
                var maxAllowedSize = 5120000;
                MultipartFormDataContent content = new MultipartFormDataContent();

                this.Output = string.Empty;
                this.progresswidth = 0;
                this.progressnow = 0;
                this.progresstotal = 0;

                foreach(var model in models)
                {
                    this.progresstotal += model.Value.BrowserFile.Size;
                }

                foreach (var model in models)
                {
                    var readstream = model.Value.BrowserFile.OpenReadStream(maxAllowedSize);                    
                    var newline = Environment.NewLine;
                    var buffersize = 4096;
                    var buffer = new byte[buffersize];
                    int read;

                    MemoryStream stream = new MemoryStream(100);
                    while ((read = await readstream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        await stream.WriteAsync(buffer, 0, read);
                        await InvokeAsync(() =>
                        {
                            this.Output += $"Read {read} bytes. {readstream.Position} / {readstream.Length}{newline}";
                            this.progressnow += buffer.Length;
                            this.progresswidth = this.progressnow / this.progresstotal * 100;
                            this.StateHasChanged();
                        });
                    }

                    if (stream.Length == stream.Position)
                    {
                        stream.Position = 0;
                    }

                    var filename = string.Concat(model.Value.BrowserFile.Name.Split(Path.GetInvalidFileNameChars()));
                    content.Add(new StreamContent(stream), "files", filename);
                }

                if (this.FileUploadHeaders != null && this.FileUploadHeaders.Any())
                {
                    foreach (var item in this.FileUploadHeaders)
                    {
                        content.Headers.Add(item.Key, item.Value);
                    }
                }

                var client = this.ihttpclientfactory.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                var result = await client.PostAsync(this.ApiUrl, content);
                var remotePath = await result.Content.ReadAsStringAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void OnFileUploadDropEventExecute(object sender, Fileuploadevent e)
        {
            if (this.DropzoneElementId == e.FileUploadDropzoneId)
            {

            }
        }

        public void Dispose()
        {
            this.OnUploadImagesEvent -= OnUploadImagesExecute;
        }

    }
}
