﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oqtane.ChatHubs.Models
{
    public class ChatHubModerator : ChatHubBaseModel
    {

        public string ChatHubUserId { get; set; }
        public string ModeratorDisplayName { get; set; }

        [NotMapped] public virtual ChatHubUser User { get; set; }
        [NotMapped] public virtual ICollection<ChatHubRoomChatHubModerator> ModeratorRooms { get; set; }

    }
}
