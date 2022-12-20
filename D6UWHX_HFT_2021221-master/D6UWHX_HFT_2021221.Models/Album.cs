
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace D6UWHX_HFT_2021221.Models
{

    [Table("Album")]
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumID { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public int TracktID { get; set; }

        [NotMapped]
        public virtual ICollection<Artist> Artists { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Track Track { get; set; }
        
        [Required]
        public double BasePrice { get; set; }
        public override string ToString()
        {
            return $"\n{this.AlbumID} | {this.Title} {this.BasePrice}  ";
        }
    }
    }




  


