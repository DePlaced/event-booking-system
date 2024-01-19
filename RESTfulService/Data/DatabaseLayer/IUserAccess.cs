using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public interface IUserAccess
    {
        /// <summary>
        /// Gets an user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The found user object, or null if nothing was found.</returns>
        User? GetUserById(int id);

        /// <summary>
        /// Gets an user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The found user object, or null if nothing was found.</returns>
        User? GetUserByUsername(string username);

        ///<summary>
        ///Creates a new user table row, from a user object
        ///</summary>
        /// <returns>user id</returns>
        int CreateUser(User userToAdd);

        /// <summary>
        /// gets a user role by id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>a userRole object</returns>
        UserRole? GetUserRoleById(int id);
    }
}
