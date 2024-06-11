using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CuaHangOto.Models
{
    public class UserRoleProvider : RoleProvider
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
            // Co so du lieu
            CSDLDataContext csdl = new CSDLDataContext("Data Source=binh;Initial Catalog=CHXE;Integrated Security=True");
            var userRoles = (from kh in csdl.KhachHangs
                             join KHROLE in csdl.KHROLEs
                             on kh.KHID equals KHROLE.KHId
                             join role in csdl.Roles
                             on KHROLE.RoleId equals role.RoleID
                             where kh.Email == username
                             select role.RoleName).ToArray();
            return userRoles;
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
}