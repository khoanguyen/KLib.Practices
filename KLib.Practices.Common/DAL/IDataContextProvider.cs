using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLib.Practices.Common.DAL;

namespace KLib.Practices.DAL
{
    public interface IDataContextProvider
    {
        IDataContext Create();
    }
}
