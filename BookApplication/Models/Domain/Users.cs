using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BookApplication.Models.Domain
{
    public class Users
    {
		
		public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}
