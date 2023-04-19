namespace Api.Model
{
    public class EmailParam
    {
        public string?  FirstName  { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? EmailSubject { get; set; }
        public string? EmailRecipient { get; set; }
        public string? EmailCarbonCopies { get; set; }
        public string? EmailBody { get; set; }
        public bool IsSendEmail { get; set; }
    }
}
