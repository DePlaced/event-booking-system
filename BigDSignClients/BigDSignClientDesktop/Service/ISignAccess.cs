using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigDSignClientDesktop.Model;

namespace BigDSignClientDesktop.Service
{
    /// <summary>
    /// Represents the contract for accessing sign-related data.
    /// </summary>
    public interface ISignAccess
    {
        /// <summary>
        /// Retrieves all signs from the service based on the provided stadium ID.
        /// </summary>
        /// <param name="stadiumId">The ID of the stadium.</param>
        /// <returns>The list of retrieved signs.</returns>
        Task<List<Sign>> GetSignsByStadiumId(int stadiumId);

        /// <summary>
        /// Saves a new sign to the service.
        /// </summary>
        /// <param name="signToSave">The sign object to save.</param>
        /// <returns>The ID of the inserted sign.</returns>
        Task<int> SaveSign(Sign signToSave);

        /// <summary>
        /// Deletes a sign with the specified ID.
        /// </summary>
        /// <param name="signId">The ID of the sign to delete.</param>
        /// <returns>True if the sign is deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteSign(int signId);

        /// <summary>
        /// Updates an existing sign.
        /// </summary>
        /// <param name="signToUpdate">The sign object containing updated information.</param>
        /// <returns>True if the sign is updated successfully; otherwise, false.</returns>
        Task<bool> UpdateSign(Sign signToUpdate);
    }
}
