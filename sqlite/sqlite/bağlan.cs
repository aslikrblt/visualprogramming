using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlite
{
    public class bağlan
    {
        public static SQLiteConnection connection = new SQLiteConnection("Data source=.\\veritabani.db;Versiyon=3");

    }
}
