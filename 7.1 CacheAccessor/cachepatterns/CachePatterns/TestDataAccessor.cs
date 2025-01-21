using CachePatterns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachePatterns
{
    internal class TestDataAccessor : IDataAccessor
    {
        List<Book> _books = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Author = "Dan Brown",
                Name = "The Da Vinci Code",
                Year = 2003
            },
            new Book()
            {
                Id = 2,
                Author = "Joanne Rowling",
                Name = "Harry Potter and the Prisoner of Azkaban",
                Year = 1999
            },
            new Book()
            {
                Id = 3,
                Author = "Mikhail Bulgakov",
                Name = "The Master and Margarita",
                Year = 1967
            },
            new Book()
            {
                Id = 4,
                Author = "	Antoine de Saint-Exupery",
                Name = "The Little Prince",
                Year = 1943
            },
            new Book()
            {
                Id = 5,
                Author = "Jerome Salinger",
                Name = "The Catcher in the Rye",
                Year = 1951
            }
        };

        List<Magazine> _magazines = new List<Magazine>()
        { 
            new Magazine()
            {
                Id = 1,
                Name = "Vogue",
                IssueNr = "10",
                IssueDate = new DateTime(2021, 10, 1)
            },
            new Magazine()
            {
                Id = 2,
                Name = "ELLE",
                IssueNr = "55",
                IssueDate = new DateTime(2020, 9, 1)
            },
            new Magazine()
            {
                Id = 3,
                Name = "Liza",
                IssueNr = "33",
                IssueDate = new DateTime(2022, 8, 1)
            },
            new Magazine()
            {
                Id = 4,
                Name = "Creazion",
                IssueNr = "3",
                IssueDate = new DateTime(2021, 3, 1)
            },
            new Magazine()
            {
                Id = 5,
                Name = "The Knitter",
                IssueNr = "11",
                IssueDate = new DateTime(2021, 11, 1)
            }
        };

        public Book getBook(int id)
        {
            return _books.Single(s => s.Id == id);
        }

        public Magazine getMagazine(int id)
        {
            return _magazines.Single(s => s.Id == id);
        }
    }
}
