using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stats.DataAccess.Entities;
using Stats.DataAccess.Repositories;

namespace Stats.DataAccess
{
    public interface IStatsService
    {
        Repository<Game> Games { get; }
        Repository<Team> Teams { get; }
        Repository<Player> Players { get; }
        Repository<GameEvent> Events { get; } 
    }
}
