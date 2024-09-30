using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.Ticket
{
    public class Ticket : BaseEntity
    {
        [Key]
        public int TicketId { get; set; }
        [MaxLength(150)]
        public string Text { get; set; }
        public int UserId { get; set; }
    }
}
