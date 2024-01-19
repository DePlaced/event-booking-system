using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigDSignClientDesktop.Model;
using BigDSignClientDesktop.Service;

namespace BigDSignClientDesktop.Controller
{

    /// <summary>
    /// Controller handling sign-related operations, acting as an intermediary between the UI and data access.
    /// </summary>
    public class SignControl
    {
        private readonly ISignAccess _sAccess;

        /// <summary>
        /// Constructor initializing the SignControl with a SignAccess instance.
        /// </summary>
        public SignControl()
        {
            _sAccess = new SignAccess();
        }

        /// <summary>
        /// Retrieves all signs from the data source.
        /// </summary>
        /// <returns>A task that represents a list of signs, or null if no signs are found.</returns>
        public async Task<List<Sign>> GetSignsByStadiumId(int stadiumId)
        {
            List<Sign> foundSigns = await _sAccess.GetSignsByStadiumId(stadiumId);
            return foundSigns;
        }

        /// <summary>
        /// Saves a new sign with the provided details to the data source.
        /// </summary>
        /// <returns>A task that represents the ID of the inserted sign.</returns>
        public async Task<int> SaveSign(string size, string resolution, string location, int stadiumId)
        {
            Sign newSign = new Sign(size, resolution, location, stadiumId);
            int insertedId = await _sAccess.SaveSign(newSign);
            return insertedId;
        }

        /// <summary>
        /// Deletes a sign with the specified ID from the data source.
        /// </summary>
        /// <param name="signIdToDelete">The ID of the sign to delete.</param>
        /// <returns>A task that represents a boolean indicating the success of the deletion operation.</returns>
        public async Task<bool> DeleteSign(int signIdToDelete)
        {
            bool deleted = await _sAccess.DeleteSign(signIdToDelete);
            return deleted;
        }

        /// <summary>
        /// Updates an existing sign in the data source with the provided sign details.
        /// </summary>
        /// <param name="signToUpdate">The sign object containing updated information.</param>
        /// <returns>A task that represents a boolean indicating the success of the update operation.</returns>
        public async Task<bool> UpdateSign(Sign signToUpdate)
        {
            bool updated = await _sAccess.UpdateSign(signToUpdate);
            return updated;
        }
    }
}
