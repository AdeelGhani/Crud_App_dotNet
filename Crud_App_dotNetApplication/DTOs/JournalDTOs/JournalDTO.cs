public class JournalDTO
{
    public int? JId { get; set; }  // Null for Create

    public DateTime VoucherDate { get; set; }

    public string VoucherType { get; set; }

    public string VoucherRef { get; set; }

    public int VoucherNo { get; set; }

    public string Narration { get; set; }

    public string StoreCode { get; set; }

    public int UserId { get; set; }

    public List<JournalDetailDTO> JournalDetails { get; set; }
}