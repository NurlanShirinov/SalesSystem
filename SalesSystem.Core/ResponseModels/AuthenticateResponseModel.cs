using SalesSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Core.ResponseModels
{
    public class AuthenticateResponsModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Token { get; set; }

        public AuthenticateResponsModel(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Username = user.Username;
            Email = user.Email;
            Token = token;
        }
    }
}
