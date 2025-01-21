using CachePatterns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachePatterns.DataGateway.Cache
{
    public class MagazineKeyFactory : IKeyFactory
    {
        public string NewSpecificKey(object domainObj)
        {
            Magazine magazine = (Magazine)domainObj;
            string key = "";
            key += $"{nameof(magazine.Id)} = {magazine.Id}, ";
            key += $"{nameof(magazine.Name)} = {magazine.Name}, ";
            key += $"{nameof(magazine.IssueNr)} = {magazine.IssueNr}, ";
            key += $"{nameof(magazine.IssueDate)} = {magazine.IssueDate}";

            return key;
        }

        public string NewPartialKey(int id)
        {
            return $"{nameof(Magazine.Id)} = {id}";
        }
    }
}
