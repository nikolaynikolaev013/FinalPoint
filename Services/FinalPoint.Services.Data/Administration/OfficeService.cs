namespace FinalPoint.Services.Data.Administration
{
    using System.Threading.Tasks;
    using FinalPoint.Data.Common.Repositories;
    using FinalPoint.Web.ViewModels.Administration;

    public class OfficeService : IOfficeService
    {
        private readonly IDeletableEntityRepository<FinalPoint.Data.Models.Office> officeRep;

        public OfficeService(IDeletableEntityRepository<FinalPoint.Data.Models.Office> officeRep)
        {
            this.officeRep = officeRep;
        }

        public async Task CreateAsync(AddOfficeInputModel model)
        {
            var newOffice = new FinalPoint.Data.Models.Office();
            newOffice.PostCode = model.PostCode;
            newOffice.Name = model.Name;
            newOffice.OfficeType = model.OfficeType;
            //TODO OwnerId
            newOffice.CityId = model.CityId;
            newOffice.Address = model.Address;
            newOffice.ResponsibleSortingCenterId = model.ResponsibleSortingCenter;

            await this.officeRep.AddAsync(newOffice);
            await this.officeRep.SaveChangesAsync();
        }
    }
}
