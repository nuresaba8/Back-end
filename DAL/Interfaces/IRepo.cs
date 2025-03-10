using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepo<CLASS,ID,RET>
    {
        RET Create(CLASS obj);
        RET Update(CLASS obj,ID id);
        List<CLASS> Get(ID page,ID pagesize);
        List<CLASS> Get();
        bool Delete(ID id);
        CLASS Get(ID id);
        void ExportApplicationsToCSV(string filePath);
    }
}
