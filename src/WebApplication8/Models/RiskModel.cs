using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApplication8.Models
{
    public partial class RiskReport
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        /*
        /// <summary>
        /// helper: get RiskItems
        /// </summary>
        public IList<RiskItem> RiskItems
        {
            get
            {
                return RRRIs.Select(r => r.RiskItem).ToList();
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
        */

        [NotMapped]
        public virtual IList<RRRI> RRRIs { get; set; } = new List<RRRI>();
    }

    public partial class RiskItem
    {
        public int Id { get; set; }
      
        [Required MaxLength(128)]
        public string Description { get; set; }

        [Required]
        public int Score { get; set; }


        [ForeignKey("RiskCategoryId") Display(Name = "Category")]
        public int RiskCategoryId { get; set; }
        public virtual RiskCategory RiskCategory { get; set; }

        [ForeignKey("RiskClassId") Display(Name = "Class")]
        public int RiskClassId { get; set; }
        public virtual RiskClass RiskClass { get; set; }

        [NotMapped]
        public virtual IList<RRRI> RRRIs { get; set; } = new List<RRRI>();

    }


    public partial class RiskClass
    {
        public RiskClass()
        {
            RiskItems = new List<RiskItem>();
        }
        public int Id { get; set; }
        [Display(Name = "Risk Classification")]
        public string Classification { get; set; }
        [Required MaxLength(128) Display(Name = "Sort Order")]
        public int Ordinal { get; set; }

        [NotMapped]
        public virtual IList<RiskItem> RiskItems { get; set; } // = new List<RiskItem>();
    }


    public partial class RiskCategory
    {
        public RiskCategory()
        {
            RiskItems = new List<RiskItem>();
        }

        public int Id { get; set; }
        [Required MaxLength(128) Display(Name ="Risk Category")]
        public string CategoryName { get; set; }
        [Display(Name ="Sort Order")]
        [Required]
        public int Ordinal { get; set; }

        [NotMapped]
        public virtual IList<RiskItem> RiskItems { get; set; } // = new List<RiskItem>();
    }

    /// <summary>
    /// M2M table: RiskReport -> RiskItem
    /// </summary>
    public partial class RRRI
    {
        public int RiskReportId { get; set; }
        public int RiskItemId { get; set; }

        public virtual RiskReport RiskReport { get; set; }
        public virtual RiskItem RiskItem { get; set; }
    }
}
