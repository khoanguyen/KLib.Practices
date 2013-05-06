using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitTest.DbContextModel
{
    public partial class KLibDBTestEntities : DbContext
    {
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
            
    }
}
