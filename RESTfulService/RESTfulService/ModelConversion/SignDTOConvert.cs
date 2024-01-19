using Data.ModelLayer;
using RESTfulService.DTOs;

namespace RESTfulService.ModelConversion
{
    public static class SignDTOConvert
    {
        public static SignDTO ToSignDTO(this Sign sign) 
        {
            return new SignDTO(
                sign.Id,
                sign.Size,
                sign.Resolution,
                sign.SignLocation,
                sign.StadiumId
            );
        }

        public static Sign ToSign(this SignDTO signDTO)
        {
            return new Sign(
                signDTO.Id,
                signDTO.Size,
                signDTO.Resolution,
                signDTO.Location,
                signDTO.StadiumId
            );
        }
    }
}
