namespace QmsDoc.Core
{
    public class DocProperty
    {
        string name;
        object value;

        public DocProperty()
        {

        }

        public DocProperty(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
        public object Value { get => value; set => this.value = value; }
        public string Name { get => name; set => name = value; }
    }
}