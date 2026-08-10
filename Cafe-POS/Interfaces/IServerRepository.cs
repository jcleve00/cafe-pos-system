using System.Net.Security;
using Cafe_POS.Models;

namespace Cafe_POS.Interfaces;

public interface IServerRepository
{
    IEnumerable<Server> GetActiveServers();
    Server GetServerById(int serverId);
}