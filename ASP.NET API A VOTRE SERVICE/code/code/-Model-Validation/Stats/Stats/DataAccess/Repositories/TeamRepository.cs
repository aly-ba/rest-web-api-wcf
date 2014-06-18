﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stats.DataAccess.Entities;

namespace Stats.DataAccess.Repositories
{
    public class TeamRepository : Repository<Team> {
        public TeamRepository( StatsDbContext context ) : base( context ) {}
    }
}