﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Oqtane.ChatHubs.Models;
using Microsoft.Extensions.Caching.Memory;
using Mihcelle.Hwavmvid.Shared.Models;
using Microsoft.VisualBasic;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Repository
{
    public class ChatHubRepository
    {

        private readonly Applicationdbcontext _db;
        private readonly string key_room_viewer_prefix = "room_viewers_";

        private IMemoryCache _cache { get; set; }

        public ChatHubRepository(Applicationdbcontext dbContext, IMemoryCache memoryCache)
        {
            this._db = dbContext;
            this._cache = memoryCache;
        }

        #region GET

        public IQueryable<ChatHubRoom> Rooms()
        {
            return this._db.ChatHubRoom;
        }
        public IQueryable<ChatHubUser> Users()
        {
            return this._db.ChatHubUser;
        }
        public IQueryable<ChatHubConnection> Connections()
        {
            return this._db.ChatHubConnection;
        }
        public IQueryable<ChatHubMessage> Messages()
        {
            return this._db.ChatHubMessage;
        }
        public IQueryable<ChatHubCam> Cams()
        {
            return this._db.ChatHubCam;
        }
        public IQueryable<ChatHubCamSequence> CamSequences()
        {
            return this._db.ChatHubCamSequence;
        }
        public IQueryable<ChatHubInvitation> Invitations()
        {
            return this._db.ChatHubInvitation;
        }
        public IQueryable<ChatHubIgnore> Ignores()
        {
            return this._db.ChatHubIgnore;
        }
        public IQueryable<ChatHubModerator> Moderators()
        {
            return this._db.ChatHubModerator;
        }
        public IQueryable<ChatHubDevice> Devices()
        {
            return this._db.ChatHubDevice;
        }
        public IQueryable<ChatHubRoomChatHubModerator> ChatHubRoomChatHubModerators()
        {
            return this._db.ChatHubRoomChatHubModerator;
        }

        public IQueryable<ChatHubRoom> GetRoomsByUser(ChatHubUser user)
        {
            return this._db.Entry(user)
                      .Collection(u => u.UserRooms)
                      .Query().Select(u => u.Room);
        }
        public IQueryable<ChatHubRoom> GetRoomsByCreator(string creatorId)
        {
            return this._db.ChatHubRoom.Where(item => item.CreatorId == creatorId);
        }
        public IQueryable<ChatHubUser> GetUsersByRoom(ChatHubRoom room)
        {
            return this._db.Entry(room)
                      .Collection(r => r.RoomUsers)
                      .Query().Select(r => r.User);
        }
        public IQueryable<ChatHubUser> GetUsersByRoomId(string roomId)
        {
            return this._db.ChatHubUser.Include(item => item.UserRooms).Where(item => item.UserRooms.Any(item => item.ChatHubRoomId == roomId));
        }
        public async Task<ChatHubRoom> GetRoomById(string roomId)
        {
            return await this._db.ChatHubRoom.FirstOrDefaultAsync(room => room.Id == roomId);
        }
        public ChatHubRoom GetRoomOneVsOne(string OneVsOneId)
        {
            return this._db.ChatHubRoom.FirstOrDefault(item => item.OneVsOneId == OneVsOneId);
        }
        public IQueryable<ChatHubConnection> GetConnectionsByUserId(string userId)
        {
            return this._db.ChatHubConnection.Where(item => item.ChatHubUserId == userId);
        }
        public ChatHubRoomChatHubUser GetRoomUser(string chatHubRoomId, string chatHubUserId)
        {
            return this._db.ChatHubRoomChatHubUser
                    .Where(item => item.ChatHubRoomId == chatHubRoomId)
                    .Where(item => item.ChatHubUserId == chatHubUserId)
                    .FirstOrDefault();
        }
        public List<ChatHubRoomChatHubUser> GetRoomUser(string chatHubRoomId)
        {
            return this._db.ChatHubRoomChatHubUser.Where(item => item.ChatHubRoomId == chatHubRoomId).ToList();
        }
        public ChatHubIgnore GetIgnore(string callerUserId, string targetUserId)
        {
            return this._db.ChatHubIgnore.FirstOrDefault(item => item.ChatHubUserId == callerUserId && item.ChatHubIgnoredUserId == targetUserId);
        }
        public IQueryable<ChatHubIgnore> GetIgnoredUsers(ChatHubUser user)
        {
            return this._db.Entry(user)
                      .Collection(u => u.Ignores)
                      .Query().Select(i => i);
        }
        public IQueryable<ChatHubUser> GetIgnoredApplicationUsers(ChatHubUser user)
        {
            IQueryable<string> chatIgnoreIdList = this._db.Entry(user)
                      .Collection(u => u.Ignores)
                      .Query()
                      .Select(i => i.ChatHubIgnoredUserId);

            return this._db.ChatHubUser.Where(u => chatIgnoreIdList.Contains(u.Id)).Include(u => u.Connections);
        }
        public IQueryable<ChatHubUser> GetIgnoredByApplicationUsers(ChatHubUser user)
        {
            IQueryable<string> chatIgnoredByIdList = this._db.ChatHubIgnore
                      .Include(i => i.User)
                      .Where(i => i.ChatHubIgnoredUserId == user.Id && (i.ModifiedOn.AddDays(7)) >= DateTime.Now)
                      .Select(i => i.ChatHubUserId);

            return this._db.ChatHubUser.Where(u => chatIgnoredByIdList.Contains(u.Id)).Include(u => u.Connections);
        }
        public IQueryable<ChatHubIgnore> GetIgnoredByUsers(ChatHubUser user)
        {
            return this._db.ChatHubIgnore.Include(i => i.User).Select(i => i).Where(i => i.Id == user.Id && (i.ModifiedOn.AddDays(7)) >= DateTime.Now);
        }
        public ChatHubSettings GetSetting(string ChatHubUserId)
        {
            return this._db.ChatHubSetting.Include(item => item.User).Where(item => item.ChatHubUserId == ChatHubUserId).FirstOrDefault();
        }
        public ChatHubSettings GetSettingByUser(ChatHubUser user)
        {
            return this._db.ChatHubSetting.Where(item => item.ChatHubUserId == user.Id).FirstOrDefault();
        }
        public async Task<ChatHubUser> GetUserByIdAsync(string id)
        {
            return await this._db.ChatHubUser.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<ChatHubUser> GetUserByUserNameAsync(string username)
        {
            return await this._db.ChatHubUser
                            .Where(u => u.UserName == Oqtane.ChatHubs.Constants.ChatHubConstants.ChatHubUserPrefix + username)
                            .FirstOrDefaultAsync();
        }
        public async Task<ChatHubUser> GetUserByDisplayNameAsync(string username)
        {
            return await this._db.ChatHubUser
                            .Where(u => u.DisplayName == username)
                            .FirstOrDefaultAsync();
        }
        public ChatHubModerator GetModerator(string ChatHubUserId)
        {
            return this._db.ChatHubModerator.FirstOrDefault(item => item.ChatHubUserId == ChatHubUserId);
        }
        public IQueryable<ChatHubModerator> GetModerators(ChatHubRoom ChatHubRoom)
        {
            try
            {
                return this._db.Entry(ChatHubRoom)
                      .Collection(item => item.RoomModerators)
                      .Query().Select(item => item.Moderator);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public ChatHubRoomChatHubModerator GetRoomModerator(string chatHubRoomId, string chatHubModeratorId)
        {
            return this._db.ChatHubRoomChatHubModerator
                    .Where(item => item.ChatHubRoomId == chatHubRoomId)
                    .Where(item => item.ChatHubModeratorId == chatHubModeratorId)
                    .FirstOrDefault();
        }
        public ChatHubWhitelistUser GetWhitelistUser(string ChatHubUserId)
        {
            return this._db.ChatHubWhitelistUser.FirstOrDefault(item => item.ChatHubUserId == ChatHubUserId);
        }
        public IQueryable<ChatHubWhitelistUser> GetWhitelistUsers(ChatHubRoom ChatHubRoom)
        {
            try
            {
                return this._db.Entry(ChatHubRoom)
                      .Collection(item => item.RoomWhitelistUsers)
                      .Query().Select(item => item.WhitelistUser);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public ChatHubRoomChatHubWhitelistUser GetRoomWhitelistUser(string chatHubRoomId, string chatHubWhitelistUserId)
        {
            return this._db.ChatHubRoomChatHubWhitelistUser
                    .Where(item => item.ChatHubRoomId == chatHubRoomId)
                    .Where(item => item.ChatHubWhitelistUserId == chatHubWhitelistUserId)
                    .FirstOrDefault();
        }
        public ChatHubBlacklistUser GetBlacklistUser(string ChatHubUserId)
        {
            return this._db.ChatHubBlacklistUser.FirstOrDefault(item => item.ChatHubUserId == ChatHubUserId);
        }
        public IQueryable<ChatHubBlacklistUser> GetBlacklistUsers(ChatHubRoom ChatHubRoom)
        {
            try
            {
                return this._db.Entry(ChatHubRoom)
                      .Collection(item => item.RoomBlacklistUsers)
                      .Query().Select(item => item.BlacklistUser);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public ChatHubRoomChatHubBlacklistUser GetRoomBlacklistUser(string chatHubRoomId, string chatHubBlacklistUserId)
        {
            return this._db.ChatHubRoomChatHubBlacklistUser
                    .Where(item => item.ChatHubRoomId == chatHubRoomId)
                    .Where(item => item.ChatHubBlacklistUserId == chatHubBlacklistUserId)
                    .FirstOrDefault();
        }
        public async Task<ChatHubCam> GetCamById(string ChatHubCamId)
        {
            return await this._db.ChatHubCam.FirstOrDefaultAsync(item => item.Id == ChatHubCamId);
        }
        public async Task<ChatHubCam> GetCam(string roomId, string ChatHubConnectionId)
        {
            return await this._db.ChatHubCam.FirstOrDefaultAsync(item => item.ChatHubRoomId == roomId && item.ChatHubConnectionId == ChatHubConnectionId);
        }
        public IQueryable<ChatHubCam> GetCamsByRoomId(string roomId)
        {
            return this._db.ChatHubCam.Where(item => item.ChatHubRoomId == roomId);
        }
        public IQueryable<ChatHubCam> GetCamsByConnectionId(string connectionId)
        {
            return this._db.ChatHubCam.Where(item => item.ChatHubConnectionId == connectionId);
        }
        public async Task<IList<ChatHubViewer>> GetViewersByRoomIdAsync(string roomId)
        {
            var key = this.key_room_viewer_prefix + roomId;
            IList<ChatHubViewer> cachedViewersList = new List<ChatHubViewer>();
            if (!this._cache.TryGetValue<IList<ChatHubViewer>>(key, out cachedViewersList))
            {
                var streamingCamsConnectionIds = await this.GetCamsByRoomId(roomId).Streaming().Select(cam => cam.ChatHubConnectionId).ToListAsync();
                var streamingUsers = await this._db.ChatHubUser.Include(user => user.Connections).Where(user => user.Connections.Any(connection => streamingCamsConnectionIds.Contains(connection.Id))).ToListAsync();
                var viewers = streamingUsers.Select(user => new ChatHubViewer() { UserId = user.Id, RoomId = roomId, Username = user.UserName }).ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10));
                cachedViewersList = this._cache.Set<IList<ChatHubViewer>>(key, viewers, cacheEntryOptions);
            }

            return cachedViewersList;
        }

        #endregion

        #region ADD

        public async Task<ChatHubRoom> AddRoom(ChatHubRoom ChatHubRoom)
        {
            await this._db.ChatHubRoom.AddAsync(ChatHubRoom);
            this._db.SaveChanges();
            return ChatHubRoom;
        }
        public async Task<ChatHubMessage> AddMessage(ChatHubMessage ChatHubMessage)
        {
            this._db.ChatHubMessage.Add(ChatHubMessage);
            await this._db.SaveChangesAsync();
            return ChatHubMessage;
        }
        public async Task AddConnection(ChatHubConnection ChatHubConnection)
        {
            await this._db.ChatHubConnection.AddAsync(ChatHubConnection);
            await this._db.SaveChangesAsync();            
        }
        public ChatHubUser AddUser(ChatHubUser ChatHubUser)
        {
            this._db.ChatHubUser.Add(ChatHubUser);
            this._db.SaveChanges();            
            return ChatHubUser;
        }
        public ChatHubRoomChatHubUser AddRoomUser(ChatHubRoomChatHubUser ChatHubRoomChatHubUser)
        {
            var item = this.GetRoomUser(ChatHubRoomChatHubUser.ChatHubRoomId, ChatHubRoomChatHubUser.ChatHubUserId);
            if (item == null)
            {
                this._db.ChatHubRoomChatHubUser.Add(ChatHubRoomChatHubUser);
                this._db.SaveChanges();
                return ChatHubRoomChatHubUser;
            }

            return item;
        }
        public ChatHubPhoto AddPhoto(ChatHubPhoto ChatHubPhoto)
        {
            try
            {
                this._db.ChatHubPhoto.Add(ChatHubPhoto);
                this._db.SaveChanges();
                return ChatHubPhoto;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public ChatHubIgnore AddIgnore(ChatHubIgnore chatHubIgnore)
        {
            try
            {
                this._db.ChatHubIgnore.Add(chatHubIgnore);
                this._db.SaveChanges();
                return chatHubIgnore;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public ChatHubSettings AddSetting(ChatHubSettings ChatHubSetting)
        {
            if (ChatHubSetting != null)
            {
                this._db.ChatHubSetting.Add(ChatHubSetting);
                this._db.SaveChanges();
            }
            return ChatHubSetting;
        }
        public ChatHubInvitation AddInvitation(ChatHubInvitation ChatHubInvitation)
        {
            if (ChatHubInvitation != null)
            {
                this._db.ChatHubInvitation.Add(ChatHubInvitation);
                this._db.SaveChanges();
            }
            return ChatHubInvitation;
        }
        public ChatHubModerator AddModerator(ChatHubModerator ChatHubModerator)
        {
            if (ChatHubModerator != null)
            {
                this._db.ChatHubModerator.Add(ChatHubModerator);
                this._db.SaveChanges();
            }
            return ChatHubModerator;
        }
        public ChatHubRoomChatHubModerator AddRoomModerator(ChatHubRoomChatHubModerator ChatHubRoomChatHubModerator)
        {
            var item = this.GetRoomModerator(ChatHubRoomChatHubModerator.ChatHubRoomId, ChatHubRoomChatHubModerator.ChatHubModeratorId);
            if (item == null)
            {
                this._db.ChatHubRoomChatHubModerator.Add(ChatHubRoomChatHubModerator);
                this._db.SaveChanges();
                return ChatHubRoomChatHubModerator;
            }

            return item;
        }
        public ChatHubWhitelistUser AddWhitelistUser(ChatHubUser targetUser)
        {
            try
            {
                ChatHubWhitelistUser ChatHubWhitelistUser = this.GetWhitelistUser(targetUser.Id);
                if (ChatHubWhitelistUser == null)
                {
                    ChatHubWhitelistUser = new ChatHubWhitelistUser()
                    {
                        ChatHubUserId = targetUser.Id,
                        WhitelistUserDisplayName = targetUser.DisplayName,
                    };

                    this._db.ChatHubWhitelistUser.Add(ChatHubWhitelistUser);
                    this._db.SaveChanges();
                }
                return ChatHubWhitelistUser;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public ChatHubRoomChatHubWhitelistUser AddRoomWhitelistUser(ChatHubRoomChatHubWhitelistUser ChatHubRoomChatHubWhitelistUser)
        {
            var item = this.GetRoomWhitelistUser(ChatHubRoomChatHubWhitelistUser.ChatHubRoomId, ChatHubRoomChatHubWhitelistUser.ChatHubWhitelistUserId);
            if (item == null)
            {
                this._db.ChatHubRoomChatHubWhitelistUser.Add(ChatHubRoomChatHubWhitelistUser);
                this._db.SaveChanges();
                return ChatHubRoomChatHubWhitelistUser;
            }

            return item;
        }
        public ChatHubBlacklistUser AddBlacklistUser(ChatHubUser targetUser)
        {
            try
            {
                ChatHubBlacklistUser ChatHubBlacklistUser = this.GetBlacklistUser(targetUser.Id);
                if (ChatHubBlacklistUser == null)
                {
                    ChatHubBlacklistUser = new ChatHubBlacklistUser()
                    {
                        ChatHubUserId = targetUser.Id,
                        BlacklistUserDisplayName = targetUser.DisplayName,
                    };

                    this._db.ChatHubBlacklistUser.Add(ChatHubBlacklistUser);
                    this._db.SaveChanges();
                }
                return ChatHubBlacklistUser;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public ChatHubRoomChatHubBlacklistUser AddRoomBlacklistUser(ChatHubRoomChatHubBlacklistUser ChatHubRoomChatHubBlacklistUser)
        {
            var item = this.GetRoomBlacklistUser(ChatHubRoomChatHubBlacklistUser.ChatHubRoomId, ChatHubRoomChatHubBlacklistUser.ChatHubBlacklistUserId);
            if (item == null)
            {
                this._db.ChatHubRoomChatHubBlacklistUser.Add(ChatHubRoomChatHubBlacklistUser);
                this._db.SaveChanges();
                return ChatHubRoomChatHubBlacklistUser;
            }

            return item;
        }
        public async Task<ChatHubCam> AddCam(ChatHubCam ChatHubCam)
        {
            this._db.ChatHubCam.Add(ChatHubCam);
            await this._db.SaveChangesAsync();
            return ChatHubCam;
        }
        public async Task<ChatHubCamSequence> AddCamSequence(ChatHubCamSequence ChatHubCamSequence)
        {
            if (ChatHubCamSequence != null)
            {
                this._db.ChatHubCamSequence.Add(ChatHubCamSequence);
                await this._db.SaveChangesAsync();
            }
            return ChatHubCamSequence;
        }
        public ChatHubGeolocation AddGeolocation(ChatHubGeolocation position)
        {
            this._db.ChatHubGeolocation.Add(position);
            this._db.SaveChanges();
            return position;
        }

        #endregion

        #region ADD OR UPDATE

        public async Task<ChatHubDevice> AddOrUpdateDevice(ChatHubDevice ChatHubDevice)
        {
            bool any = this.Devices().Any(item => item.ChatHubUserId == ChatHubDevice.ChatHubUserId && item.ChatHubRoomId == ChatHubDevice.ChatHubRoomId && item.UserAgent == ChatHubDevice.UserAgent && item.Type == ChatHubDevice.Type);
            if (ChatHubDevice != null && !any)
            {
                this._db.ChatHubDevice.Add(ChatHubDevice);
                await this._db.SaveChangesAsync();
            }
            else if (ChatHubDevice != null && any)
            {
                this._db.Entry(ChatHubDevice).State = EntityState.Modified;
                await this._db.SaveChangesAsync();
            }

            return ChatHubDevice;
        }

        #endregion

        #region DELETE

        public void DeleteUser(string UserId)
        {
            ChatHubUser ChatHubUser = _db.ChatHubUser.Where(item => item.Id == UserId).FirstOrDefault();
            if (ChatHubUser != null)
            {
                this._db.ChatHubUser.Remove(ChatHubUser);
                this._db.SaveChanges();
            }
        }
        public void DeleteRoom(string ChatHubRoomId)
        {
            ChatHubRoom ChatHubRoom = _db.ChatHubRoom.Where(item => item.Id == ChatHubRoomId).FirstOrDefault();
            if (ChatHubRoom != null)
            {
                this._db.Remove(ChatHubRoom);
                this._db.SaveChanges();
            }
        }
        public void DeleteRooms(string userId, string ModuleId)
        {
            try
            {
                IQueryable<ChatHubRoom> rooms = this._db.ChatHubRoom.Where(item => item.CreatorId == userId)
                    .Where(item => item.ModuleId == ModuleId);
                this._db.ChatHubRoom.RemoveRange(rooms);
                this._db.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public void DeleteMessage(string ChatHubMessageId, string ChatHubRoomId)
        {
            try
            {
                ChatHubMessage ChatHubMessage = this._db.ChatHubMessage.Where(item => item.Id == ChatHubMessageId)
                    .Where(item => item.ChatHubRoomId == ChatHubRoomId).FirstOrDefault();
                this._db.ChatHubMessage.Remove(ChatHubMessage);
                this._db.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public void DeleteMessages(string userId)
        {
            IQueryable<ChatHubMessage> messages = this._db.ChatHubMessage.Where(item => item.ChatHubUserId == userId);
            if (messages != null)
            {
                this._db.ChatHubMessage.RemoveRange(messages);
                this._db.SaveChanges();
            }
        }
        public void DeleteConnection(string ChatHubConnectionId, string ChatHubUserId)
        {
            ChatHubConnection ChatHubConnection = this._db.ChatHubConnection.Where(item => item.Id == ChatHubConnectionId)
                .Where(item => item.ChatHubUserId == ChatHubUserId).FirstOrDefault();

            if (ChatHubConnection != null)
            {
                this._db.ChatHubConnection.Remove(ChatHubConnection);
                this._db.SaveChanges();
            }
        }
        public void DeleteConnections(string userId)
        {
            try
            {
                IQueryable<ChatHubConnection> connections = this._db.ChatHubConnection.Where(item => item.ChatHubUserId == userId);
                this._db.ChatHubConnection.RemoveRange(connections);
                this._db.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public void DeleteRoomUser(string ChatHubRoomId, string ChatHubUserId)
        {
            ChatHubRoomChatHubUser item = this.GetRoomUser(ChatHubRoomId, ChatHubUserId);
            if (item != null)
            {
                this._db.Remove(item);
                this._db.SaveChanges();
            }
        }
        public void DeleteRoomUser(string ChatHubRoomId)
        {
            List<ChatHubRoomChatHubUser> items = this.GetRoomUser(ChatHubRoomId);
            if (items != null)
            {
                this._db.ChatHubRoomChatHubUser.RemoveRange(items);
                this._db.SaveChanges();
            }
        }
        public void DeleteIgnore(string callerId, string targetId)
        {
            ChatHubIgnore chatHubIgnore = this.GetIgnore(callerId, targetId);
            if (chatHubIgnore != null)
            {
                this._db.ChatHubIgnore.Remove(chatHubIgnore);
                this._db.SaveChanges();
            }
        }
        public void DeleteModerator(string ModeratorId)
        {
            ChatHubModerator ChatHubModerator = this._db.ChatHubModerator.Where(item => item.Id == ModeratorId).FirstOrDefault();
            if (ChatHubModerator != null)
            {
                this._db.ChatHubModerator.Remove(ChatHubModerator);
                this._db.SaveChanges();
            }
        }
        public void DeleteRoomModerator(string ChatHubRoomId, string ChatHubModeratorId)
        {
            ChatHubRoomChatHubModerator item = this.GetRoomModerator(ChatHubRoomId, ChatHubModeratorId);
            if (item != null)
            {
                this._db.ChatHubRoomChatHubModerator.Remove(item);
                this._db.SaveChanges();
            }
        }
        public void DeleteWhitelistUser(string WhitelistUserId)
        {
            ChatHubWhitelistUser ChatHubWhitelistUser = this._db.ChatHubWhitelistUser.Where(item => item.Id == WhitelistUserId).FirstOrDefault();
            if (ChatHubWhitelistUser != null)
            {
                this._db.ChatHubWhitelistUser.Remove(ChatHubWhitelistUser);
                this._db.SaveChanges();
            }
        }
        public void DeleteRooWhitelistUser(string ChatHubRoomId, string ChatHubWhitelistUserId)
        {
            ChatHubRoomChatHubWhitelistUser item = this.GetRoomWhitelistUser(ChatHubRoomId, ChatHubWhitelistUserId);
            if (item != null)
            {
                this._db.ChatHubRoomChatHubWhitelistUser.Remove(item);
                this._db.SaveChanges();
            }
        }
        public void DeleteBlacklistUser(string BlacklistUserId)
        {
            ChatHubBlacklistUser ChatHubBlacklistUser = this._db.ChatHubBlacklistUser.Where(item => item.Id == BlacklistUserId).FirstOrDefault();
            if (ChatHubBlacklistUser != null)
            {
                this._db.ChatHubBlacklistUser.Remove(ChatHubBlacklistUser);
                this._db.SaveChanges();
            }
        }
        public void DeleteRoomBlacklistUser(string ChatHubRoomId, string ChatHubBlacklistUserId)
        {
            ChatHubRoomChatHubBlacklistUser item = this.GetRoomBlacklistUser(ChatHubRoomId, ChatHubBlacklistUserId);
            if (item != null)
            {
                this._db.ChatHubRoomChatHubBlacklistUser.Remove(item);
                this._db.SaveChanges();
            }
        }
        public void DeleteCam(string ChatHubCamId)
        {
            ChatHubCam ChatHubCam = _db.ChatHubCam.Where(item => item.Id == ChatHubCamId).FirstOrDefault();
            if (ChatHubCam != null)
            {
                this._db.Remove(ChatHubCam);
                this._db.SaveChanges();
            }
        }
        public async Task DeleteSequence(string ChatHubSequenceId)
        {
            ChatHubCamSequence ChatHubCamSequence = await _db.ChatHubCamSequence.Where(item => item.Id == ChatHubSequenceId).FirstOrDefaultAsync();
            if (ChatHubCamSequence != null)
            {
                this._db.Remove(ChatHubCamSequence);
                this._db.SaveChanges();
            }
        }
        public async Task DeleteInvitation(string ChatHubInvitationId)
        {
            ChatHubInvitation ChatHubInvitation = await _db.ChatHubInvitation.Where(item => item.Id == ChatHubInvitationId).FirstOrDefaultAsync();
            if (ChatHubInvitation != null)
            {
                this._db.Remove(ChatHubInvitation);
                this._db.SaveChanges();
            }
        }

        #endregion

        #region TODO: remove oqtane framework workarrounds with discriminators

        public async Task UpdateUserColumnAsync()
        {
            try
            {
                await _db.Database.ExecuteSqlRawAsync($"IF COL_LENGTH('dbo.User', 'UserType') IS NULL BEGIN ALTER TABLE [dbo].[User] ADD [UserType] [nvarchar](256) NULL END");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task UpdateUserAsync(Applicationuser user)
        {
            try
            {
                await _db.Database.ExecuteSqlRawAsync($"UPDATE [dbo].[Applicationusers] SET UserType='ChatHubUser' WHERE Id={user.Id}");
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region UPDATE

        public async Task<ChatHubRoom> UpdateRoom(ChatHubRoom obj)
        {
            this._db.Entry(obj).State = EntityState.Modified;
            await this._db.SaveChangesAsync();

            return await this._db.ChatHubRoom.FirstOrDefaultAsync(item => item.Id == obj.Id);
        }
        public async Task<ChatHubMessage> UpdateMessage(ChatHubMessage obj)
        {
            this._db.Entry(obj).State = EntityState.Modified;
            await this._db.SaveChangesAsync();

            return obj;
        }
        public async Task<ChatHubConnection> UpdateConnection(ChatHubConnection obj)
        {
            this._db.Entry(obj).State = EntityState.Modified;
            await this._db.SaveChangesAsync();

            return obj;
        }
        public async Task<ChatHubRoom> UpdateRoomStatus(ChatHubRoom obj)
        {
            this._db.Entry(obj).State = EntityState.Modified;
            await this._db.SaveChangesAsync();

            return obj;
        }
        public async Task<ChatHubIgnore> UpdateIgnore(ChatHubIgnore obj)
        {
            this._db.Entry(obj).State = EntityState.Modified;
            await this._db.SaveChangesAsync();

            return await this._db.ChatHubIgnore.FirstOrDefaultAsync(item => item.Id == obj.Id);
        }
        public async Task<ChatHubSettings> UpdateSetting(ChatHubSettings obj)
        {
            this._db.Entry(obj).State = EntityState.Modified;
            await this._db.SaveChangesAsync();

            return await this._db.ChatHubSetting.FirstOrDefaultAsync(item => item.Id == obj.Id);
        }
        public async Task<ChatHubCam> UpdateCam(ChatHubCam obj)
        {
            this._db.Entry(obj).State = EntityState.Modified;
            await this._db.SaveChangesAsync();

            return obj;
        }

        #endregion

    }
}
 