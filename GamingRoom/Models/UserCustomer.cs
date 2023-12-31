﻿

namespace GamingRoom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("UserCustomers")]
    public class UserCustomer
    {
        [Key]
        public int UserId { get; set; }

        //[Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        [Remote("IsEmailValid", "Validazioni", ErrorMessage = "Indirizzo e-mail già presente")]

        public string Email { get; set; }

        //[Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        //[Required]
        [StringLength(50)]
        public string LastName { get; set; }

        //[Required]
        [StringLength(255)]
        public string Address { get; set; }

        //[Required]
        [StringLength(10)]
        public string ZipCode { get; set; }

        //[Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public string Role { get; set; } = "User";
    }

}
