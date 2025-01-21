using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public class PersonDataMapper : DataMapperBase, IDataMapper<Person>
    {
        public string personsTableName = "Persons";
        public string emailsTableName = "Emails";
        public IEnumerable<Person> GetAll()
        {
            DataTable personsTable = GetTable(personsTableName);
            DataTable emailsTable = GetTable(emailsTableName);

            var result = new List<Person>();
            foreach (DataRow personRow in personsTable.Rows)
            {
                result.Add(new Person
                {
                    Id = (string)personRow["id"],
                    FirstName = (string)personRow["firstName"],
                    LastName = (string)personRow["lastName"],
                    Email = (string)emailsTable.Select($"id = {(string)personRow["emailId"]}").FirstOrDefault()["email"]
                });
            }
            return result;
        }

        public Person Get(string id)
        {
            DataRow personDataRow = GetRow(GetTable(personsTableName), id);
            DataRow emailDataRow = GetRow(GetTable(emailsTableName), (string)personDataRow["emailId"]);
            var result = new Person() { 
                Id = (string)personDataRow["id"],
                FirstName = (string)personDataRow["firstName"],
                LastName = (string)personDataRow["lastName"],
                Email = (string)emailDataRow["email"],
            };
            return result;
        }

        public IEnumerable<Person> Find(string key, string value)
        {
            var result = new List<Person>();
            var rows = FindRows(GetTable(personsTableName), key, value);
            foreach (DataRow row in rows)
            {
                result.Add(new Person()
                {
                    Id = (string)row["id"],
                    FirstName = (string)row["firstName"],
                    LastName = (string)row["lastName"],
                    Email = (string)GetRow(GetTable(emailsTableName), (string)row["emailId"])["email"],
                });
            }
            return result;
        }

        public void Insert(Person person)
        {
            DataRow emailRow = GetTable(emailsTableName).NewRow();
            emailRow["id"] = new Guid().ToString();
            emailRow["email"] = person.Email;

            DataRow personRow = GetTable(personsTableName).NewRow();
            personRow["id"] = new Guid().ToString();
            personRow["emailId"] = emailRow["id"];
            personRow["firstName"] = person.FirstName;
            personRow["lastName"] = person.LastName;
            
            InsertRow(emailRow, emailsTableName);
            InsertRow(personRow, personsTableName);
        }

        public void Update(Person person)
        {

            DataRow personRow = GetRow(GetTable(personsTableName), person.Id);
            personRow["firstName"] = person.FirstName;
            personRow["lastName"] = person.LastName;

            DataRow emailRow = GetRow(GetTable(emailsTableName), (string)personRow["emailId"]);
            emailRow["email"] = person.Email;

            UpdateRow(personRow, personsTableName);
            UpdateRow(emailRow, emailsTableName);
        }

        public void Delete(string id)
        {
            string emailId = (string)GetRow(GetTable(personsTableName), id)["emailId"];
            DeleteRow(emailId, emailsTableName);
            DeleteRow(id, personsTableName);
        }
    }
}
