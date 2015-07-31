using System;
using System.Reflection;

namespace com.shanevandermeer.wheresmymovies.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}