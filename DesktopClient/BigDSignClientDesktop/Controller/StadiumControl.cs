using BigDSignClientDesktop.Model;
using BigDSignClientDesktop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignClientDesktop.Controller
{
    /// <summary>
    /// Controller managing stadium-related operations, acting as an intermediary between the UI and data access.
    /// </summary>
    public class StadiumControl
    {
        private readonly IStadiumAccess _stAccess;

        /// <summary>
        /// Constructor initializing the StadiumControl with a StadiumAccess instance.
        /// </summary>
        public StadiumControl() 
        {
            _stAccess = new StadiumAccess();
        }

        /// <summary>
        /// Retrieves all stadiums from the data source.
        /// </summary>
        /// <returns>A task that represents a list of stadiums, or null if no stadiums are found.</returns>
        public async Task<List<Stadium>> GetAllStadiums()
        {
            List<Stadium> foundStadiums = await _stAccess.GetAllStadiums();
            return foundStadiums;
        }

        /// <summary>
        /// Saves a new stadium with the provided details to the data source.
        /// </summary>
        /// <returns>A task that represents the ID of the inserted stadium.</returns>
        public async Task<int> SaveStadium(string stadiumName, string street, string city, int zipcode, int adminId)
        {
            Stadium newStadium = new Stadium(stadiumName, street, city, zipcode, adminId);
            int insertedId = await _stAccess.SaveStadium(newStadium);
            return insertedId;
        }

        /// <summary>
        /// Deletes a stadium with the specified ID from the data source.
        /// </summary>
        /// <param name="stadiumIdToDelete">The ID of the stadium to delete.</param>
        /// <returns>A task that represents a boolean indicating the success of the deletion operation.</returns>
        public async Task<bool> DeleteStadium(int stadiumIdToDelete)
        {
            bool deleted = await _stAccess.DeleteStadium(stadiumIdToDelete);
            return deleted;
        }

        /// <summary>
        /// Updates an existing stadium in the data source with the provided stadium details.
        /// </summary>
        /// <param name="stadiumToUpdate">The stadium object containing updated information.</param>
        /// <returns>A task that represents a boolean indicating the success of the update operation.</returns>
        public async Task<bool> UpdateStadium(Stadium stadiumToUpdate)
        {
            bool updated = await _stAccess.UpdateStadium(stadiumToUpdate);
            return updated;
        }
    }
}
