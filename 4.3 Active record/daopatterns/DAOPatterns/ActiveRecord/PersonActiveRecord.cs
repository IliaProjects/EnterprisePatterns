using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPatterns.RowDataGateway
{
    internal class PersonActiveRecord : GatewayBase
    {
        public static string idColumn = "id";

        public static string emailColumn = "email";

        public static string firstNameColumn = "firstName";

        public static string lastNameColumn = "lastName";

        public string _id { get; private set; }
        public string _firstName { get; private set; }
        public string _lastName { get; private set; }
        public string _email { get; private set; }

        private PersonActiveRecord() : base("Persons")
        {
        }
        public static PersonActiveRecord Find(string id)
        {
            PersonActiveRecord result = new PersonActiveRecord();
            DataRow row = result.GetRow(id);
            if (row == null)
            {
                throw new Exception("No such person registered");
            }

            result._id = (string)row[idColumn];
            result._email = (string)row[emailColumn];
            result._firstName = (string)row[firstNameColumn];
            result._lastName = (string)row[lastNameColumn];

            return result;
        }
        public static PersonActiveRecord FindByEmail(string email)
        {
            PersonActiveRecord result = new PersonActiveRecord();
            var row = result.FindByColumn("email", email).FirstOrDefault();
            if (row == null)
            {
                throw new Exception("No such email registered");
            }

            result._id = (string)row[idColumn];
            result._email = (string)row[emailColumn];
            result._firstName = (string)row[firstNameColumn];
            result._lastName = (string)row[lastNameColumn];

            return result;
        }
        public static IEnumerable<PersonActiveRecord> GetAll()
        {
            var result = new List<PersonActiveRecord>();
            var table = new PersonActiveRecord().GetTable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                PersonActiveRecord gatewayUnit = new PersonActiveRecord();

                gatewayUnit._id = (string)table.Rows[i][idColumn];
                gatewayUnit._email = (string)table.Rows[i][emailColumn];
                gatewayUnit._firstName = (string)table.Rows[i][firstNameColumn];
                gatewayUnit._lastName = (string)table.Rows[i][lastNameColumn];

                result.Add(gatewayUnit);
            }
            return result;
        }
        public PersonActiveRecord(string firstName, string lastName, string email) : base("Persons")
        {
            if (firstName == null || firstName.Length < 1)
            {
                throw new ArgumentNullException(nameof(firstName));
            }
            if (lastName == null || lastName.Length < 1)
            {
                throw new ArgumentNullException(nameof(lastName));
            }
            if (email == null || email.Length < 1) 
            { 
                throw new ArgumentNullException(nameof(email));
            }
            if (FindByColumn("email", email).Length > 0)
            {
                throw new ArgumentException("Email already registered");
            }
            _id = new Guid().ToString();
            _firstName = firstName;
            _lastName = lastName;
            _email = email;

            DataRow row = GetTable().NewRow();

            row[idColumn] = _id;
            row[firstNameColumn] = _firstName;
            row[lastNameColumn] = _lastName;
            row[emailColumn] = _email;

            InsertRow(row);
        }

        public void setFirstName(string firstName)
        {
            if (firstName == null || firstName.Length < 1)
            {
                throw new ArgumentNullException(nameof(firstName));
            }
            var row = GetRow(_id);
            row[firstNameColumn] = firstName;
            UpdateRow(row);
        }

        public void setLastName(string lastName)
        {
            if (lastName == null || lastName.Length < 1)
            {
                throw new ArgumentNullException(nameof(lastName));
            }
            var row = GetRow(_id);
            row[lastNameColumn] = lastName;
            UpdateRow(row);
        }

        public void setEmail(string email)
        {
            if (email == null || email.Length < 1)
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (FindByColumn("email", email).Length > 0)
            {
                throw new ArgumentException("Email already registered");
            }
            var row = GetRow(_id);
            row[emailColumn] = email;
            UpdateRow(row);
        }

        public void Delete()
        {
            DeleteRow(_id);
        }
    }
}
