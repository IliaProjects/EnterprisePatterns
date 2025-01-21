using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPatterns.RowDataGateway
{
    internal class PersonRowGateway : GatewayBase
    {
        public static string idColumn = "id";

        public static string emailColumn = "email";

        public static string firstNameColumn = "firstName";

        public static string lastNameColumn = "lastName";

        public string _id { get; private set; }
        public string _firstName { get; private set; }
        public string _lastName { get; private set; }
        public string _email { get; private set; }

        private PersonRowGateway() : base("Persons")
        {
        }
        public static PersonRowGateway Find(string id)
        {
            PersonRowGateway result = new PersonRowGateway();
            DataRow row = result.GetRow(id);

            result._id = (string)row[idColumn];
            result._email = (string)row[emailColumn];
            result._firstName = (string)row[firstNameColumn];
            result._lastName = (string)row[lastNameColumn];

            return result;
        }
        public static IEnumerable<PersonRowGateway> GetAll()
        {
            var result = new List<PersonRowGateway>();
            var table = new PersonRowGateway().GetTable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                PersonRowGateway gatewayUnit = new PersonRowGateway();

                gatewayUnit._id = (string)table.Rows[i][idColumn];
                gatewayUnit._email = (string)table.Rows[i][emailColumn];
                gatewayUnit._firstName = (string)table.Rows[i][firstNameColumn];
                gatewayUnit._lastName = (string)table.Rows[i][lastNameColumn];

                result.Add(gatewayUnit);
            }
            return result;
        }
        public PersonRowGateway(string firstname, string lastname, string email) : base("Persons")
        {
            _id = new Guid().ToString();
            _firstName = firstname;
            _lastName = lastname;
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
            var row = GetRow(_id);
            row[firstNameColumn] = firstName;
            UpdateRow(row);
        }

        public void setLastName(string lastName)
        {
            var row = GetRow(_id);
            row[lastNameColumn] = lastName;
            UpdateRow(row);
        }

        public void setEmail(string email)
        {
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
