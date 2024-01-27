using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ConfigurationModel
    {
        [Key]
        [Column("KeyName")]
        public string KeyName { get; set; }
        public string Value { get; set; }
    }
}
