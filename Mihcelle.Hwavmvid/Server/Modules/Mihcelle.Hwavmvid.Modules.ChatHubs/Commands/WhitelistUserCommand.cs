﻿using System.Composition;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Shared.Constants;
using Oqtane.ChatHubs.Models;
using Oqtane.ChatHubs.Enums;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    [Export("ICommand", typeof(ICommand))]
    [Command("whitelistuser", "[username]", new string[] { Authentication.Anonymousrole, Authentication.Userrole, Authentication.Anonymousrole }, "Usage: /whitelistuser")]
    public class WhitelistUserCommand : BaseCommand
    {
        public override async Task Execute(CommandServicesContext context, CommandCallerContext callerContext, string[] args, ChatHubUser caller)
        {

            if (args.Length == 0)
            {
                await context.ChatHub.SendClientNotification("No arguments found.", callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);
                return;
            }

            string targetUserName = args[0];

            ChatHubUser targetUser = await context.ChatHubRepository.GetUserByDisplayNameAsync(targetUserName);
            targetUser = targetUser == null ? await context.ChatHubRepository.GetUserByUserNameAsync(targetUserName) : targetUser;
            if (targetUser == null)
            {
                await context.ChatHub.SendClientNotification("No user found.", callerContext.RoomId, callerContext.ConnectionId, caller, ChatHubMessageType.System);
                return;
            }

            await context.ChatHub.AddWhitelistUser(targetUser.Id, callerContext.RoomId);
        }
    }
}