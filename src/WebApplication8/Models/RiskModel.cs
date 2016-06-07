using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public partial class RiskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }

        [Display(Name ="Category")]
        public int RiskCategoryId { get; set; }
        public RiskCategory RiskCategory { get; set; }

        [Display(Name = "CLASS")]
        public int RiskClassId { get; set; }
        public RiskClass RiskClass { get; set; }
    }

    public partial class RiskClass
    {
        public int Id { get; set; }
        [Display(Name = "Risk Classification")]
        public string Classification { get; set; }
        [Display(Name = "Sort Order")]
        public int Ordinal { get; set; }
        public List<RiskItem> RiskItems { get; set; }
    }


    public partial class RiskCategory
    {
        public int Id { get; set; }
        [Display(Name ="Risk Category")]
        public string CategoryName { get; set; }
        [Display(Name ="Sort Order")]
        public int Ordinal { get; set; }
        public List<RiskItem> RiskItems { get; set; }

    }

}
