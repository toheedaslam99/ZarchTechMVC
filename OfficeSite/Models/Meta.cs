using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_infoGlobalNew.Models
{
    public partial class Apply
    {
        [NotMapped]
        public HttpPostedFileBase FileResume { get; set; }
    }
}