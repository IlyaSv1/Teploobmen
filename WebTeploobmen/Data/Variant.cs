using System.ComponentModel.DataAnnotations;

namespace WebTeploobmen.Data
{
    public class Variant
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int H0 { get; set; }
        public int Tmal { get; set; }
        public int Tbol { get; set; }
        public double Wg { get; set; }
        public double Cg { get; set; }
        public double Cm { get; set; }
        public double Gm { get; set; }
        public double Av { get; set; }
        public double Da { get; set; }
    }
}
