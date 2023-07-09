using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class JsonModel
    {
        public class ModeJson
        {
            public List<employees> employees { get; set; }
            public company company { get; set; }
        }
        public class employees
        {
            public string Id { get; set; }
            public string Department { get; set; }
            public string Position { get; set; }
            public string Name { get; set; }
            public decimal Salary { get; set; }
        }

        public class company
        {
            public string name { get; set; }
            public address address { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
        }
        public class address
        {
            public string street { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string zipcode { get; set; }
        }

    }
}
