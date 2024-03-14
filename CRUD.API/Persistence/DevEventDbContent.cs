using CRUD.API.Entities;

namespace CRUD.API.Persistence

{
    public class DevEventsDbContent
    {

        public List<DevEvent> DevEvents { get; set; }

        public DevEventsDbContent()
        {
            DevEvents = new List<DevEvent>();
        }

    }
}
