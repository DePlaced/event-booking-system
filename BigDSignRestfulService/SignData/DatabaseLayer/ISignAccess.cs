using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignData.ModelLayer;

namespace SignData.DatabaseLayer
{
    public interface ISignAccess
    {
        ///<summary>
        ///gets a specific sign from the database based on the id
        ///</summary>
        Task<Sign?> GetSignById(int id);

        ///<summary>
        ///gets all signs from the database
        ///</summary>
        Task<List<Sign>> GetAllSigns(int id);

        ///<summary>
        ///adds a new sign row to the sign table in the database
        ///</summary>
        Task<int> CreateSign(Sign signToAdd);

        ///<summary>
        ///Updates the Sign with a specific ID in the database
        ///</summary>
        Task<bool> UpdateSign(int id, Sign signToUpdate);

        ///<summary>
        ///Deletes a Sign with the given ID
        ///</summary>
        Task<bool> DeleteSignById(int id);
    }
}
