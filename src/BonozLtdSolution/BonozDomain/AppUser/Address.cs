namespace BonozDomain.AppUser
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RoadNumber { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string Division { get; set; }
        public string PoliceStation { get; set; }
        public string HouseNumber { get; set; }
        public User User { get; set; }
    }
}