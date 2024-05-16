using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities;

[PrimaryKey("CustomerId")]
[Table("tblCustomers")]
public class TblCustomers
{
    [Key]
    public int CustomerId { get; set; }
    [StringLength(100)]
    public string FirstName {  get; set; }
    [StringLength(150)]
    public string LastName { get; set; }
    [StringLength(500)]
    public string Email {  get; set; }
    [Column(TypeName = "datetime")]
    public DateTime Created { get; set; } = DateTime.Now;
    [Column(TypeName = "datetime")]
    public DateTime? Updated { get; set; } = null;
}
