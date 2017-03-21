namespace BeFit.Models
{
    public class TrainersServices
    {
        public int TrainerID { get; set; }
        public int ServiceID { get; set; }
        public Trainer Trainer { get; set; }
        public Service Service { get; set; }
    }
}