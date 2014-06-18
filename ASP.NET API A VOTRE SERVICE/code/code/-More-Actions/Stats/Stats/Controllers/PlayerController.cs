using System;
using System.Linq;
using System.Web.Http;
using Stats.DataAccess;
using Stats.Filters;
using Stats.Models;

namespace Stats.Controllers {
    public class PlayerController : ApiController {
        private readonly IModelFactory _modelFactory;
        private readonly IStatsService _service;

        public PlayerController( ) {
            _service = new StatsService( );
            _modelFactory = new ModelFactory( );
        }

        public IHttpActionResult Get( ) {
            try {
                var players = _service.Players.Get( );
                var models = players.Select( _modelFactory.Create );

                return Ok( models );
            } catch ( Exception ex ) {
                return InternalServerError( ex );
            }
        }

        public IHttpActionResult Get( int id ) {
            try {
                var player = _service.Players.Get( id );
                var model = _modelFactory.Create( player );

                return Ok( model );
            } catch ( Exception ex ) {
                return InternalServerError( ex );
            }
        }

        [ModelValidator]
        public IHttpActionResult Post( [FromBody] PlayerModel playerModel ) {
            try {
                var playerEntity = _modelFactory.Create( playerModel );
                var player = _service.Players.Insert( playerEntity );

                var model = _modelFactory.Create( player );
                return Created( string.Format( "http://localhost:13362/api/player/{0}", model.PlayerId ), model );
            } catch ( Exception ex ) {
                return InternalServerError( ex );
            }
        }
    }
}