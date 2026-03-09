using Crud_App_dotNetCore.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class JournalDetail : BaseEntity
{
    [Required]
    public int JId { get; set; }

    [Required]
    public short Part { get; set; }

    [Required]
    [MaxLength(7)]
    public string AccCode { get; set; }

    [Column(TypeName = "decimal(14,2)")]
    public decimal Debit { get; set; }

    [Column(TypeName = "decimal(14,2)")]
    public decimal Credit { get; set; }

    [MaxLength(150)]
    public string LineNarration { get; set; }

    [ForeignKey("JId")]
    public Journal Journal { get; set; }
}