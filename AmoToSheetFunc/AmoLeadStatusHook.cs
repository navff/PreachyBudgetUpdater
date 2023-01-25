using System.Collections.Generic;

namespace AmoToSheetFunc
{
    public class AmoLeadStatusHook
    {
        public LeadStatus Leads { get; set; }  = new LeadStatus();
        public AccountStatus Account { get; set; } = new AccountStatus();
    }

    public class LeadStatus
    {
        public List<LeadStatusItem> Status { get; set; } = new List<LeadStatusItem>();
    }

    public class LeadStatusItem
    {
        public long Id { get; set; }
        public long Status_id { get; set; }
        public long Pipeline_id { get; set; }
        public long Old_status_id { get; set; }
        public long Old_pipeline_id { get; set; }
    }

    public class AccountStatus
    {
        public long Id { get; set; }
        public string Subdomain { get; set; }
    }

}