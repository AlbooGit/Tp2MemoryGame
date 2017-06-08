using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExtentionMethods //Ce code a été fourni par Peter McCormick https://github.com/Malpp/LINQ-Capitalize/blob/master/ExtentionMethods.cs
{
  public static class MyExtensions
  {
    public static string Capitalize(this String str)
    {
      return char.ToUpper(str[0]) + str.Substring(1);
    }
  }
}

