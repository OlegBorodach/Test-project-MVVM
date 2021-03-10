using System;

#nullable disable

namespace Model
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public string Contractor { get; set; }
        public DateTime Date { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
