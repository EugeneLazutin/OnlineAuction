using System;
using System.Linq;
using System.Web.Security;
using OnlineAuction.DAL.Repositories;
using OnlineAuction.Entities;

namespace OnlineAuction.WebStructure
{
    class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string email)
        {
            User user;
            using (var repo = new Repository<User>())
            {
                user = repo.GetList().FirstOrDefault(x => x.Email == email);
            }

            if (user != null)
            {
                Role role;
                using (var repo = new Repository<Role>())
                {
                    role = repo.Get(user.RoleId);
                }

                if (role != null)
                {
                    return new[] { role.Name };
                }
            }
            return new string[] { };
        }

        public override void CreateRole(string roleName)
        {
            using (var repo = new Repository<Role>())
            {
                repo.Add(new Role { Name = roleName });
            }
        }

        public override bool IsUserInRole(string email, string roleName)
        {
            User user;
            using (var repo = new Repository<User>())
            {
                user = repo.GetList().FirstOrDefault(x => x.Email == email);
            }

            if (user != null)
            {
                Role role;
                using (var repo = new Repository<Role>())
                {
                    role = repo.Get(user.RoleId);
                }

                if (role != null && role.Name == roleName)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
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

        public override string[] GetUsersInRole(string roleName)
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
}
