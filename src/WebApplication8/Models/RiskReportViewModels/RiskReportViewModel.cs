using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;

namespace WebApplication8.Models.RiskReportViewModels
{

    public class DetailsViewModel
    {
        public RiskReport Report { get; set; }
        public List<RiskItem> RiskItems { get; set; }
    }

}
