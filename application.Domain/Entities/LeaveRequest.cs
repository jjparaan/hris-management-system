﻿using application.Domain.Common;
using application.Domain.Enums.Leave;

namespace application.Domain.Entities
{
    public class LeaveRequest : AuditableBaseEntity
    {
        public int LeaveRequestId { get; set; }
        public int EmployeeId { get; set; } // Foreign key to Employee entity
        public virtual Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveType LeaveType { get; set; } 
        public LeaveStatus Status { get; set; }
        public string Reason { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
