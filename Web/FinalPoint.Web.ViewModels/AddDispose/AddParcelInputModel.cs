namespace FinalPoint.Web.ViewModels.AddDispose
{
    public class AddParcelInputModel
    {
        public string Description { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public double Weight { get; set; }

        //public virtual Office ReceivingOffice { get; set; }

        //public virtual Client Sender { get; set; }

        //public virtual Client Recipent { get; set; }

        public int CurrentOfficeId { get; set; }

    }
}
