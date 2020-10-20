using System;

namespace Damacon.DAL.Database.EF
{
    public class UserLite
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public UserType UserType { get; set; }
        public Nullable<long> StoreID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public bool Disable { get; set; }
        public ApplicationLink DefaultLoginPage { get; set; }

    }
}
