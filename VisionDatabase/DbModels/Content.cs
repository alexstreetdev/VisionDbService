using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VisionDatabase.DbModels
{
    public class Content
    {

        [Column("ContentId", TypeName = "bigint")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ContentId { get; set; }

        [Column("ImageId", TypeName = "varchar(50)")]
        [Required]
        public string ImageId { get; set; }

        [Column("X", TypeName = "int")]
        public int X { get; set; }

        [Column("Y", TypeName = "int")]
        public int Y { get; set; }

        [Column("Width", TypeName = "int")]
        public int Width { get; set; }

        [Column("Height", TypeName = "int")]
        public int Height { get; set; }

        [Column("ContentDescription")]
        public string ContentDescription { get; set; }

    }
}
