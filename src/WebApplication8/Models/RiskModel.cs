using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public partial class RiskReport
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public List<RRRI> RRRIs { get; set; }

        /// <summary>
        /// helper method Add riskitem to this report (for join table)
        /// </summary>
        /// <param name="ri"></param>
        public void AddRiskItem( RiskItem ri)
        {
            RRRI rrri = new RRRI { RiskReportId = this.Id, RiskItemId = ri.Id };
            RRRIs.Add(rrri);
        }

        /// <summary>
        /// get risk items related to this report
        /// </summary>
        public List<RiskItem> RiskItems
        {
            get
            {
                return RRRIs.Select(i => i.RiskItem).ToList();
            }
        }
    }

    public partial class RiskItem
    {
        public int Id { get; set; }
      
        [Required MaxLength(128)]
        public string Description { get; set; }
        [Required]
        public int Score { get; set; }

        [Display(Name ="Category")]
        public int RiskCategoryId { get; set; }
        public RiskCategory RiskCategory { get; set; }

        [Display(Name = "Class")]
        public int RiskClassId { get; set; }
        public RiskClass RiskClass { get; set; }

        public List<RRRI> RRRIs { get; set; }

    }


    public partial class RiskClass
    {
        public int Id { get; set; }
        [Display(Name = "Risk Classification")]
        public string Classification { get; set; }
        [Required MaxLength(128) Display(Name = "Sort Order")]
        public int Ordinal { get; set; }
        public virtual List<RiskItem> RiskItems { get; set; }
    }


    public partial class RiskCategory
    {
        public int Id { get; set; }
        [Required MaxLength(128) Display(Name ="Risk Category")]
        public string CategoryName { get; set; }
        [Display(Name ="Sort Order")]
        [Required]
        public int Ordinal { get; set; }
        public virtual List<RiskItem> RiskItems { get; set; }

    }

    /// <summary>
    /// M2M table: RiskReport -> RiskItem
    /// </summary>
    public partial class RRRI
    {
        public int RiskReportId { get; set; }
        public RiskReport RiskReport { get; set; }

        public int RiskItemId { get; set; }
        public RiskItem RiskItem { get; set; }

    }
}
