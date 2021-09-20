using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrAgain.Models
{
    public class User
    {

        public User()
        {
            Messages = new List<Messages>();
        }


        public int Id { get; set; }


     
        [MaxLength(150)]
        public string Username { get; set; }

      
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }

      
        // 1 - * AppUSer || Message
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
