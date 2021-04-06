using DAL;

namespace BLL.Models
{

    public partial class StatusModel
    {
        public int ID { get; set; }

        public string StatusName { get; set; }

        public StatusModel(Status s)
        {
            ID = s.ID;
            StatusName = s.StatusName;
        }

        public StatusModel() { }
    }
}
