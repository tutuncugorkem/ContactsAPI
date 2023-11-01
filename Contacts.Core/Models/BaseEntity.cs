using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Core.Models
{
    public class BaseEntity  //base yapı olduğu için new anahtar sözcüğü ile nesne alamamamız lazım
    {

        public int Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        public long Phone { get; set; }

        public string Address { get; set; }

    }
}
