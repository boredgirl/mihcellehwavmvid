﻿using Microsoft.AspNetCore.SignalR;
using System.Composition;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Shared.Constants;
using System;
using Oqtane.ChatHubs.Models;
using Oqtane.ChatHubs.Enums;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Repository;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("past", "[message]", new string[] { Authentication.Anonymousrole, Authentication.Userrole, Authentication.Anonymousrole } , "Usage: /past")]
    public class PastCommand : BaseCommand
    {
        public override async Task Execute(CommandServicesContext context, CommandCallerContext callerContext, string[] args, ChatHubUser caller)
        {

            if (args.Length == 0)
            {
                await context.ChatHub.SendClientNotification("No arguments found.", callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);
                return;
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
            chatHubMessageClientModel.ModifiedOn = chatHubMessageClientModel.ModifiedOn.AddSeconds(-10);

            var connectionsIds = context.ChatHubService.GetAllExceptConnectionIds(caller);
            await context.ChatHub.Clients.GroupExcept(callerContext.RoomId.ToString(), connectionsIds).SendAsync("AddMessage", chatHubMessageClientModel);

        }
    }
}