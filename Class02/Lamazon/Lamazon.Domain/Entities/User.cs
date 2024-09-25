﻿namespace Lamazon.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string RoleKey { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public User()
        {
            Invoices = new HashSet<Invoice>();
        }
    }
}