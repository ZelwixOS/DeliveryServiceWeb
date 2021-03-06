using DAL;

namespace BLL.Models
{

    public partial class TypeOfCargoModel
    {   
        public int ID { get; set; }

        public string TypeName { get; set; }

        public double Coefficient { get; set; }

        public bool Active { get; set; }

        public TypeOfCargoModel(TypeOfCargo t)
        {
            ID = t.ID;
            TypeName = t.TypeName;
            Coefficient = t.Coefficient;
            Active = t.Active;
        }

        public TypeOfCargoModel() { }
    }
}
