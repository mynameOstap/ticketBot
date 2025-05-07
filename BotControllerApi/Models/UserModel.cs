using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BotControllerApi.Models;



[Table("User")] public class UserModel
{
    [Column("id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("email")] public string Email { get; set; }
    [Column("hashedPassword")] public string hashedPassword { get; set; }
}