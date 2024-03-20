using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AccountManagementMicroService.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string? createdby { get; set; }
        [Required]
        public DateTime? createddate { get; set; }
        [AllowNull]
        public string? updatedby { get; set; }
        [AllowNull]
        public DateTime? updateddate { get; set; }
        public bool? isdeleted { get; set; } = false;
    }
}
