using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Team:BaseEntity
    {
        [Required]
        [MinLength(7)]
        [MaxLength(50)]
        public string Fullname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Position { get; set; }
        [Required]
        [MinLength(7)]
        [MaxLength(100)]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }
    }
}
