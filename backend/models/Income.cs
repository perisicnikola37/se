public class Income {
    public int Id { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
     public IncomeGroup IncomeGroup { get; set; }
}