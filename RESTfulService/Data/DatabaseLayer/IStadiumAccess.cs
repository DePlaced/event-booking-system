using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public interface IStadiumAccess
    {
        ///<summary>
        ///Adds a new Stadium to the Stadium table in the database, also creates a address row and a zipcode row, if it doesnt aldready exist
        ///</summary>
        Task<Stadium?> GetStadiumById(int id);

        ///<summary>
        ///Deletes a Stadium from Stadium table with the given ID
        ///</summary>
        Task<List<Stadium>> GetAllStadiums();

        ///<summary>
        ///Returns all the Stadiums in the Stadium table, with joined attributes from the Address and Zipcode table 
        ///</summary>
        Task<int> CreateStadium(Stadium stadiumToAdd);

        ///<summary>
        ///Returns a Stadium with the given ID
        ///</summary>
        Task<bool> UpdateStadium(int id, Stadium stadiumToUpdate);

        ///<summary>
        ///Updates the Stadium with a specific ID in the database
        ///</summary>
        Task<bool> DeleteStadiumById(int id);
    }
}
