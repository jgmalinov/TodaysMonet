namespace TodoApi.Models
{
    public class Status
    {
        public long id {  get; set; }
        public Statuses StatusType {  get; set; }
        public DateTime DateLogged { get; set; }
        public int MinutesLogged { get; set; }
        public int DeviationFromTarget { get; set; }
    }
}
