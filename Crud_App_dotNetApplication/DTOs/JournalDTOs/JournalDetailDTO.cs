public class JournalDetailDTO
{
    public int? JdId { get; set; }   // Null for Create

    public short Part { get; set; }

    public string AccCode { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public string LineNarration { get; set; }
}