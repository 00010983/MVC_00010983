using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CW_DSCC_10983_MVC.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }
        public string Title { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int Duration { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
    }
}
