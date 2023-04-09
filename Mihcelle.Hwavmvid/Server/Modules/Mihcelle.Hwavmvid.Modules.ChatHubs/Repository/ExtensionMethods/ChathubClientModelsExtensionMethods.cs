﻿using Oqtane.ChatHubs.Enums;
using Oqtane.ChatHubs.Models;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Repository
{
    public static class ChathubClientModelsExtensionMethods
    {
        public static ChatHubRoom ClientModel(this ChatHubRoom obj)
        {
            ChatHubRoom room = new ChatHubRoom()
            {
                Id = obj.Id,
                ModuleId = obj.ModuleId,
                CreatorId = obj.CreatorId,
                OneVsOneId = obj.OneVsOneId,
                Status = obj.Status,
                Content = obj.Content,
                ImageUrl = obj.ImageUrl,
                BackgroundColor = obj.BackgroundColor,
                Title = obj.Title,
                Type = obj.Type,
                SnapshotUrl = obj.SnapshotUrl,
                CreatedBy = obj.CreatedBy,
                CreatedOn = obj.CreatedOn,
                ModifiedBy = obj.ModifiedBy,
                ModifiedOn = obj.ModifiedOn,
            };

            return room;
        }
        public static ChatHubUser ClientModel(this ChatHubUser obj)
        {
            ChatHubUser user = new ChatHubUser()
            {
                Id = obj.Id,
                UserName = obj.UserName,
                DisplayName = obj.DisplayName,
                Email = obj.Email,
                UserType = obj.UserType,
                CreatedBy = obj.CreatedBy,
                CreatedOn = obj.CreatedOn,
                ModifiedBy = obj.ModifiedBy,
                ModifiedOn = obj.ModifiedOn,
                FrameworkUserId = obj.Id,
                PasswordHash = "guest",
                EmailConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
            };

            return user;
        }
        public static ChatHubConnection ClientModel(this ChatHubConnection obj)
        {
            ChatHubConnection connection = new ChatHubConnection()
            {
                Id = obj.Id,
                ConnectionId = obj.ConnectionId,
                ChatHubUserId = obj.ChatHubUserId,
                Status = obj.Status,
                IpAddress = obj.IpAddress,
                UserAgent = obj.UserAgent,
                CreatedBy = obj.CreatedBy,
                CreatedOn = obj.CreatedOn,
                ModifiedBy = obj.ModifiedBy,
                ModifiedOn = obj.ModifiedOn,
            };

            return connection;
        }
        public static ChatHubCam ClientModel (this ChatHubCam obj) {

            ChatHubCam cam = new ChatHubCam()
            {
                Id = obj.Id,
                Status = obj.Status,
                ChatHubConnectionId = obj.ChatHubConnectionId,
                ChatHubRoomId = obj.ChatHubRoomId,
                TotalVideoSequences = obj.TotalVideoSequences,
                VideoUrl = obj.VideoUrl,
                VideoUrlExtension = obj.VideoUrlExtension,
                CreatedBy = obj.CreatedBy,
                CreatedOn = obj.CreatedOn,
                ModifiedBy = obj.ModifiedBy,
                ModifiedOn = obj.ModifiedOn,
            };

            return cam;
        }
        public static ChatHubSettings ClientModel(this ChatHubSettings obj)
        {

            ChatHubSettings settings = new ChatHubSettings()
            {
                Id = obj.Id,
                ChatHubUserId = obj.ChatHubUserId,
                MessageColor = obj.MessageColor,
                UsernameColor = obj.UsernameColor,
                CreatedBy = obj.CreatedBy,
                CreatedOn = obj.CreatedOn,
                ModifiedBy = obj.ModifiedBy,
                ModifiedOn = obj.ModifiedOn,
            };

            return settings;
        }
        public static ChatHubMessage ClientModel(this ChatHubMessage obj)
        {
            ChatHubMessage message = new ChatHubMessage()
            {
                Id = obj.Id,
                ChatHubUserId = obj.ChatHubUserId,
                ChatHubRoomId = obj.ChatHubRoomId,
                Type = obj.Type,
                Content = obj.Content,                
                CreatedBy = obj.CreatedBy,
                CreatedOn = obj.CreatedOn,
                ModifiedBy = obj.ModifiedBy,
                ModifiedOn = obj.ModifiedOn,
            };

            return message;
        }
        public static ChatHubIgnore ClientModel(this ChatHubIgnore obj)
        {
            ChatHubIgnore ignore = new ChatHubIgnore()
            {
                Id = obj.Id,
                ChatHubUserId = obj.ChatHubUserId,
                ChatHubIgnoredUserId = obj.ChatHubIgnoredUserId,
                CreatedBy = obj.CreatedBy,
                CreatedOn = obj.CreatedOn,
                ModifiedBy = obj.ModifiedBy,
                ModifiedOn = obj.ModifiedOn,
            };

            return ignore;
        }

        public static ChatHubDevice ClientModel(this ChatHubDevice obj)
        {
            ChatHubDevice device = new ChatHubDevice()
            {
                Id = obj.Id,
                ChatHubUserId = obj.ChatHubUserId,
                UserAgent = obj.UserAgent,
                Type = obj.Type,
                DefaultDeviceId = obj.DefaultDeviceId,
                DefaultDeviceName = obj.DefaultDeviceName,
                CreatedBy = obj.CreatedBy,
                CreatedOn = obj.CreatedOn,
                ModifiedBy = obj.ModifiedBy,
                ModifiedOn = obj.ModifiedOn,
            };

            return device;
        }

        public static ChatHubGeolocation ClientModel(this ChatHubGeolocation obj)
        {
            ChatHubGeolocation device = new ChatHubGeolocation()
            {
                Id = obj.Id,
                ChatHubConnectionId = obj.ChatHubConnectionId,
                state = obj.state,
                latitude = obj.latitude,
                longitude = obj.longitude,
                altitude = obj.altitude,
                altitudeaccuracy = obj.altitudeaccuracy,
                accuracy = obj.accuracy,
                heading = obj.heading,
                speed = obj.speed,
                CreatedBy = obj.CreatedBy,
                CreatedOn = obj.CreatedOn,
                ModifiedBy = obj.ModifiedBy,
                ModifiedOn = obj.ModifiedOn,
            };

            return device;
        }
    }
}
