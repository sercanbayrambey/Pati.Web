using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface IBaseApiCrudService
    {

        //I think its bad architecture. Refactor needed.
        bool Add(object entity);
        bool Update(object entity);
        bool Create(object entity);
        bool Delete(object entity);    
    }
}
