using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chucksoft.Azure.Model
{
   public class CreateContainer
   {
       private readonly string _name;

       /// <summary>
       /// Initializes a new instance of the <see cref="CreateContainer"/> class.
       /// </summary>
       /// <param name="name">The name.</param>
       public CreateContainer(string name)
       {
           _name = name;
       }

       /// <summary>
       /// Gets this instance.
       /// </summary>
       /// <returns></returns>
       public string Get()
       {
           return string.Empty;
       }
   }
}
