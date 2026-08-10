using Cafe_POS.Models;
using Cafe_POS.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cafe_POS.Repositories;

public class ServerRepository : IServerRepository
{
    private CafeContext _dbContext;
    private readonly AppConfig _appConfig;
    public ServerRepository(AppConfig config)
    {
        // Get the Connection string out of the AppConfiguration object
        // and put in the context
        _appConfig = config;
        _dbContext = new CafeContext(_appConfig.ConnectionString);
    }
    public IEnumerable<Server> GetActiveServers()
    {
        return _dbContext.Servers
            .Where(s => s.TermDate == null)
            .ToList();
    }
    public Server GetServerById(int serverId)
    {
        return _dbContext.Servers
            .FirstOrDefault(s => s.ServerId == serverId);
    }
}