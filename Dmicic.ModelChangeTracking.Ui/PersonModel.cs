using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelChangeTracking.UI
{
    public class PersonModel : EditableObject<PersonModel>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
