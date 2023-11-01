using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Core.DTOs
{
    public class BaseUpdateDto
    {
        //update bazlı dto
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
