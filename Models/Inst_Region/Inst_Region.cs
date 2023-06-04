using System;
using System.ComponentModel.DataAnnotations;


namespace HRMIS_API.Models
{
    public class Inst_Region
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string title { get; set; } = string.Empty;
        public int added_user_id { get; set; }
        public DateTime added_date { get; set; }
        public int updated_user_id { get; set; }
        public DateTime updated_date { get; set; }
        public int status { get; set; }
        public bool is_active { get; set; }
    }
}
