using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Shared.Messages;

public class ExampleMessage : IMassage
{
    public string Text { get; set; }
}
