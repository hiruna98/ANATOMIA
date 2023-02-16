
public class DataModel
{
    public string _id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string layer { get; set; }
    public string organImage { get; set; }
    public string displayName { get; set; }

    public DataModel()
    {

    }

    public DataModel(string _id, string name, string description, string layer, string organImage, string displayName)
    {
        this._id = _id;
        this.name = name;
        this.description = description;
        this.layer = layer;
        this.organImage = organImage;
        this.displayName = displayName;
    }
}
