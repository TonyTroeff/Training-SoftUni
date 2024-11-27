using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.IO.Contracts
{
    public interface IWriter
    {
        void Write(string message);
        void WriteLine(string message);
        void WriteText(string message);
    }
}
