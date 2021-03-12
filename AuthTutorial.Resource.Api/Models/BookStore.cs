using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTutorial.Resource.Api.Models
{
    public class BookStore
    {
        public List<Book> Books => new List<Book>
        {
            new Book{Id = 1, Author = "J. K. Rowling", Title = "Harry Potter and Philosopher`s Stone", Price = 10.45M},
            new Book{Id = 2, Author = "Herman Melville", Title = "Moby-Dick", Price = 8.52M},
            new Book{Id = 3, Author = "Jules Verne", Title = "The Mysterious Island", Price = 7.11M},
            new Book{Id = 4, Author = "Carlo Collodi", Title = "The Adventures of Pinocchio", Price = 6.42M}
        };

        public Dictionary<Guid, int[]> Orders => new Dictionary<Guid, int[]>
        {
            { Guid.Parse("5fe16eac-997c-408f-8012-e078492c4f1c"), new int[] { 1, 3} },
            { Guid.Parse("b6e1955f-af39-41fa-9697-c12cb84746c8"), new int[] { 2, 3, 4} }
        };

    }
}
