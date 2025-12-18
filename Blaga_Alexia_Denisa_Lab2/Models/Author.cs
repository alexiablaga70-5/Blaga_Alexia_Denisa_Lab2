namespace Blaga_Alexia_Denisa_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICollection<Book>? Books { get; set; }

    }
}
