using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AuthorDTO : BaseDTO
    {
        public string FullName { get; set; }
    }
}