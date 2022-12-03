using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzWorkerService
{
    internal class PrintTime:IPrintTime
    {
        public string GetCurrentTime()
        {
            return DateTime.Now.ToString();
        }
    }
}
