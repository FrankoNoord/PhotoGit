using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PhotoHub.Models
{
    public partial class Access
    {
        public Guid MediaAccessId { get; set; }
        public Guid? MediaId { get; set; }
        public string UserId { get; set; }
        public string Sharing { get; set; }

        public virtual Media Media { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
