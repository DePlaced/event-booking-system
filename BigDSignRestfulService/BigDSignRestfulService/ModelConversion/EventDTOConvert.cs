using SignData.ModelLayer;
using BigDSignRestfulService.DTOs;
using System.Collections.Generic;

namespace BigDSignRestfulService.ModelConversion
{
    public static class EventDTOConvert
    {
        public static EventDTO ToEventDTO(this Event @event)
        {
            return new EventDTO(
                @event.Id,
                @event.EventName,
                @event.EventDate,
                @event.EventDescription,
                @event.Price,
                @event.AvailabilityStatus,
                @event.SignId,
                @event.RowId,
                @event.RowIdBig
            );
        }

        public static Event ToEvent(this EventDTO eventDTO)
        {
            return new Event(
                eventDTO.Id,
                eventDTO.EventName,
                eventDTO.EventDate,
                eventDTO.EventDescription,
                eventDTO.Price,
                eventDTO.AvailabilityStatus,
                eventDTO.SignId,
                eventDTO.RowId,
                eventDTO.RowIdBig
            );
        }
    }
}
