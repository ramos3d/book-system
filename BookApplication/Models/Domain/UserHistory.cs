using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BookApplication.Models.Domain
{
    public class UserHistory
    {
		public Guid Id { get; set; }
        public Guid Users_Id { get; set; }
        public string To_Buy { get; set; }
        public string Owned_books { get; set; }
    }
}
