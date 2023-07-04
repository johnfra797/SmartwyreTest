using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smartwyre.Data.Enum;

public class RebateCalculation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Identifier { get; set; }
    public string RebateIdentifier { get; set; }
    public IncentiveType IncentiveType { get; set; }
    public decimal Amount { get; set; }
}
