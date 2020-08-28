using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace worldgenerator {
    public static class Utility {
        public static PropertyInfo[] GetTypeProperties(object obj) {
            return obj.GetType().GetProperties();
        }
    }

    // public class Content<T> {
    //     public T Value;
    //     public string Name;
    //
    //     public Content(string name) {
    //         Name = name;
    //     }
    // }
    
}
  