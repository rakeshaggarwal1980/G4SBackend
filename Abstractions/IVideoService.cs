using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoChat.Models;

namespace VideoChat.Abstractions
{
    public interface IVideoService
    {
        string GetTwilioJwt(string identity);
        Task<IEnumerable<RoomDetails>> GetAllRoomsAsync();
    }
}
