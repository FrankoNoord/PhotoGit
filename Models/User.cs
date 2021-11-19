using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGit.Models
{
    public class User
    {
        [Key]
        public int UId { get; set; }
        
        public  string UName { get; set; }

       


    }
}
