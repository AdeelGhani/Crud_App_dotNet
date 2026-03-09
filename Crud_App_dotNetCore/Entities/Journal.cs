using Crud_App_dotNetCore.Entities;
using System.ComponentModel.DataAnnotations;

public class Journal : BaseEntity
{
    [Required]
    public DateTime VoucherDate { get; set; }

    [Required]
    [MaxLength(2)]
    public string VoucherType { get; set; }  // CR, CP, BR, BP, JV

    [Required]
    [MaxLength(8)]
    public string VoucherRef { get; set; }

    [Required]
    public int VoucherNo { get; set; }

    [MaxLength(100)]
    public string Narration { get; set; }

    [MaxLength(4)]
    public string StoreCode { get; set; }

    public ICollection<JournalDetail> JournalDetails { get; set; }
}