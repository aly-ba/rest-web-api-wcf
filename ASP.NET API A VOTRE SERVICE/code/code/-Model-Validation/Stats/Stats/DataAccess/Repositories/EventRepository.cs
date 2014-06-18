using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stats.DataAccess.Entities;

namespace Stats.DataAccess.Repositories
{
    public class EventRepository : Repository<GameEvent> {
        public EventRepository( StatsDbContext context ) : base( context ) {}
    }
}