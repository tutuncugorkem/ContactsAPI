using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Core.DTOs
{
    public class BaseDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

    }
}
