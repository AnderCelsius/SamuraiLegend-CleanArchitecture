using SamuraiLegend.Application.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailRequest mailRequest);
    }
}
