using System;

namespace Stats.DataAccess.Entities {
    public abstract class ReportingBase {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}