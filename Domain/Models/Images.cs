using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostDigitaliser.Server.Models;

namespace Domain.Models
{
    public class Images
    {
        public Guid? Id { get; set; }
        public string FileName { get; set; }

        //Navigation properties

        //Image can be of a receipt but also of another type (in the future invoices)
        public Receipts? Receipts { get; set; } // Reference navigation to dependent (receipts)
    }
}
