﻿using Microsoft.AspNetCore.SignalR;
using System.Composition;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Shared.Constants;
using System;
using Oqtane.ChatHubs.Enums;
using Oqtane.ChatHubs.Models;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Repository;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("shuffle", "[message]", new string[] { Authentication.Anonymousrole, Authentication.Userrole, Authentication.Anonymousrole } , "Usage: /shuffle")]
    public class ShuffleCommand : BaseCommand
    {
        public override async Task Execute(CommandServicesContext context, CommandCallerContext callerContext, string[] args, ChatHubUser caller)
        {

            if (args.Length == 0)
            {
                await context.ChatHub.SendClientNotification("No arguments found.", callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);
                return;
            }

            for(var i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Shuffle();
            }

            string msg = String.Join(" ", args).Trim();

            ChatHubMessage chatHubMessage = new ChatHubMessage()
            {
                ChatHubRoomId = callerContext.RoomId,
                ChatHubUserId = caller.Id,
                Content = msg,
                Type = ChatHubMessageType.Guest.ToString()
            };
            await context.ChatHubRepository.AddMessage(chatHubMessage);
            ChatHubMessage chatHubMessageClientModel = context.ChatHubService.CreateChatHubMessageClientModel(chatHubMessage, caller);

            var connectionsIds = context.ChatHubService.GetAllExceptConnectionIds(caller);
            await context.ChatHub.Clients.GroupExcept(callerContext.RoomId.ToString(), connectionsIds).SendAsync("AddMessage", chatHubMessageClientModel);

        }
    }

    public static class ShuffleExtension
    {
        public static string Shuffle(this string str)
        {
            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }
    }

}