using BigDSignClientDesktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignClientDesktop.Service
{
    /// <summary>
    /// Represents the contract for accessing stadium-related data.
    /// </summary>
    internal interface IStadiumAccess
    {
        /// <summary>
        /// Retrieves a stadium from the service based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the stadium to retrieve.</param>
        /// <returns>The retrieved stadium.</returns>
        Task<Stadium?> GetStadium(int id);

        /// <summary>
        /// Retrieves all stadiums from the service.
        /// </summary>
        /// <param name="id">The ID of the stadium to retrieve.</param>
        /// <returns>The list of retrieved stadiums.</returns>
        Task<List<Stadium>> GetAllStadiums();

        /// <summary>
        /// Saves a new stadium to the service.
        /// </summary>
        /// <param name="stadiumToSave">The stadium object to save.</param>
        /// <returns>The ID of the inserted stadium.</returns>
        Task<int> SaveStadium(Stadium stadiumToSave);

        /// <summary>
        /// Deletes a stadium with the specified ID.
        /// </summary>
        /// <param name="stadiumId">The ID of the stadium to delete.</param>
        /// <returns>True if the stadium is deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteStadium(int id);

        /// <summary>
        /// Updates an existing stadium.
        /// </summary>
        /// <param name="stadiumToUpdate">The stadium object containing updated information.</param>
        /// <returns>True if the stadium is updated successfully; otherwise, false.</returns>
        Task<bool> UpdateStadium(Stadium stadiumToUpdate);
    }
}
