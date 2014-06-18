using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stats.DataAccess;
using Stats.Models;

namespace Stats.Controllers
{
    public class PlayerController : ApiController {
        private IStatsService _service;
        private IModelFactory _modelFactory;

        public PlayerController(  ) {
            _service = new StatsService( );
            _modelFactory = new ModelFactory( );
        }

        public IHttpActionResult Get( ) {
            var players = _service.Players.Get( );
            var models = players.Select( _modelFactory.Create );

            return Ok( models );
        }

        public IHttpActionResult Get( int id ) {
            try {
                var player = _service.Players.Get( id );
                var model = _modelFactory.Create( player );

                return Ok( model );
            } catch ( Exception ex ) {
                //Logging
#if DEBUG
                return InternalServerError( ex );
#endif
                return InternalServerError( );
            }
            
        }

        public IHttpActionResult Post([FromBody]PlayerModel playerModel ) {
            var playerEntity = _modelFactory.Create( playerModel );
            var player = _service.Players.Insert( playerEntity );

            var model = _modelFactory.Create( player );
            return Created( string.Format( "http://localhost:13362/api/player/{0}", model.PlayerId ), model );
        }
    }
}
