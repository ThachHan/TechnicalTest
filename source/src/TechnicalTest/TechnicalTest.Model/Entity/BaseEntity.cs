using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TechnicalTest.Model.Entity
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
        public int? CreatedUserId { get; set; }
        public int? UpdatedUserId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool IsDeleted { get; set; } = false;
    }
}
