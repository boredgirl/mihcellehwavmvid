using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text;

namespace Mihcelle.Hwavmvid.Client
{
    public class Applicationservice
    {

        private NavigationManager _navigationmanager { get; set; }
        public HubConnection? _connection { get; set; }

        private const string _administrator = "host";

        private string _applicationid = System.Guid.NewGuid().ToString();

        public Applicationservice(NavigationManager navigationmanager)
        {
            this._navigationmanager = navigationmanager;
        }

        public async Task<bool> Establishapplicationconnection()
        {
            try
            {

                if (this._connection?.State == HubConnectionState.Connected
                 || this._connection?.State == HubConnectionState.Connecting
                 || this._connection?.State == HubConnectionState.Reconnecting)
                {
                    // throw alert
                }

                this._connection = this.Buildapplicationhubconnection();
                await this._connection.StartAsync().ContinueWith(async task =>
                {
                    if (task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.Faulted)
                    {

                        await this._connection.SendAsync("Establishapplicationconnection").ContinueWith((task) =>
                        {
                            if (task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.Faulted)
                            {

                            }
                        });
                    }
                });

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return false;
        }

        private HubConnection Buildapplicationhubconnection()
        {

            StringBuilder urlBuilder = new StringBuilder();
            var hubconnectionuri = this._navigationmanager.BaseUri + "api/applicationhub";

            urlBuilder.Append(hubconnectionuri);
            urlBuilder.Append("?username=" + _administrator);

            var url = urlBuilder.ToString();
            return new HubConnectionBuilder().WithUrl(url, options =>
            {
                //options.Cookies.Add(this.IdentityCookie);
                options.Headers.Add("applicationid", this._applicationid);
                options.Headers.Add("platform", "mihcelle.hwavmvid");
                options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
            })
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.WriteIndented = false;
                options.PayloadSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                options.PayloadSerializerOptions.AllowTrailingCommas = true;
                options.PayloadSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
                options.PayloadSerializerOptions.DefaultBufferSize = 4096;
                options.PayloadSerializerOptions.MaxDepth = 41;
                options.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            })
            .Build();
        }

    }
}
