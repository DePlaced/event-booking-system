using Data.ModelLayer;
using RESTfulService.DTOs;
using System.Collections.Generic;
using Data.DatabaseLayer;

namespace RESTfulService.ModelConversion
{
    public static class StadiumDTOConvert
    {
        public static StadiumDTO ToStadiumDTO(this Stadium stadium)
        {
            return new StadiumDTO(
                stadium.Id,
                stadium.StadiumName,
                stadium.Street,
                stadium.City,
                stadium.Zipcode,
                stadium.AdminId
            );
        }

        // addressId and adminId are not part of the StadiumDTO but are additional properties of the Stadium model
        public static Stadium ToStadium(this StadiumDTO stadiumDTO)
        {
            return new Stadium(
                stadiumDTO.Id,
                stadiumDTO.StadiumName,
                stadiumDTO.Street,
                stadiumDTO.City,
                stadiumDTO.Zipcode,
                stadiumDTO.AdminId
            );
        }
    }
}
