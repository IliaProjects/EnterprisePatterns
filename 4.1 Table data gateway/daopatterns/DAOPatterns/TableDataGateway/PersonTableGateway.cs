using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace DAOPatterns.TableDataGateway
{
    public class PersonTableGateway : GatewayBase, ITableGateway<Person>
    {
        public string idColumn = "id";
        public string emailColumn = "email";
        public string firstNameColumn = "firstName";
        public string lastNameColumn = "lastName";
        public PersonTableGateway() : base("Persons")
        {
        }

        public Person Get(string id)
        {
            DataRow row = GetRow(id);
            Person person = new Person
            {
                Id = (string)row[idColumn],
                Email = (string)row[emailColumn],
                FirstName = (string)row[firstNameColumn],
                LastName = (string)row[lastNameColumn],
            };
            return person;
        }

        public IEnumerable<Person> GetAll()
        {
            DataTable table = GetTable();
            var result = new List<Person>();

            foreach (DataRow row in table.Rows)
            {
                Person person = new Person
                {
                    Id = (string)row[idColumn],
                    Email = (string)row[emailColumn],
                    FirstName = (string)row[firstNameColumn],
                    LastName = (string)row[lastNameColumn],
                };
                result.Add(person);
            }
            return result;
        }

        public void Insert(Person person)
        {
            DataRow row = GetTable().NewRow();
            foreach (PropertyInfo property in person.GetType().GetProperties())
            {
                row[property.Name] = property.GetValue(person);
            }
            InsertRow(row);
        }

        public void Update(Person person)
        {
            DataRow row = GetTable().NewRow();
            foreach (PropertyInfo property in person.GetType().GetProperties())
            {
                row[property.Name] = property.GetValue(person);
            }
            UpdateRow(row);
        }

        public void Delete(string id)
        {
            DeleteRow(id);
        }
    }
}
