using Events.Application.DTO.RequestDTO;
using Events.Domain.Entities;

namespace TestEventApplication.UseCases
{
    public static class ObjectsGen
    {
        #region Events
        public static EventRequestDto GetEventRequest()
        {
            return new EventRequestDto
            {
                LocationId = 1,
                CategoryId = 1,
                Name = "Event",
                Description = "Description",
                EventTime = DateTime.UtcNow.AddDays(7),
                MaxParticipants = 20,
            };
        }

        public static Event GetEventObject()
        {
            return new Event
            {
                Id = 1,
                Name = "Event",
                Description = "Description",
                CategoryId = 1,
                LocationId = 1,
                EventTime = DateTime.UtcNow.AddDays(7),
                MaxParticipants = 20,
                ImagePath = "images\\G-x--WwY-RA.jpg"
            };
        }
        #endregion

        #region EventCategory
        public static EventCategory GetEventCategoryObject()
        {
            return new EventCategory
            { 
                Id = 1,
                Name = "Category" 
            };
        }

        public static EventCategoryRequestDto GetEventCategoryRequest()
        {
            return new EventCategoryRequestDto
            {
                Name = "Category"
            };
        }

        public static List<EventCategory> GetEventCategoriesList()
        {
            return new List<EventCategory>
            {
                new EventCategory { Id = 1, Name = "First Category" },
                new EventCategory { Id = 2, Name = "Second" },
                new EventCategory { Id = 3, Name = "Third"}
            };
        }

        #endregion

        #region Locations
        public static Location GetLocationObject()
        {
            return new Location
            {
                Id = 1,
                StreetId = 1,
                Name = "Location",
                House = "11"
            };
        }

        public static LocationRequestDto GetLocationRequest()
        {
            return new LocationRequestDto
            {
                StreetId = 1,
                Name = "Location",
                House = "11"
            };
        }
        #endregion

        #region Participant
        public static Participant GetParticipantObject()
        {
            return new Participant
            {
                Id = 1,
                EventId = 1,
                UserId = 1,
                RegistrationDate = DateTime.UtcNow,
            };
        }

        public static ParticipantRequestDto GetParticipantRequest()
        {
            return new ParticipantRequestDto
            {
                EventId = 1,
                UserId = 1,
                RegistrationDate = DateTime.UtcNow,
            };
        }
        #endregion

        #region Users
        public static List<User> GetUsersList()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "One",
                    Surname = "One",
                    Birthday = DateTime.Now.AddYears(-20),
                    Email = "m@gmail.com",
                    IsAdmin = true,
                    PasswordHash = "hash"
                },
                new User
                {
                    Id = 2,
                    Name = "Two",
                    Surname = "Two",
                    Birthday = DateTime.Now.AddYears(-20),
                    Email = "t@gmail.com",
                    IsAdmin = false,
                    PasswordHash = "hash"
                },
                new User
                {
                    Id = 1,
                    Name = "Three",
                    Surname = "Three",
                    Birthday = DateTime.Now.AddYears(-20),
                    Email = "t@gmail.com",
                    IsAdmin = false,
                    PasswordHash = "hash"
                },
            };
        }
        #endregion
    }
}
