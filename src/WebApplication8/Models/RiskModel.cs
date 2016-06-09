using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApplication8.Models
{
    public partial class RiskReport
    {
        public int Id { get; set; }

        [Required MaxLength(128)]
        public string Title { get; set; }

        
        /// <summary>
        /// helper: get List<int> of RiskItem.Ids
        /// </summary>
        public IList<int> RiskItemIds
        {
            get
            {
                return this.RRRIs.Select(r => r.RiskItemId).ToList();
            }
        }

        /// <summary>
        /// helper: Add RiskItem
        /// </summary>
        /// <param name="ri"></param>
        public void AddRiskItem(RiskItem ri)
        {
            RRRIs.Add(new RRRI { RiskReport = this, RiskItem = ri });
        }

        public IList<RRRI> RRRIs { get; set; } = new List<RRRI>();
    }


    public partial class RiskItem
    {
        public int Id { get; set; }
      
        [Required MaxLength(128)]
        public string Description { get; set; }

        [Required]
        public int Score { get; set; }


        public int RiskCategoryId { get; set; }
        public RiskCategory RiskCategory { get; set; }

        public int RiskClassId { get; set; }
        public RiskClass RiskClass { get; set; }

        public IList<RRRI> RRRIs { get; set; } = new List<RRRI>();

    }


    public partial class RiskClass
    {
        public int Id { get; set; }

        [Display(Name = "Risk Classification") MaxLength(128)]
        public string Classification { get; set; }

        [Display(Name = "Sort Order") Required]
        public int Ordinal { get; set; }

        public IList<RiskItem> RiskItems { get; set; }  = new List<RiskItem>();
    }


    public partial class RiskCategory
    {
        public int Id { get; set; }

        [Display(Name ="Risk Category") Required MaxLength(128)]
        public string CategoryName { get; set; }

        [Display(Name ="Sort Order") Required]
        public int Ordinal { get; set; }

        public IList<RiskItem> RiskItems { get; set; } = new List<RiskItem>();
    }

    /// <summary>
    /// M2M table: RiskReport -> RiskItem
    /// </summary>
    public partial class RRRI
    {
        public int RiskReportId { get; set; }
        public int RiskItemId { get; set; }

        public RiskReport RiskReport { get; set; }
        public RiskItem RiskItem { get; set; }
    }
}
