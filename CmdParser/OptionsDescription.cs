using System;
using System.Collections.Generic;
using System.Text;

namespace CmdParser
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class OptionsDescription : Attribute
    {

    }
}
