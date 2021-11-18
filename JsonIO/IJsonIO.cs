using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerLibrary.IO
{
    public interface IJsonIO
    {
        void Save();

        void Load();
    }
}
