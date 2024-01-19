using BigDSignWebApp.ViewModels;
using ModelLayer;

namespace BigDSignWebApp.ModelConversion
{
    public static class EventModelConvert
    {
        public static Event ToEvent(this EventModel eventModel)
        {
            return new Event(
                eventModel.Id,
                eventModel.EventName,
                eventModel.EventDate,
                eventModel.EventDescription,
                eventModel.Price,
                eventModel.AvailabilityStatus,
                eventModel.SignId,
                eventModel.RowId,
                eventModel.RowIdBig
            );
        }

        public static EventModel ToEventModel(this Event @event)
        {
            return new EventModel(
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
    }
}