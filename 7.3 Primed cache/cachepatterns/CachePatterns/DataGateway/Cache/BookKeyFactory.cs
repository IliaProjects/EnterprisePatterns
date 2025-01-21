using CachePatterns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachePatterns.DataGateway.Cache
{
    public class BookKeyFactory : IKeyFactory
    {
        public string NewSpecificKey(object domainObj)
        {
            Book book = (Book)domainObj;
            string key = "";
            key += $"{nameof(book.Id)} = {book.Id}, ";
            key += $"{nameof(book.Name)} = {book.Name}, ";
            key += $"{nameof(book.Author)} = {book.Author}, ";
            key += $"{nameof(book.Year)} = {book.Year}";

            return key;
        }

        public string NewPartialKey(int id)
        {
            return $"{nameof(Book.Id)} = {id}";
        }
    }
}
