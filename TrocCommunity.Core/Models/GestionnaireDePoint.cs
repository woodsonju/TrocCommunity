namespace TrocCommunity.Core.Models
{
    public class GestionnaireDePoint : BaseEntity
    {
        public int PointRecus { get; set; }
        public int PointUtilises { get; set; }
        public int  Solde { get; set; }
        public int AvanceDePoint { get; set; }
        public int PointDisponible { get; set; }
    }
}