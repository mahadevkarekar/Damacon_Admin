using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Damacon.WebApp.Models
{
  public class TableSchedulerModel
  {
    public int iYear { get; set; }
    public int iMonth { get; set; }
    public int iDay { get; set; }
    public bool bIsDisable { get; set; }
    public bool bIsUsed { get; set; }

    public List<int> Days;
  }
}