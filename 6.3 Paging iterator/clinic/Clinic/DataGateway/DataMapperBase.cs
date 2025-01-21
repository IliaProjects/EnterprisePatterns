using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataGateway
{
    public abstract class DataMapperBase
    {
        protected AppDbContext _dbContext;

        public DataMapperBase()
        {
            _dbContext = AppDbContext.getInstance();
        }
        protected bool TryToSaveDataChanges()
        {
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
