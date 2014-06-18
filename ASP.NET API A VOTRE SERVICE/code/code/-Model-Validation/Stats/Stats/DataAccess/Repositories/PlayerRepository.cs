using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stats.DataAccess.Entities;

namespace Stats.DataAccess.Repositories
{
    public class PlayerRepository :Repository<Player> {
        public PlayerRepository( StatsDbContext context ) : base( context ) {}
    }
}