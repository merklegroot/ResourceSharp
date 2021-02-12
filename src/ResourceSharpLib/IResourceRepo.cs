using System.Reflection;

namespace ResourceSharpLib
{
    public interface IResourceRepo
    {
        string Get(string name);
        string Get(string name, Assembly assembly);
        
        T Get<T>(string name);
        T Get<T>(string name, Assembly assembly);
    }
}
