namespace WebTeploobmen.Models
{
    public class HomeCalcViewModel
    {
        public int? TMalRes {  get; set; }
        public int? TBolRes { get; set; }
        public int? Razn {  get; set; }
        public int? H0 { get; set; }
        public int? Tmal { get; set; }
        public int? Tbol { get; set; }
        public double? Wg { get; set; }
        public double? Cg { get; set; }
        public double? Cm { get; set; }
        public double? Gm { get; set; }
        public double? Av { get; set; }
        public double? Da { get; set; }
        public List<double> TempMaterial { get; set; } = [260, 200, 170, 140, 120, 110];
        public List<double> TempGas { get; set; } = [100, 120, 150, 190, 250];
    }
}
