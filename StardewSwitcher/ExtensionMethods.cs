using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StardewSwitcher
{
  public static class ExtensionMethods
  {
    extension(XmlNode node)
    {
      public XmlNode? GetChildWithName(string name)
      {
        foreach(XmlNode childNode in node.ChildNodes)
        {
          if (childNode.Name == name) return childNode;
        }
        return null;
      }
    }
  }
}
