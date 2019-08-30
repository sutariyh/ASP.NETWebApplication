using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBapp.Models
{
    public class RankColor
    {
        public int rank_id { get; set; }
        public int student_id { get; set; }
        public IEnumerable<SelectListItem> rank_beltcolor { get; set; }
        public System.DateTime rank_date { get; set; }

        public virtual student student { get; set; }
    }
}