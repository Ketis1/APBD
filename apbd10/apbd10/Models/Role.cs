using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd10.Models;

[Table("Roles")]
public class Role
{
    [Key]
    [Column("PK_role")]
    public int RoleId { get; set; }
    
    [Column("name")]
    [MaxLength(100)]
    public string RoleName { get; set; }
    
    public IEnumerable<Account> Accounts { get; set; }
}