﻿using Oqtane.ChatHubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oqtane.ChatHubs.Extensions
{
    public static class ChatHubServiceExtensionMethods
    {

        public static void AddRoom(this List<ChatHubRoom> rooms, ChatHubRoom room)
        {
            if (!rooms.Any(x => x.Id == room.Id))
            {
                rooms.Add(room);
            }
        }
        public static void RemoveRoom(this List<ChatHubRoom> rooms, ChatHubRoom room)
        {
            var chatRoom = rooms.First(x => x.Id == room.Id);
            if (chatRoom != null)
            {
                rooms.Remove(chatRoom);
            }
        }
        public static void AddMessage(this ChatHubRoom room, ChatHubMessage message)
        {
            if (!room.Messages.Any(x => x.Id == message.Id))
            {
                room.Messages.Add(message);
            }
        }
        public static void AddUser(this List<ChatHubRoom> rooms, ChatHubUser user, string roomId)
        {
            var room = rooms.FirstOrDefault(x => x.Id.ToString() == roomId);
            if (room != null && !room.Users.Any(x => x.Id == user.Id))
            {
                room.Users.Add(user);
            }
        }
        public static void RemoveUser(this List<ChatHubRoom> rooms, ChatHubUser user, string roomId)
        {
            var room = rooms.FirstOrDefault(x => x.Id.ToString() == roomId);
            if (room != null)
            {
                var userItem = room.Users.FirstOrDefault(x => x.Id == user.Id);
                if (userItem != null)
                {
                    room.Users.Remove(userItem);
                }
            }
        }
        public static void AddInvitation(this List<ChatHubInvitation> invitations, ChatHubInvitation invitation)
        {
            if (!invitations.Any(item => item.Id == invitation.Id))
            {
                invitations.Add(invitation);
            }
        }
        public static void RemoveInvitation(this List<ChatHubInvitation> invitations, string id)
        {
            var item = invitations.First(item => item.Id == id);
            if (item != null)
            {
                invitations.Remove(item);
            }
        }
        public static void AddWaitingRoomItem(this List<ChatHubWaitingRoomItem> waitingRoomItems, ChatHubWaitingRoomItem waitingRoomItem)
        {
            if (!waitingRoomItems.Any(item => item.Guid == waitingRoomItem.Guid))
            {
                waitingRoomItems.Add(waitingRoomItem);
            }
        }
        public static void RemoveWaitingRoomItem(this List<ChatHubWaitingRoomItem> waitingRoomItems, Guid guid)
        {
            var item = waitingRoomItems.First(item => item.Guid == guid);
            if (item != null)
            {
                waitingRoomItems.Remove(item);
            }
        }
        public static void AddIgnoredUser(this List<ChatHubUser> ignoredUsers, ChatHubUser user)
        {
            if (!ignoredUsers.Any(x => x.Id == user.Id))
            {
                ignoredUsers.Add(user);
            }
        }
        public static void RemoveIgnoredUser(this List<ChatHubUser> ignoredUsers, ChatHubUser user)
        {
            var item = ignoredUsers.FirstOrDefault(x => x.Id == user.Id);
            if (item != null)
            {
                ignoredUsers.Remove(item);
            }
        }
        public static void AddIgnoredByUser(this List<ChatHubUser> ignoredByUsers, ChatHubUser user)
        {
            if (!ignoredByUsers.Any(x => x.Id == user.Id))
            {
                ignoredByUsers.Add(user);
            }
        }
        public static void RemoveIgnoredByUser(this List<ChatHubUser> ignoredByUsers, ChatHubUser user)
        {
            var item = ignoredByUsers.FirstOrDefault(x => x.Id == user.Id);
            if (item != null)
            {
                ignoredByUsers.Remove(item);
            }
        }
        public static void AddModerator(this List<ChatHubRoom> rooms, ChatHubModerator moderator, string roomId)
        {
            var room = rooms.FirstOrDefault(item => item.Id == roomId);
            if (room != null && !room.Moderators.Any(item => item.Id == moderator.Id))
            {
                room.Moderators.Add(moderator);
            }
        }
        public static void RemoveModerator(this List<ChatHubRoom> rooms, ChatHubModerator moderator, string roomId)
        {
            var room = rooms.FirstOrDefault(item => item.Id == roomId);
            if (room != null)
            {
                var modi = room.Moderators.FirstOrDefault(item => item.Id == moderator.Id);
                if (modi != null)
                {
                    room.Moderators.Remove(modi);
                }
            }
        }
        public static void AddWhitelistUser(this List<ChatHubRoom> rooms, ChatHubWhitelistUser whitelistUser, string roomId)
        {
            var room = rooms.FirstOrDefault(item => item.Id == roomId);
            if (room != null && !room.WhitelistUsers.Any(item => item.Id == whitelistUser.Id))
            {
                room.WhitelistUsers.Add(whitelistUser);
            }
        }
        public static void RemoveWhitelistUser(this List<ChatHubRoom> rooms, ChatHubWhitelistUser whitelistUser, string roomId)
        {
            var room = rooms.FirstOrDefault(item => item.Id == roomId);
            if (room != null)
            {
                var user = room.WhitelistUsers.FirstOrDefault(item => item.Id == whitelistUser.Id);
                if (user != null)
                {
                    room.WhitelistUsers.Remove(user);
                }
            }
        }
        public static void AddBlacklistUser(this List<ChatHubRoom> rooms, ChatHubBlacklistUser blacklistUser, string roomId)
        {
            var room = rooms.FirstOrDefault(item => item.Id == roomId);
            if (room != null && !room.BlacklistUsers.Any(item => item.Id == blacklistUser.Id))
            {
                room.BlacklistUsers.Add(blacklistUser);
            }
        }
        public static void RemoveBlacklistUser(this List<ChatHubRoom> rooms, ChatHubBlacklistUser blacklistUser, string roomId)
        {
            var room = rooms.FirstOrDefault(item => item.Id == roomId);
            if (room != null)
            {
                var user = room.BlacklistUsers.FirstOrDefault(item => item.Id == blacklistUser.Id);
                if (user != null)
                {
                    room.BlacklistUsers.Remove(user);
                }
            }
        }
        public static void AddCam(this List<ChatHubRoom> rooms, ChatHubCam cam, string roomId)
        {
            var room = rooms.FirstOrDefault(item => item.Id == roomId);
            if (room != null && !room.Cams.Any(item => item.Id == cam.Id))
            {
                room.Cams.Add(cam);
            }
        }
        public static void RemoveCam(this List<ChatHubRoom> rooms, ChatHubCam cam, string roomId)
        {
            var room = rooms.FirstOrDefault(item => item.Id == roomId);
            if (room != null)
            {
                var user = room.Cams.FirstOrDefault(item => item.Id == cam.Id);
                if (user != null)
                {
                    room.Cams.Remove(user);
                }
            }
        }

    }
}
