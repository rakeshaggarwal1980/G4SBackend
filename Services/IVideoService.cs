using System.Collections.Generic;
using System.Threading.Tasks;
using videoApp.VideoChat.Models;

namespace videoApp.VideoChat.Services
{
    public interface IVideoService
    {
        string GetTwilioJwt(string identity);

        Task<IEnumerable<RoomDetails>> GetAllRoomsAsync();
    }
}