using System.Data;

namespace DenLilleShopGUI
{
    public class Parameters
    {
        public string Parameter { get; set; }
        public SqlDbType Type { get; set; }
        public object Value { get; set; }

        public Parameters()
        {
            this.Parameter = "";
            this.Type = SqlDbType.Text;
            this.Value = "";
        }
        public Parameters(string Parameter, SqlDbType Type, object Value)
        {
            this.Parameter = Parameter;
            this.Type = Type;
            this.Value = Value;
        }

    }
}
