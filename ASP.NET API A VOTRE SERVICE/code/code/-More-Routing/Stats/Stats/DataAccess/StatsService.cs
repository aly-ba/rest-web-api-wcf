using Stats.DataAccess.Entities;
using Stats.DataAccess.Repositories;

namespace Stats.DataAccess {
    public class StatsService : IStatsService {
        private Repository<GameEvent> _events;
        private Repository<Game> _games;
        private Repository<Player> _players;
        private Repository<Team> _teams;

        public Repository<Game> Games {
            get {
                if ( _games == null )
                    _games = new GameRepository( new StatsDbContext( ) );

                return _games;
            }
        }

        public Repository<Team> Teams {
            get {
                if ( _teams == null )
                    _teams = new TeamRepository( new StatsDbContext( ) );

                return _teams;
            }
        }

        public Repository<Player> Players {
            get {
                if ( _players == null )
                    _players = new PlayerRepository( new StatsDbContext( ) );

                return _players;
            }
        }

        public Repository<GameEvent> Events {
            get {
                if ( _events == null )
                    _events = new EventRepository( new StatsDbContext( ) );

                return _events;
            }
        }
    }
}