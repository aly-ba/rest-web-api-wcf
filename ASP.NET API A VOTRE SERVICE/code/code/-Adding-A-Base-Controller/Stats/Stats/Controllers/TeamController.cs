using System;
using System.Linq;
using System.Web.Http;
using Stats.DataAccess;
using Stats.Filters;
using Stats.Models;

namespace Stats.Controllers {
    public class TeamController : ApiController {
        private readonly IModelFactory _modelFactory;
        private readonly IStatsService _service;

        public TeamController( ) {
            _service = new StatsService( );
            _modelFactory = new ModelFactory( );
        }

        public IHttpActionResult Get( ) {
            try {
                var teams = _service.Teams.Get( );
                var models = teams.Select( _modelFactory.Create );

                return Ok( models );
            } catch ( Exception ex ) {
                return InternalServerError( ex );
            }
        }

        public IHttpActionResult Get( int id ) {
            try {
                var team = _service.Teams.Get( id );
                var model = _modelFactory.Create( team );

                return Ok( model );
            } catch ( Exception ex ) {
                return InternalServerError( ex );
            }
        }

        [ModelValidator]
        public IHttpActionResult Post( [FromBody] TeamModel teamModel ) {
            try {
                var teamEntity = _modelFactory.Create( teamModel );
                var team = _service.Teams.Insert( teamEntity );

                var model = _modelFactory.Create( team );
                return Created( string.Format( "http://localhost:13362/api/team/{0}", model.TeamId ), model );
            } catch ( Exception ex ) {
                return InternalServerError( ex );
            }
        }

        [ModelValidator]
        public IHttpActionResult Put( [FromBody] TeamModel teamModel ) {
            try {
                var teamEntity = _modelFactory.Create( teamModel );
                var team = _service.Teams.Update( teamEntity );

                var model = _modelFactory.Create( team );

                return Ok( model );
            } catch ( Exception ex ) {
                return InternalServerError( ex );
            }
        }

        public IHttpActionResult Delete( int id ) {
            try {
                var teamEntity = _service.Teams.Get( id );
                if ( teamEntity != null )
                    _service.Teams.Delete( teamEntity );

                return Ok( );
            } catch ( Exception ex ) {
                return InternalServerError( ex );
            }
        }
    }
}