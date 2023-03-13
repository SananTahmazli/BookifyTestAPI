using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstracts
{
    public interface IEmailRepository
    {
        void SendEmail(Message message);
    }
}