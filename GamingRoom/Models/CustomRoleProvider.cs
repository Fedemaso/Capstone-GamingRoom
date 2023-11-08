using System;
using System.Linq;
using System.Web.Security;
using GamingRoom.Models;

namespace GamingRoom
{


    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            ModelDBContext db = new ModelDBContext();
            UserCustomer utente = db.UserCustomers.FirstOrDefault(u => u.Email == username);
            if (utente != null)
            {
                string[] roles = new string[] { utente.Role };
                return roles;
            }
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }




    //public class CustomRoleProvider : RoleProvider
    //{
    //    public override string[] GetRolesForUser(string username)
    //    {
    //        using (var context = new ModelDBContext())
    //        {
    //            var user = context.UserCustomers.SingleOrDefault(u => u.Username == username);
    //            if (user != null)
    //            {
    //                return new[] { user.Role };
    //            }
    //        }
    //        return new string[] { };
    //    }

    //    public override void CreateRole(string roleName)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override bool IsUserInRole(string username, string roleName)
    //    {
    //        using (var context = new ModelDBContext())
    //        {
    //            var user = context.UserCustomers.SingleOrDefault(u => u.Username == username);
    //            if (user != null)
    //            {
    //                return user.Role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase);
    //            }
    //        }
    //        return false;
    //    }

    //    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override string[] GetUsersInRole(string roleName)
    //    {
    //        using (var context = new ModelDBContext())
    //        {
    //            return context.UserCustomers.Where(u => u.Role == roleName).Select(u => u.Username).ToArray();
    //        }
    //    }

    //    public override string[] GetAllRoles()
    //    {
    //        using (var context = new ModelDBContext())
    //        {
    //            return context.UserCustomers.Select(u => u.Role).Distinct().ToArray();
    //        }
    //    }

    //    public override bool RoleExists(string roleName)
    //    {
    //        using (var context = new ModelDBContext())
    //        {
    //            return context.UserCustomers.Any(u => u.Role == roleName);
    //        }
    //    }

    //    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    //    {
    //        using (var context = new ModelDBContext())
    //        {
    //            return context.UserCustomers.Where(u => u.Role == roleName && u.Username.Contains(usernameToMatch)).Select(u => u.Username).ToArray();
    //        }
    //    }

    //    public override string ApplicationName { get; set; }

    //    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
