namespace Api.Model
{
    public class Orders
    {
        public int UserId { get; set; }
        public string Name { get; set; }=string.Empty;
        public int BookId { get; set; }

        public string BookName { get; set; } = string.Empty;

        public DateTime OrderedOn { get; set; }

        public int Returned { get; set; }
    }
}
