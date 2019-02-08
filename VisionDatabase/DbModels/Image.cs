using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisionDatabase.DbModels
{
    [Table("Image")]
    public class Image
    {

        [Key]
        [Column("ImageId", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string ImageId { get; set; }

        [Column("Filename", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Filename { get; set; }

        [Column("EventTime", TypeName = "datetime")]
        public DateTime EventTime { get; set; }

        [Column("CorrelationId", TypeName = "varchar(50)")]
        public string CorrelationId { get; set; }

        [Column("SequenceNumber", TypeName = "int")]
        public int SequenceNumber { get; set; }

        [Column("ImageLocation", TypeName = "varchar(255)")]
        [MaxLength(255)]
        public string ImageLocation { get; set; }

        [Column("Source", TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string Source { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ExpiryOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public List<Content> Contents { get; set; }


        public Image()
        {
            Contents = new List<Content>();
        }

    }
}
