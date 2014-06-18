using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stats.DataAccess.Entities;

namespace Stats.Models
{
    public interface IModelFactory {
        PlayerModel Create( Player player );
        Player Create( PlayerModel playerModel );
    }

    public class ModelFactory : IModelFactory
    {
        public PlayerModel Create( Player player ) {
            return new PlayerModel {FirstName = player.FirstName, LastName = player.LastName, PlayerId = player.ID, TeamId = player.Team != null ? player.Team.ID : 0, TeamName = player.Team != null ? player.Team.Name : null};
        }

        public Player Create( PlayerModel playerModel ) {
            return new Player {FirstName = playerModel.FirstName, LastName = playerModel.LastName, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now};
        }
    }
}