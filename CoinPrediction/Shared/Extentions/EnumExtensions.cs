namespace Shared.Extentions
{
    public static class EnumExtensions
    {
        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }

        public static byte ToByte(this Enum enumValue)
        {
            return Convert.ToByte(enumValue);
        }
    }
    public class EnumDescription : Attribute
    {
        public EnumDescription(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
    }
}
