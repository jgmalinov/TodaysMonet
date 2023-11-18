using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TodaysMonet.Models
{
    public class Status
    {
        public long id {  get; set; }
        public Statuses StatusType {  get; set; }
        public DateTime Timestamp { get; set; }
        public int MinutesLogged { get; set; }
        public int DeviationFromTarget { get; set; }
    }
}
