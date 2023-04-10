namespace Api.Model
{
    public class Book:BaseClass
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } =string.Empty;
        public float Price { get; set; } = 0;
        public bool Ordered { get; set; } = false;
        public int CategoryId { get; set; }

        public BookCategory Category { get; set; } = new BookCategory();
    }
}
