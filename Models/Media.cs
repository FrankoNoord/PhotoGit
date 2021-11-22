using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PhotoHub.Models
{
    public partial class Media
    {
        public Media()
        {
            Access = new HashSet<Access>();
         
        }

        public Guid MediaId { get; set; }

        [Required(ErrorMessage = "Please enter a valid name for the Media")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Put in a valid file for the Media")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Please enter a valid Date for the Media")]
        public DateTime? DateUpload { get; set; }

         [Required(ErrorMessage = "Please enter valid Discription for the Media")]
        public string MediaDiscription { get; set; }
       

        public virtual ICollection<Access> Access { get; set; }

        //Added for the upload function
        
        [NotMapped]
        [DataType(DataType.Upload)]
        [Display(Name = "Select File")]
        [Required(ErrorMessage = "Choose a file to upload")]
        public IFormFile MediaFile { get; set; }

        //Added for the download function
        /*public int Id { get; set; }
        [Display(Name = "Uploaded File")]
        public String FileName { get; set; }
        public byte[] FileContent { get; set; }*/
    }
}
