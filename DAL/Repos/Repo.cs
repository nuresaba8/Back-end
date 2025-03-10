using DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class Repo
    {
        protected Janata_WifiEntities db;
        protected Repo()
        {
            db = new Janata_WifiEntities();
        }
    }
}
