using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SignDLL.CreateDLL;

namespace SignDLL
{
    // when run project, the dll of this class created
    internal class CreateDLL
    {

        public string DllName { get; set; } = "Example name";
        public DateTimeOffset DllTime { get; set; } = new DateTimeOffset(2023, 2, 2, 2, 2, 2, TimeSpan.Zero);


    }
}
