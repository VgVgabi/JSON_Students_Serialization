using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_Students_Serialization
{
    public class User
    {
        //private double _tuitionFee = 10000;
        public string Name { get; set; }
        public int Age { get; set; }
        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }
        //                
        public override string ToString()
        {
            return Name + " " + Age;
        }
    }
}
