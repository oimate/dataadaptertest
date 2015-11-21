using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataadaptertest
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dt = Storage.GetTable("DMS_MFP");
            var q = dt.AsEnumerable()
                .First(dr => dr.Field<int>("Id") == 200);
            q["homeskid"] = "bbbb";
            Storage.UpdateMFPTable(dt);
        }
    }
}
