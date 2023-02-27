﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Mihcelle.Hwavmvid.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Client
{
    public class Applicationprovider
    {

        public NavigationManager _navigationmanager { get; set; }

        public event Action _oncontextpagechanged;

        public AuthenticationState? _contextauth { get; set; }
        public Applicationsite _contextsite { get; set; }

        private Applicationpage contextpage { get; set; }
        public Applicationpage _contextpage 
        {
            get
            {
                return this.contextpage;
            }
            set
            {
                this.contextpage = value;
                this._oncontextpagechanged?.Invoke();
            }
        }
        public Applicationcontainer _contextcontainer { get; set; }


        public HubConnection? _connection { get; set; }

        private const string unauthorizeduser = "unauthorizeduser";

        private string _applicationid = Guid.NewGuid().ToString();

        public Applicationprovider(NavigationManager navigationmanager)
        {
            _navigationmanager = navigationmanager;
        }
        public async Task<bool> Establishapplicationconnection()
        {
            try
            {

                if (_connection?.State == HubConnectionState.Disconnected
                 || _connection?.State == HubConnectionState.Connecting
                 || _connection?.State == HubConnectionState.Reconnecting)
                {
                    Console.WriteLine("User not connected..");
                }

                _connection = Buildapplicationhubconnection();
                await _connection.StartAsync().ContinueWith(async task =>
                {
                    if (task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.Faulted)
                    {

                        await _connection.SendAsync("Establishapplicationconnection").ContinueWith((task) =>
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
            var hubconnectionuri = _navigationmanager.BaseUri + "api/applicationhub";

            urlBuilder.Append(hubconnectionuri);
            urlBuilder.Append("?username=" + unauthorizeduser);

            var url = urlBuilder.ToString();
            return new HubConnectionBuilder().WithUrl(url, options =>
            {
                //options.Cookies.Add(this.IdentityCookie);
                options.Headers.Add("applicationid", _applicationid);
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
