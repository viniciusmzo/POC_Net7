namespace Poc_net7.Entities
{
    public class DevEvent
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DevEventSpeaker> Speakers { get; set; }
        public bool IsDeleted { get; set; }

        public DevEvent()
        {
            Speakers = new List<DevEventSpeaker>();
            IsDeleted = false;
        }

        public void Update(string title, string description, DateTime startDate, DateTime endDate) 
        {
            
        }
    }
}
