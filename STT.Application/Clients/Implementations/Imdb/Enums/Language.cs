using System.ComponentModel;

namespace STT.Application.Clients.Implementations.Imdb.Enums
{
    public enum Language
    {
        [Description("en")]
        English = 0,

        [Description("ka")]
        Georgian = 1,

        [Description("uk")]
        Ukrainian = 2,

        [Description("ru")]
        Russian = 3,

        [Description("zh")]
        Chinese = 4
    }
}