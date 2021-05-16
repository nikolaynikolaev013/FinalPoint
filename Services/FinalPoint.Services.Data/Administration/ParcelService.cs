using System;
using FinalPoint.Data.Common.Repositories;
using FinalPoint.Data.Models;

namespace FinalPoint.Services.Data.Administration
{
    public class ParcelService
    {
        private readonly IDeletableEntityRepository<Parcel> parcelRep;

        public ParcelService(IDeletableEntityRepository<Parcel> parcelRep)
        {
            this.parcelRep = parcelRep;
        }
    }
}
