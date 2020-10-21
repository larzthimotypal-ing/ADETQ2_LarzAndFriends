using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADETQ2_LarzAndFriends.Models
{
    public class Members
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string StudentNumber { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }



    }
}
