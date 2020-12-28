using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenMinhDuyThong.Models
{
    public class PhoneModels
    {
        [Key]
        public int IdPro { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string NamePro { get; set; }
        [Column(TypeName = "int")]
        public int Price { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string Description { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string CoverImageUrl { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        
        
    }
}
