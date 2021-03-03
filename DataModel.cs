using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nir2
{
    interface DataModel
    {
        double[,] GetData(double? param = null);
        bool HasParam();
    }
}
