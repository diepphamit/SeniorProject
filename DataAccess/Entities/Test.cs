using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int Score { get; set; }
        public int TotalCorrect { get; set; }
        public DateTime CreateAt { get; set; }
        public int TotalSentences { get; set; }
        public virtual ICollection<TestDetail> TestDetails { get; set; }
    }
}
