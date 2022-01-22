using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Title { get; set; }
        public int GenreId { get; set; }
        // Genre object will be added
        public int AuthorId { get; set; }
        // Author object will be added
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}