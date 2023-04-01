using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01Sydorova.DataManage
{
    internal class DataStorage
    {
        private static IDataStorage _storage;
        internal static IDataStorage Storage { get { return _storage; } }
        internal static void Initialize(IDataStorage storage) { _storage = storage; }
    }
}
