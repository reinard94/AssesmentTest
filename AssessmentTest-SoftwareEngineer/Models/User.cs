using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessmentTest_SoftwareEngineer.Models
{
    public class UserRes
    {
        public List<User> data { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }
}