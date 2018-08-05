using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class IndexPersonModel
    {
        public string Cnic { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public List<PersonModel> Persons { get; set; }

        public string FormNumber { get; set; }
    }
}
