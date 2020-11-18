using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshFishMVVM.Helpers
{
    public interface IClosable
    {
        public Action Close { get; set; }
    }
}
